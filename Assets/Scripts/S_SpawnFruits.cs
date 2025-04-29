using UnityEngine;
using System.Collections;

public class S_SpawnFruits : MonoBehaviour
{

    // Fruit spawn
    public GameObject[] fruits;
    public float maxSpawnTime = 5f;
    public float minSpawnTime = 1f;
    public bool canSpawn = true;
    private GameObject newFruit;
    private float timeElapsed = 0f;
    private float spawnTime;
    private float spawnZValue = 10.25f;

    // Player health
    public GameObject Player;
    private S_PlayerController playerController;
    private float playerHealth;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Set up first spawn time
        spawnTime = Random.Range(minSpawnTime, maxSpawnTime);
        timeElapsed = 0f;

        // Get player health points reference
        playerController = Player.GetComponent<S_PlayerController>();
        playerHealth = playerController.healthPoints;
    }

    // Update is called once per frame
    void Update()
    {
        // Spawn fruit when the timer is done and reset values
        timeElapsed += Time.deltaTime;
        if (timeElapsed > spawnTime && canSpawn)
        {
            SpawnFruit();
            timeElapsed = 0f;
            spawnTime = Random.Range(minSpawnTime, maxSpawnTime);
        }

        // Stop fruit spawn if the player has no health left
        playerHealth = playerController.healthPoints;
        if (playerHealth <= 0f)
        {
            canSpawn = false;
            if (Input.GetButtonDown("Restart"))
            {
                DestroyAllFruits();
            }
        }
    }

    void SpawnFruit()
    {
        // Spawn a random fruit of the index
        int randomIndex = Random.Range(0, fruits.Length);
        newFruit = Instantiate(fruits[randomIndex]);
        newFruit.transform.position = new Vector3(Random.Range(-3f, 3f), 0, spawnZValue);
        newFruit.transform.rotation = Quaternion.Euler(90, 0, 0);
        newFruit.transform.parent = transform;
    }

    private void DestroyAllFruits()
    {
        // créer une boucle for qui check tous les enfants et supprime tous ceux qui ne s'appellent pas "SM_PlanetStation"
       
    }

}
