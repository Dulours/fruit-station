using UnityEngine;

public class S_Timer : MonoBehaviour
{
    public float timer = 0f;
    public bool timeStarted = true;
    public GUIStyle style;
    private int minutes;
    private int seconds;
    private int millisec;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        timeStarted = true;
    }

    // Update is called once per frame
    void Update()
    {
        // Calculate timer if time has started
        if (timeStarted == true)
        {
            timer += Time.deltaTime;
            minutes = Mathf.FloorToInt(timer / 60F) % 60;
            seconds = Mathf.FloorToInt(timer % 60);
            millisec = Mathf.FloorToInt((timer * 100) % 100);
        }
    }

    private void OnGUI()
    {
        // Format string
        string niceTime = string.Format("{0:00}:{1:00}:{2:00}", minutes, seconds, millisec);

        // Display timer
        GUI.Label(new Rect(10, 10, 250, 100), niceTime, style);
    }
}

