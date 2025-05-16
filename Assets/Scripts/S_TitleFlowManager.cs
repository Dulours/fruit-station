using UnityEngine;
using UnityEngine.SceneManagement;

public class S_TitleFlowManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Restart"))
        {
            SceneManager.LoadScene("Main_Scene");
        }
    }
}
