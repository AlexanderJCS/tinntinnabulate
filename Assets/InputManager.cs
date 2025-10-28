using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    [SerializeField] private InputActionMap inputs;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for (int i = 1; i <= 5; i++)
        {
            int index = i;
            InputAction action = inputs.FindAction(index.ToString());
            action.performed += ctx => OnButtonPressed(index);
            action.Enable();
        }
    }

    private void OnButtonPressed(int index)
    {
        Debug.Log("Pressed button: " + index);
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
