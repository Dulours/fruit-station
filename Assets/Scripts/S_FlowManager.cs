using UnityEngine;

public class S_FlowManager : MonoBehaviour
{

    public GameObject player;
    public GameObject planet;
    public GameObject gameOverText;
    private S_PlayerController playerController;
    private S_RotateObject rotatePlanet;
    private S_SpawnFruits fruitSpawner;
    private float playerHealth;
    private float defaultPlanetRotationSpeed;
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
        playerHealth = playerController.healthPoints;
        planetStartRotation = planet.transform.rotation;
        playerStartPosition = player.transform.position;
        defaultHealthPoints = playerController.healthPoints;
        defaultPlanetRotationSpeed = rotatePlanet.rotationSpeed;
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
    }

    private void GameOver()
    {
        gameOverText.SetActive(true);
        rotatePlanet.rotationSpeed = 0f;
        isGameOver = true;
    }

    private void RestartGame()
    {
        planet.transform.rotation = planetStartRotation;
        player.transform.position = playerStartPosition;
        gameOverText.SetActive(false);
        isGameOver = false;
        playerHealth = defaultHealthPoints;
        playerController.healthPoints = defaultHealthPoints;
        rotatePlanet.rotationSpeed = defaultPlanetRotationSpeed;
        playerController.canDash = true;
        playerController.canMove = true;
        fruitSpawner.canSpawn = true;
    }
}
