using UnityEngine;

[RequireComponent(typeof(SpriteAnimator))]
public class Enemy : MonoBehaviour
{
    public Sprite[] frames;
    public float fps = 10f;

    void Start()
    {
        GetComponent<SpriteAnimator>().Play(frames, true, fps);
    }
}
