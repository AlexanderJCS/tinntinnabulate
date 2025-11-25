/*
 * Disclaimer: this script is written by AI
 */

using UnityEngine;

[RequireComponent(typeof(Camera))]
public class ForceAspectRatio : MonoBehaviour
{
    public float targetAspect = 16f / 9f;
    Camera cam;

    void Start()
    {
        cam = GetComponent<Camera>();
        ApplyAspect();
    }

    void ApplyAspect()
    {
        float windowAspect = (float)Screen.width / Screen.height;
        float scale = windowAspect / targetAspect;

        if (scale < 1f)
        {
            // Window is too tall → letterbox (top/bottom black bars)
            float viewportHeight = scale;
            cam.rect = new Rect(
                0f,
                (1f - viewportHeight) * 0.5f,
                1f,
                viewportHeight
            );
        }
        else
        {
            // Window is too wide → pillarbox (left/right black bars)
            float viewportWidth = 1f / scale;
            cam.rect = new Rect(
                (1f - viewportWidth) * 0.5f,
                0f,
                viewportWidth,
                1f
            );
        }
    }
}