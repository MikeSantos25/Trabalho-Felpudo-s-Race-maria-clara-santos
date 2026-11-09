using UnityEngine;


public class Mover : MonoBehaviour
{
    [Tooltip("1 = anda junto com o chao. Maior que 1 = mais rapido que o chao.")]
    public float speedMultiplier = 1f;
    [Tooltip("Velocidade extra constante (ex.: o lesmo andando na direcao do jogador).")]
    public float extraSpeed = 0f;
    public float destroyMargin = 3f;

    Camera cam;

    void Start()
    {
        cam = Camera.main;
    }

    void Update()
    {
        GameManager gm = GameManager.Instance;
        if (gm != null && !gm.IsPlaying) return;

        float speed = (gm != null ? gm.CurrentSpeed * speedMultiplier : 0f) + extraSpeed;
        transform.position += Vector3.left * (speed * Time.deltaTime);

        if (cam != null)
        {
            float leftEdge = cam.transform.position.x - cam.orthographicSize * cam.aspect - destroyMargin;
            if (transform.position.x < leftEdge) Destroy(gameObject);
        }
    }
}
