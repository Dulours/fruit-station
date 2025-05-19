using UnityEngine;

public class S_HighScoreManager : MonoBehaviour
{
    public float highScore;
    private GameObject timer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        DontDestroyOnLoad(this);

        // En gros il faut : 
        // Si on ne trouve pas de gameobject timer et que le highscore n'a pas encore de valeur, alors on met 0
        // Si on trouve le gameobject timer on récupère le highscore
        // Si on ne trouve pas le gameobject timer et que le highscore a une valeur, on affiche cette valeur
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
