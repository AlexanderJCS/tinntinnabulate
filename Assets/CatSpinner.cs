using UnityEngine;

public class CatSpinner : MonoBehaviour
{
    [SerializeField] private string path;
    [SerializeField] private int frames;
    [SerializeField] private float fps = 60;
    private Sprite[] sprites;
    private SpriteRenderer spriteRenderer;
    
    
    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        sprites = new Sprite[frames];
        
        for (int i = 0; i < frames; i++)
        {
            sprites[i] = Resources.Load<Sprite>($"{path}/{(i + 1).ToString("D4")}");
        }
    }

    private void Update()
    {
        float time = Time.time;
        int frameIndex = (int)(time * fps) % frames;
        spriteRenderer.sprite = sprites[frameIndex];
        Debug.Log(frameIndex);
    }
}
