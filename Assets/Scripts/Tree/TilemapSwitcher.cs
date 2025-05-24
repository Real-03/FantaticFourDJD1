using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapSwitcher : MonoBehaviour
{
    public Tilemap tilemapToHide; // Tilemap que será escondido
    public Tilemap tilemapToShow; // Tilemap que será mostrado

    private void Start()
    {
        if (tilemapToShow != null)
        {
            tilemapToShow.gameObject.SetActive(false);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Thing"))
        {
            
            if (tilemapToHide != null)
            {
                Debug.Log("Desactive");
                tilemapToHide.gameObject.SetActive(false); // Esconde o primeiro tilemap
            }
            if (tilemapToShow != null)
            {
                Debug.Log("Active");
                tilemapToShow.gameObject.SetActive(true); // Mostra o segundo tilemap
            }
        }
    }
}