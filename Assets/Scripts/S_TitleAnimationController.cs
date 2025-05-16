using UnityEngine;

public class S_TitleAnimationController : MonoBehaviour
{
    public enum textProfile { main, sub };
    public textProfile profile;
    private Animator animatorController;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animatorController = GetComponent<Animator>();

        // Set up animator booleans according to fruit type
        if (profile == textProfile.main)
        {
            animatorController.SetBool("isMainTitle", true);
        }
        else 
        {
            animatorController.SetBool("isMainTitle", false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
