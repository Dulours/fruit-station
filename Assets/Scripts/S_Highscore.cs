using UnityEngine;

public class S_Highscore : MonoBehaviour
{
    public GameObject timer;
    private S_Timer timerScript;
    private string highScoreDisplay;
    private float highscore;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        timerScript = timer.GetComponent<S_Timer>();
        highScoreDisplay= timerScript.highScoreDisplay;
        highscore = timerScript.highScore;

        if (highscore == 0)
        {
            GetComponent<TMPro.TextMeshProUGUI>().text = "Highscore : 0";
        }
        else
        {
            GetComponent<TMPro.TextMeshProUGUI>().text = "Highscore : " + highScoreDisplay;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
