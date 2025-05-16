using UnityEngine;
using System.Collections;

public class S_ScreenShake : MonoBehaviour
{
    public AnimationCurve shakeCurve;
    public AnimationCurve FOVCurve;
    public GameObject player;
    public float duration = 0.5f;
    public bool isShaking = false;

    private void Start()
    {
    }
    private void Update()
    {
        if (isShaking)
        {
            isShaking = false;
            StartCoroutine(Shaking());
            StartCoroutine(FOVShake());
        }
    }

    IEnumerator Shaking()
    {
        Vector3 startPosition = transform.position;
        float elapsedTime = 0f;

        // Shake during duration
        while (elapsedTime< duration)
        {
            elapsedTime+= Time.deltaTime;
            float strength = shakeCurve.Evaluate(elapsedTime / duration);
            transform.position = startPosition + Random.insideUnitSphere * strength;
            yield return null;
        }

        transform.position = startPosition;
    }

    IEnumerator FOVShake()
    {
        Camera cam = gameObject.GetComponent<Camera>();
        float initFOV = cam.fieldOfView;
        float elapsedTime = 0f;

        // Change cam's FOV following the curve
        while (elapsedTime < FOVCurve[FOVCurve.length - 1].time)
        {
            elapsedTime += Time.deltaTime;
            cam.fieldOfView = FOVCurve.Evaluate(elapsedTime) * initFOV;
            yield return null;
        }

        cam.fieldOfView = initFOV;
    }
}
