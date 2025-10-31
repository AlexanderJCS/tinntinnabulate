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
    public float startTime;
    public float tolerance = 0.1f;  // percent of the polyrhythm interval
    private bool firstUpdate = false;
    
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        
        for (int i = 1; i <= 5; i++)
        {
            int index = i;
            InputAction action = inputs.FindAction(index.ToString());
            action.performed += ctx => OnButtonPressed(index);
            action.canceled += ctx => OnButtonReleased(index);
            action.Enable();
        }

        startTime = Time.time;
    }

    private IEnumerator BeepOn2ndBeat()
    {
        while (true)
        {
            audioSource.PlayOneShot(buttonSounds[0]);

            yield return new WaitForSeconds(60f / bpm);
        }
    }

    private void Update()
    {
        if (!firstUpdate)
        {
            StartCoroutine(BeepOn2ndBeat());
            firstUpdate = true;
        }
    }

    private void OnButtonReleased(int index)
    {
        Debug.Log(cats[index - 1] == null);
        cats[index - 1].SetPressed(false);
    }
    
    private void OnButtonPressed(int index)
    {
        audioSource.PlayOneShot(buttonSounds[index - 1]);

        cats[index - 1].SetPressed(true);
        
        float pressTime = Time.time - startTime;             // seconds since start
        float beatInterval = 60f / bpm;                      // seconds per beat
        float cycleSeconds = 2f * beatInterval;              // your cycle is 2 beats

        // safe array access
        if (index - 1 < 0 || index - 1 >= polyrhythms.Length)
        {
            Debug.LogWarning("polyrhythms array missing entry for index " + index);
            return;
        }

        uint poly = polyrhythms[index - 1];
        if (poly < 1) return;

        // each subdivision (the expected hits) in seconds:
        float subdivisionSec = cycleSeconds / poly;

        // find nearest expected hit time and error (signed)
        float nearestIndex = Mathf.Round(pressTime / subdivisionSec);
        float expectedTime = nearestIndex * subdivisionSec;
        float error = pressTime - expectedTime; // + = late (dragging), - = early (rushing)

        // tolerance: fraction of one subdivision. We'll treat tolerance as total window fraction.
        float windowHalf = subdivisionSec * tolerance * 0.5f;

        if (Mathf.Abs(error) <= windowHalf)
        {
            Debug.Log($"{index}: on time (error {error:0.000}s)");
            return;
        }

        // normalized: 0..1 where 1 == half the subdivision (the worst possible within cycle)
        float percentOff = Mathf.Clamp01(Mathf.Abs(error) / (subdivisionSec * 0.5f)) * 100f;

        if (error > 0f)
        {
            Debug.Log($"{index}: dragging {percentOff:0.00}% (error {error:0.000}s)");
        }
        else
        {
            Debug.Log($"{index}: rushing {percentOff:0.00}% (error {error:0.000}s)");
        }
    }
}
