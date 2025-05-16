using UnityEngine;
using System.Collections;

public class S_SpawnFruits : MonoBehaviour
{

    // Fruit spawn
    public GameObject[] fruits;
    public float maxSpawnTime = 5f;
    public float minSpawnTime = 1f;
    public float spawnSpeedMult = 2f;
    public bool canSpawn = true;
    private GameObject newFruit;
    private float timeElapsed = 0f;
    private float spawnTime;
    private float spawnZValue = 10.25f;
    private float minSpawnTimeInit;
    private float maxSpawnTimeInit;

    // Player health
    public GameObject Player;
    private S_PlayerController playerController;
    private float playerHealth;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Get the min & max spawn time initial values
        minSpawnTimeInit = minSpawnTime;
        maxSpawnTimeInit = maxSpawnTime;

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
        // Reduce min and max spawn time as time passes
        minSpawnTime = Mathf.Clamp(minSpawnTime - spawnSpeedMult * Time.deltaTime, 0.1f, minSpawnTimeInit);
        maxSpawnTime = Mathf.Clamp(maxSpawnTime - spawnSpeedMult * Time.deltaTime, 0.4f, maxSpawnTimeInit);

        // Spawn fruit when the timer is done and reset values
        timeElapsed += Time.deltaTime;
        if (timeElapsed > spawnTime && canSpawn)
        {
            SpawnFruit();
            timeElapsed = 0f;
            spawnTime = Random.Range(minSpawnTime, maxSpawnTime);
        }

        // Stop fruit spawn if the player has no health left and destroy the last ones
        playerHealth = playerController.healthPoints;
        if (playerHealth <= 0f)
        {
            canSpawn = false;
            minSpawnTime = minSpawnTimeInit;
            maxSpawnTime = maxSpawnTimeInit;
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
        // Détruit tous les enfants sauf la planète
        for (int i = 0; i < transform.childCount; i++)
            if (transform.GetChild(i).gameObject.name != "SM_PlanetStation")
            {
                Destroy(transform.GetChild(i).gameObject);
            }
       
    }

}
