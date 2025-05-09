using System.Collections;
using UnityEngine;

public class Dash : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private bool canDash = true;
    private bool isDashing = false;
    private float dashingPower = 100f;
    private float dashingTime = 1f;
    private float dashingCooldown = 1f;

    [SerializeField] private Rigidbody2D rb;


    public KeyCode shootKey = KeyCode.E;   // Tecla para dash
    void Start()
    {
    
    }
    void Update()
    {
        if (Input.GetKeyDown(shootKey))
        {
            Debug.Log("Key pressed ");
            DashHability();  
            
        }
    }

    // Update is called once per frame
    void DashHability()
    {
        Debug.Log("Dash Hability Start");
        if(canDash == true && isDashing == false)
            StartCoroutine(DashHabilityUse());
        else
            Debug.Log("You cant dash");
    }
    private IEnumerator DashHabilityUse()
    {
        Debug.Log("dash start");
        canDash = false;
        isDashing = true;
        Vector2 teste =  new Vector2(dashingPower,0f);
        rb.AddForce(teste, ForceMode2D.Force);
        Debug.Log("Velocity" +rb.linearVelocity.x);
        Debug.Log(dashingPower);
        
        yield return new WaitForSeconds(dashingTime);
        isDashing = false;
        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
        Debug.Log("dash end");
    }
}
