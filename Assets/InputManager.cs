using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    [SerializeField] private InputActionMap inputs;
    private AudioSource audioSource;
    [SerializeField] private AudioClip[] buttonSounds;

    [SerializeField] private uint[] polyrhythms;
    [SerializeField] private CatSpinner[] cats;
    public float bpm = 60;  // where the beat aligns on the 2 polyrhythm
    
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        
        for (int i = 1; i <= 5; i++)
        {
            int index = i;
            InputAction action = inputs.FindAction(index.ToString());
            action.performed += _ => OnButtonPressed(index);
            action.canceled += _ => OnButtonReleased(index);
            action.Enable();
        }
    }

    private void OnButtonReleased(int index)
    {
        cats[index - 1].SetPressed(false);
    }
    
    private void OnButtonPressed(int index)
    {
        cats[index - 1].SetPressed(true);
    }
}
