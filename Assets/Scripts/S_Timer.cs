using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class S_Timer : MonoBehaviour
{
    public float timer = 0f;
    public bool timeStarted = true;
    public bool isInPause = false;
    public GUIStyle style;
    public GUIStyle highscoreStyle;
    public string niceTime;
    public string highScoreDisplay;
    public float highScore = 0f;
    public float newScore;
    private int minutes;
    private int seconds;
    private int millisec;
    public bool isInMainMenu;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        timeStarted = true;
        isInMainMenu = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().name == "Menu_Scene")
        {
            isInMainMenu = true;
        }
        else
        {
            isInMainMenu= false;
        }

        if (!isInMainMenu)
        {
            // Calculate timer if time has started
            if (timeStarted == true)
            {
                timer += Time.deltaTime;
                minutes = Mathf.FloorToInt(timer / 60F) % 60;
                seconds = Mathf.FloorToInt(timer % 60);
                millisec = Mathf.FloorToInt((timer * 100) % 100);
            }
            else if (!timeStarted && !isInPause)
            {
                SaveScore();
            }
        }
    }

    private void OnGUI()
    {
        if (!isInMainMenu)
        {
            // Format string
            niceTime = string.Format("{0:00}:{1:00}:{2:00}", minutes, seconds, millisec);

            // Display timer
            GUI.Label(new Rect(10, 10, 250, 100), niceTime, style);

            // Display timer
            GUI.Label(new Rect(10, 32, 250, 100), highScoreDisplay, highscoreStyle);
        }
        
    }

    private void SaveScore()
    {
        isInPause = true;
        newScore = minutes * 10000 + seconds * 100 + millisec;

        if (highScore == 0)
        {
            highScore = newScore;
            highScoreDisplay = string.Format("{0:00}:{1:00}:{2:00}", minutes, seconds, millisec);
        }
        else if (newScore > highScore)
        {
            highScore = newScore;
            highScoreDisplay = string.Format("{0:00}:{1:00}:{2:00}", minutes, seconds, millisec);
        }
    }
}

