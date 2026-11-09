using UnityEngine;

/// <summary>
/// Animador simples de sprites (passa os quadros um por um).
/// Assim voce nao precisa criar Animator nem AnimationClip.
/// </summary>
[RequireComponent(typeof(SpriteRenderer))]
public class SpriteAnimator : MonoBehaviour
{
    SpriteRenderer sr;
    Sprite[] current;
    bool loop;
    float frameTime = 1f / 12f;
    float timer;
    int index;

  
    public bool Finished { get; private set; }

    public void Play(Sprite[] frames, bool loopAnimation = true, float fps = 12f)
    {
        if (frames == null || frames.Length == 0) return;
        if (sr == null) sr = GetComponent<SpriteRenderer>();

        if (frames == current && (loopAnimation || !Finished)) return;

        current = frames;
        loop = loopAnimation;
        frameTime = 1f / Mathf.Max(1f, fps);
        timer = 0f;
        index = 0;
        Finished = false;
        sr.sprite = current[0];
    }

    void Update()
    {
        if (current == null || Finished) return;

        timer += Time.deltaTime;
        while (timer >= frameTime)
        {
            timer -= frameTime;
            index++;

            if (index >= current.Length)
            {
                if (loop)
                {
                    index = 0;
                }
                else
                {
                    index = current.Length - 1;
                    Finished = true;
                    break;
                }
            }
        }

        sr.sprite = current[index];
    }
}
