using UnityEngine;

public class Dash : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public KeyCode shootKey = KeyCode.E;   // Tecla para atirar
    void Start()
    {
        if (Input.GetKeyDown(shootKey))
        {
            DashHability();  
        }
    }

    // Update is called once per frame
    void DashHability()
    {

    }
}
