using UnityEngine;


[RequireComponent(typeof(SpriteAnimator), typeof(Collider2D))]
public class Fruit : MonoBehaviour
{
    public Sprite[] idleFrames;
    public Sprite[] explodeFrames;
    public float idleFps = 8f;
    public float explodeFps = 18f;

    SpriteAnimator anim;
    Collider2D col;
    bool collected;

    void Awake()
    {
        anim = GetComponent<SpriteAnimator>();
        col = GetComponent<Collider2D>();
    }

    void Start()
    {
        anim.Play(idleFrames, true, idleFps);
    }

    public void Collect()
    {
        if (collected) return;
        collected = true;
        col.enabled = false;

        if (GameManager.Instance != null) GameManager.Instance.AddFruit();

        if (explodeFrames != null && explodeFrames.Length > 0)
            anim.Play(explodeFrames, false, explodeFps);
        else
            Destroy(gameObject);
    }

    void Update()
    {
        if (collected && anim.Finished) Destroy(gameObject);
    }
}
