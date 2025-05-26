using UnityEngine;

public class S_HighScoreManager : MonoBehaviour
{
    public float highScore;
    public GameObject timer;
    private S_Timer timerScript;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        DontDestroyOnLoad(this);

        timer = GameObject.Find("Timer");
        timerScript = timer.GetComponent<S_Timer>();
        if (timer == null)
        {
            highScore = 0f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(timerScript.highScore > highScore)
        {
            highScore = timerScript.highScore;
        }

        print(highScore);
    }
    private void OnLevelWasLoaded(int level)
    {
        timer = GameObject.Find("Timer");
        timerScript = timer.GetComponent<S_Timer>();
    }
}

