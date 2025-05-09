using UnityEngine;

public class Destroy : MonoBehaviour
{
    [SerializeField] private float time;
    private void Start()
    {
        
        Destroy(gameObject, time);
    }

    private void Update()
    {

    }
}
