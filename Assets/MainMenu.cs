
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEditor;

public class MainMenu : MonoBehaviour
{
    public TMP_Text difficultytext;

    public void ToggleDifficulty()
    {
        if (difficultytext == null) 
        {
            Debug.LogError("Difficulty Text is not assigned in the Inspector!");
            return;
        }

        switch (GameGameMode.gameMode)
        {
            case GameMode.NORMAL:
                difficultytext.text = "‹ AUTOPLAY ›";
                GameGameMode.gameMode = GameMode.AUTOPLAY;
                break;
            case GameMode.AUTOPLAY:
                difficultytext.text = "‹ FREESTYLE ›";

                GameGameMode.gameMode = GameMode.FREESTYLE;
                break;
            case GameMode.FREESTYLE:
                difficultytext.text = "‹ NORMAL ›";

                GameGameMode.gameMode = GameMode.NORMAL;
                break;
        }
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    
    public void Quit()
    {
        #if UNITY_EDITOR
            EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
}