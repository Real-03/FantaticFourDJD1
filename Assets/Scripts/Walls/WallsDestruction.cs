using UnityEngine;
using System.Collections;
using UnityEngine.Tilemaps;
public class WallsDestruction : MonoBehaviour
{
    [SerializeField] private Tilemap tilemap; // Referência ao Tilemap
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Colisão detectada com: " + collision.gameObject.name);
        
        PlayerMovement playerMovement = collision.gameObject.GetComponent<PlayerMovement>();
        if (playerMovement != null)
        {
            ContactPoint2D contact = collision.contacts[0]; // Obtém o ponto de contato
            Vector3Int tilePosition = tilemap.WorldToCell(contact.point);

            Destroy(tilemap.gameObject);
            
        }   
    }
}
