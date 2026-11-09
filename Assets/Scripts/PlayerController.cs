using UnityEngine;


[RequireComponent(typeof(Rigidbody2D), typeof(BoxCollider2D), typeof(SpriteAnimator))]
public class PlayerController : MonoBehaviour
{
    [Header("Movimento")]
    public float jumpVelocity = 17f;
    public float gravityScale = 4f;
    [Range(0.3f, 1f)] public float crouchHeightFactor = 0.6f;

    [Header("Animacoes (arraste os sprites de cada pasta)")]
    public Sprite[] runFrames;
    public Sprite[] jumpFrames;
    public Sprite[] crouchFrames;
    public Sprite[] fallFrames;
    public float runFps = 14f;
    public float jumpFps = 12f;
    public float crouchFps = 16f;
    public float fallFps = 10f;

    [Header("Hitbox (ajustada automaticamente pelo sprite)")]
    public bool autoFitCollider = true;
    [Range(0.4f, 1f)] public float hitboxWidth = 0.7f;
    [Range(0.4f, 1f)] public float hitboxHeight = 0.9f;

    Rigidbody2D rb;
    BoxCollider2D box;
    SpriteAnimator anim;
    Vector2 standSize;
    Vector2 standOffset;
    bool grounded;
    bool crouching;
    bool dead;
    readonly ContactPoint2D[] contacts = new ContactPoint2D[8];

    bool Playing
    {
        get { return GameManager.Instance == null || GameManager.Instance.IsPlaying; }
    }

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        box = GetComponent<BoxCollider2D>();
        anim = GetComponent<SpriteAnimator>();

        rb.gravityScale = gravityScale;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;

        if (autoFitCollider)
        {
            Sprite reference = (runFrames != null && runFrames.Length > 0)
                ? runFrames[0]
                : GetComponent<SpriteRenderer>().sprite;

            if (reference != null)
            {
                Bounds b = reference.bounds;
                Vector2 size = new Vector2(b.size.x * hitboxWidth, b.size.y * hitboxHeight);
                box.size = size;
               
                box.offset = new Vector2(b.center.x, b.min.y + size.y * 0.5f);
            }
        }

        standSize = box.size;
        standOffset = box.offset;
    }

    void Update()
    {
        if (dead || !Playing) return;

        UpdateGrounded();

        if (grounded && GameInput.JumpPressed())
        {
            SetCrouch(false);
            rb.linearVelocity = new Vector2(0f, jumpVelocity);
            grounded = false;
        }
        else
        {
            SetCrouch(grounded && GameInput.CrouchHeld());
        }

        UpdateAnimation();
    }

    void UpdateGrounded()
    {
        grounded = false;
        if (rb.linearVelocity.y > 0.1f) return;

        int count = rb.GetContacts(contacts);
        for (int i = 0; i < count; i++)
        {
            if (Mathf.Abs(contacts[i].normal.y) > 0.5f)
            {
                grounded = true;
                break;
            }
        }
    }

    void SetCrouch(bool value)
    {
        if (value == crouching) return;
        crouching = value;

        if (crouching)
        {
            Vector2 size = new Vector2(standSize.x, standSize.y * crouchHeightFactor);
            box.size = size;
            box.offset = new Vector2(standOffset.x, standOffset.y - (standSize.y - size.y) * 0.5f);
        }
        else
        {
            box.size = standSize;
            box.offset = standOffset;
        }
    }

    void UpdateAnimation()
    {
        if (!grounded)
        {
            if (rb.linearVelocity.y > 0.1f) anim.Play(jumpFrames, false, jumpFps);
            else anim.Play(fallFrames, false, fallFps);
        }
        else if (crouching)
        {
            anim.Play(crouchFrames, false, crouchFps);
        }
        else
        {
            anim.Play(runFrames, true, runFps);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (dead) return;

        Fruit fruit = other.GetComponentInParent<Fruit>();
        if (fruit != null)
        {
            fruit.Collect();
            return;
        }

        if (other.GetComponentInParent<Enemy>() != null)
        {
            Die();
        }
    }

    void Die()
    {
        dead = true;
        SetCrouch(false);
        anim.Play(fallFrames, false, fallFps);
        rb.linearVelocity = new Vector2(0f, 6f);

        if (GameManager.Instance != null) GameManager.Instance.GameOver();
    }
}
