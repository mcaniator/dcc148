using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorController : MonoBehaviour
{
    [SerializeField] Sprite[] sprites;

    private SpriteRenderer spriteRenderer;
    private float elapsedTime = 0;
    private int currentFrame = 0;
    private bool playing = false;
    private float frameTime;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        frameTime = 1.0f / sprites.Length;
    }

    // Update is called once per frame
    void Update()
    {
        if(playing)
        {
            elapsedTime += Time.deltaTime;
            if(elapsedTime >= frameTime)
            {
                elapsedTime = 0;
                currentFrame = (currentFrame + 1) % sprites.Length;
                spriteRenderer.sprite = sprites[currentFrame];
                Debug.Log(currentFrame);
            }
        }
    }

    public void Play()
    {
        playing = true;
    }

    public void Stop()
    {
        playing = false;
        currentFrame = 1;
        elapsedTime = 0;
        spriteRenderer.sprite = sprites[currentFrame];
    }
}
