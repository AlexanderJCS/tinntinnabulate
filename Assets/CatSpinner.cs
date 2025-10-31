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
    private Sprite[] spritesUp;
    private Sprite[] spritesIntermediate;
    private Sprite[] spritesDown;
    private SpriteRenderer spriteRenderer;
    private bool pressed;
    private float pressTime;
    private Transform transform;
    
    private void Start()
    {
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

    public void SetPressed(bool down, string message, Color color)
    {
        pressed = down;
        pressTime = Time.time;

        Vector3 pos = transform.position;
        floatingTextSpawner.SpawnText(new Vector2(pos.x, pos.y) + 2 * Vector2.up, message, color);
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
        
        float time = Time.time;
        int frameIndex = (int)(time * fps) % frames;
        spriteRenderer.sprite = spritesCurrent[frameIndex];
    }
}
