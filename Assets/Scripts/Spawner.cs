using UnityEngine;

public class Spawner : MonoBehaviour
{
    [System.Serializable]
    public class Entry
    {
        public GameObject prefab;
        public float weight = 1f;
        [Tooltip("Altura (Y) em que o objeto nasce")]
        public float y;
    }

    [Header("Inimigos")]
    public Entry[] enemies;
    [Tooltip("Distancia minima e maxima entre um inimigo e o proximo")]
    public Vector2 enemyGap = new Vector2(11f, 18f);

    [Header("Frutas")]
    public GameObject[] fruits;
    public Vector2 fruitGap = new Vector2(4f, 8f);
    [Tooltip("Alturas (Y) possiveis das frutas: uma baixa (so correr) e uma alta (precisa pular)")]
    public float[] fruitHeights = { -3.5f, -1.1f };
    [Tooltip("Frutas nao nascem perto demais de um inimigo")]
    public float minFruitEnemyDistance = 3f;

    [Header("Geral")]
    public float firstSpawnDistance = 12f;
    public float spawnMargin = 2f;

    float enemyCountdown;
    float fruitCountdown;
    float sinceEnemy = 100f;
    Camera cam;

    void Start()
    {
        cam = Camera.main;
        enemyCountdown = firstSpawnDistance;
        fruitCountdown = firstSpawnDistance * 0.5f;
    }

    void Update()
    {
        GameManager gm = GameManager.Instance;
        if (gm == null || !gm.IsPlaying) return;

        float step = gm.CurrentSpeed * Time.deltaTime;
        enemyCountdown -= step;
        fruitCountdown -= step;
        sinceEnemy += step;

        if (enemyCountdown <= 0f)
        {
            SpawnEnemy();
            sinceEnemy = 0f;
            enemyCountdown = Random.Range(enemyGap.x, enemyGap.y);
        }

        if (fruitCountdown <= 0f)
        {
            if (sinceEnemy < minFruitEnemyDistance || enemyCountdown < minFruitEnemyDistance)
            {
                fruitCountdown = 1f; 
            }
            else
            {
                SpawnFruit();
                fruitCountdown = Random.Range(fruitGap.x, fruitGap.y);
            }
        }
    }

    float SpawnX()
    {
        if (cam == null) return 12f;
        return cam.transform.position.x + cam.orthographicSize * cam.aspect + spawnMargin;
    }

    void SpawnEnemy()
    {
        Entry entry = Pick(enemies);
        if (entry == null) return;
        Instantiate(entry.prefab, new Vector3(SpawnX(), entry.y, 0f), Quaternion.identity);
    }

    void SpawnFruit()
    {
        if (fruits == null || fruits.Length == 0 || fruitHeights == null || fruitHeights.Length == 0) return;

        GameObject prefab = fruits[Random.Range(0, fruits.Length)];
        if (prefab == null) return;

        float y = fruitHeights[Random.Range(0, fruitHeights.Length)];
        Instantiate(prefab, new Vector3(SpawnX(), y, 0f), Quaternion.identity);
    }

    static Entry Pick(Entry[] list)
    {
        if (list == null) return null;

        float total = 0f;
        foreach (Entry e in list)
            if (e != null && e.prefab != null) total += Mathf.Max(0f, e.weight);

        if (total <= 0f) return null;

        float roll = Random.value * total;
        Entry last = null;
        foreach (Entry e in list)
        {
            if (e == null || e.prefab == null) continue;
            last = e;
            roll -= Mathf.Max(0f, e.weight);
            if (roll <= 0f) return e;
        }
        return last;
    }
}
