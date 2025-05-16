using UnityEngine;

public class S_AnimationController : MonoBehaviour
{
    public enum fruitProfile { orange, big, standard };
    public fruitProfile profile;
    private Animator animatorController;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animatorController = GetComponent<Animator>();

        // Set up animator booleans according to fruit type
        if (profile == fruitProfile.orange)
        {
            animatorController.SetBool("isOrange", true);
            animatorController.SetBool("isBig", false);
        }
        else if (profile == fruitProfile.big)
        {
            animatorController.SetBool("isOrange", false);
            animatorController.SetBool("isBig", true);
        }
        else
        {
            animatorController.SetBool("isOrange", false);
            animatorController.SetBool("isBig", false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
