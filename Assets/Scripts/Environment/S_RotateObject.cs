using UnityEngine;

public class S_RotateObject : MonoBehaviour
{

    public float rotationSpeed = 0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // Rotate object
        transform.Rotate(Vector3.left, rotationSpeed * Time.deltaTime);

    }
}
