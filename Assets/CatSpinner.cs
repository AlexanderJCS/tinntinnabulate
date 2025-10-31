using Unity.VisualScripting;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class CatSpinner : MonoBehaviour
{
    [SerializeField] private string pathUp;
    [SerializeField] private string pathIntermediate;
    [SerializeField] private string pathDown;
    [SerializeField] private int frames;
    [SerializeField] private float fps = 60;
    [SerializeField] private float intermediateStateDuration = 0.1f;
    [SerializeField] private FloatingTextSpawner floatingTextSpawner;
    [SerializeField] private float hitToleranceMs = 100f;
    private Sprite[] spritesUp;
    private Sprite[] spritesIntermediate;
    private Sprite[] spritesDown;
    private SpriteRenderer spriteRenderer;
    private bool pressed;
    private float pressTime;
    private Transform transform;
    private AudioSource audioSource;
    private float startTime;
    
    private void Start()
    {
        startTime = Time.time;
        audioSource = GetComponent<AudioSource>();
        transform = GetComponent<Transform>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        spritesUp = new Sprite[frames];
        spritesIntermediate = new Sprite[frames];
        spritesDown = new Sprite[frames];
        
        for (int i = 0; i < frames; i++)
        {
            spritesUp[i] = Resources.Load<Sprite>($"{pathUp}/{(i + 1).ToString("D4")}");
            spritesIntermediate[i] = Resources.Load<Sprite>($"{pathIntermediate}/{(i + 1).ToString("D4")}");
            spritesDown[i] = Resources.Load<Sprite>($"{pathDown}/{(i + 1).ToString("D4")}");
        }
    }

    public void SetPressed(bool down)
    {
        pressed = down;
        pressTime = Time.time;

        if (down)
        {
            Hit();
        }
    }

    private void Hit()
    {
        float time = Time.time - startTime;
        float rotationInterval = frames / fps;

        float expectedHitTime = Mathf.Round(time / rotationInterval) * rotationInterval;
        float deltaMs = (expectedHitTime - time) * 1000;
        
        if (Mathf.Abs(deltaMs) <= hitToleranceMs / 2)
        {
            audioSource.PlayOneShot(audioSource.clip);
            floatingTextSpawner.SpawnText(transform.position + Vector3.up, $"Hit!", Color.green);
        }
        else if (deltaMs < 0)
        {
            floatingTextSpawner.SpawnText(transform.position + Vector3.up, $"Dragging {deltaMs:0} ms", Color.red);
        }
        else
        {
            floatingTextSpawner.SpawnText(transform.position + Vector3.up, $"Rushing {deltaMs:0} ms", Color.yellow);
        }
    }
    
    private void Update()
    {
        ref Sprite[] spritesCurrent = ref spritesUp;
        if (Time.time - pressTime < intermediateStateDuration)
        {
            spritesCurrent = ref spritesIntermediate;
        }
        else if (pressed)
        {
            spritesCurrent = ref spritesDown;
        }
        
        float time = Time.time - startTime;
        int frameIndex = (int)(time * fps) % frames;
        spriteRenderer.sprite = spritesCurrent[frameIndex];
    }
}
