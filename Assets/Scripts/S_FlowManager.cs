using UnityEngine;
using UnityEngine.SceneManagement;

public class S_FlowManager : MonoBehaviour
{

    public GameObject player;
    public GameObject planet;
    public GameObject gameOverText;
    public GameObject timer;
    private GameObject AudioManager;
    private S_AudioManager audioManagerScript;
    private S_PlayerController playerController;
    private S_RotateObject rotatePlanet;
    private S_SpawnFruits fruitSpawner;
    private S_Timer timerScript;
    private float playerHealth;
    private float defaultHealthPoints;
    private bool isGameOver = false;
    private Quaternion planetStartRotation;
    private Vector3 playerStartPosition;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerController = player.GetComponent<S_PlayerController>();
        rotatePlanet = planet.GetComponent<S_RotateObject>();
        fruitSpawner = planet.GetComponent<S_SpawnFruits>();
        timerScript = timer.GetComponent<S_Timer>();
        playerHealth = playerController.healthPoints;
        planetStartRotation = planet.transform.rotation;
        playerStartPosition = player.transform.position;
        defaultHealthPoints = playerController.healthPoints;
        AudioManager = GameObject.Find("AudioManager");
        audioManagerScript = AudioManager.GetComponent<S_AudioManager>();
    }

    // Update is called once per frame
    void Update()
    {
        playerHealth = playerController.healthPoints;
        // Game is over if player health reach 0
        if (playerHealth <= 0 && !isGameOver)
        {
            GameOver();
        }        
    }

    private void LateUpdate()
    {
        // If we restart the game, restart every necessary value
        if (isGameOver && Input.GetButtonDown("Restart"))
        {
            RestartGame();
        }
        else if (isGameOver && Input.GetButtonDown("Back"))
        {
            // Play music again
            AudioManager.GetComponent<AudioSource>().Play();
            SceneManager.LoadScene("Menu_Scene");
        }
    }

    private void GameOver()
    {
        gameOverText.SetActive(true);
        rotatePlanet.rotationSpeed = 0f;
        rotatePlanet.canRotate = false;
        isGameOver = true;
        timerScript.timeStarted = false;
        AudioManager.GetComponent<AudioSource>().Stop();
    }

    private void RestartGame()
    {
        // Reset planet and player positions
        planet.transform.rotation = planetStartRotation;
        player.transform.position = playerStartPosition;
        rotatePlanet.canRotate = true;
        // Hide text
        gameOverText.SetActive(false);
        // Reset flow
        isGameOver = false;
        playerHealth = defaultHealthPoints;
        playerController.healthPoints = defaultHealthPoints;
        playerController.canDash = true;
        playerController.canMove = true;
        fruitSpawner.canSpawn = true;
        // Reset timer and let it run again
        timerScript.timeStarted = true;
        timerScript.timer = 0f;
        timerScript.isInPause = false;
        // Play music again
        AudioManager.GetComponent<AudioSource>().Play();
    }
}
