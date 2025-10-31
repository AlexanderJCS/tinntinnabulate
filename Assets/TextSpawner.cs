using UnityEngine;

public class FloatingTextSpawner : MonoBehaviour
{
    public Camera mainCamera;
    public Canvas canvas;
    public GameObject floatingTextPrefab;

    public void SpawnText(Vector3 worldPos, string message, Color color)
    {
        Vector3 screenPos = mainCamera.WorldToScreenPoint(worldPos);
        GameObject textObj = Instantiate(floatingTextPrefab, canvas.transform);
        textObj.GetComponent<RectTransform>().position = screenPos;
        textObj.GetComponent<FloatingText>().Initialize(message, color);
    }
}