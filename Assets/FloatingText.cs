using UnityEngine;
using TMPro;

public class FloatingText : MonoBehaviour
{
    public float lifetime = 1.5f;
    public float riseSpeed = 1f;
    public float fadeDuration = 1f;

    private TextMeshProUGUI text;
    private CanvasGroup canvasGroup;
    private float startTime;
    private RectTransform rectTransform;

    private void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
        canvasGroup = gameObject.AddComponent<CanvasGroup>();
        rectTransform = GetComponent<RectTransform>();
    }

    public void Initialize(string message, Color color)
    {
        text.text = message;
        text.color = color;
        startTime = Time.time;
    }

    private void Update()
    {
        float elapsed = Time.time - startTime;

        // Move upward
        rectTransform.anchoredPosition += riseSpeed * Time.deltaTime * Vector2.up;

        // Fade out
        float t = elapsed / fadeDuration;
        canvasGroup.alpha = Mathf.Lerp(1f, 0f, t);

        // Destroy after lifetime
        if (elapsed >= lifetime)
        {
            Destroy(gameObject);
        }
    }
}