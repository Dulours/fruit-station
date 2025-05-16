using UnityEngine;

public class S_RotateObject : MonoBehaviour
{

    public float rotationSpeed = 0f;
    public float interpolationSpeed;
    public float speedMultiplier = 1f;
    public float maxRotation = 50f;
    public bool canRotate = true;
    private float desiredRotationSpeed = 0f;
    private float initialRotationSpeed;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        initialRotationSpeed= rotationSpeed;
        desiredRotationSpeed = rotationSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if (canRotate)
        {
            desiredRotationSpeed = desiredRotationSpeed + (speedMultiplier * Time.deltaTime);

            // Lerp rotationSpeed value to new desired one
            rotationSpeed = Mathf.MoveTowards(rotationSpeed, desiredRotationSpeed, interpolationSpeed * Time.deltaTime);

            // Rotate object
            transform.Rotate(Vector3.left, rotationSpeed * Time.deltaTime);
        }


        else
        {
            desiredRotationSpeed = initialRotationSpeed;
            rotationSpeed = initialRotationSpeed;
        }
    }
}
