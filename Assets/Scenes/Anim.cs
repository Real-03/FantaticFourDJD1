using UnityEngine;

public class Anim : MonoBehaviour
{
    [SerializeField]
    private Animator anim;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            anim.SetTrigger("Attack");
        }
        if(Input.GetKeyDown(KeyCode.LeftControl))
        {
            anim.SetBool("crouch", true);
        }
        if(Input.GetKeyUp(KeyCode.LeftControl))
        {
            anim.SetBool("crouch", false);
        }
        
    }
}
