using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    [SerializeField] private InputActionMap inputs;
    private AudioSource audioSource;
    [SerializeField] private AudioClip[] buttonSounds;
    
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        
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
        audioSource.PlayOneShot(buttonSounds[index - 1]);
    }
}
