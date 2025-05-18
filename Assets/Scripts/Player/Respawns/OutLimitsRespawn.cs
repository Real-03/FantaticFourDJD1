using UnityEngine;

public class OutLimitsRespawn : MonoBehaviour
{
    // Layer que queremos detectar (defina no Inspector)
    public LayerMask respawnLayers;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Verifica se o layer do objeto está dentro do respawnLayers
        if (((1 << collision.gameObject.layer) & respawnLayers) != 0)
        {
            // Só continua se tiver o componente PlayerMovement
            if (collision.gameObject.GetComponent<PlayerMovement>() != null)
            {
                Respawn respawnScript = FindObjectOfType<Respawn>();
                if (respawnScript != null)
                {
                    StartCoroutine(respawnScript.RespawnPlayer(collision.gameObject));
                }
                else
                {
                    Debug.LogWarning("Script de Respawn não encontrado");
                }
            }
        }
    }
}