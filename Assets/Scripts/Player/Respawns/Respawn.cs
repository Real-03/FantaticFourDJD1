using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Respawn : MonoBehaviour
{
    public float respawnDelay = 3f;

    public IEnumerator RespawnPlayer(GameObject player)
    {
        yield return new WaitForSeconds(0.5f);
        SpriteRenderer sprite = player.GetComponent<SpriteRenderer>();
        Collider2D col = player.GetComponent<Collider2D>();
        PlayerMovement move = player.GetComponent<PlayerMovement>();

        if (sprite != null) sprite.enabled = false;
        if (col != null) col.enabled = false;
        if (move != null) move.enabled = false;

        yield return new WaitForSeconds(respawnDelay);

        // Verifica todos os jogadores ativos com o componente PlayerMovement
        PlayerMovement[] allPlayers = FindObjectsOfType<PlayerMovement>();
        PlayerMovement alivePlayer = allPlayers.FirstOrDefault(p =>
                                                                p.gameObject.activeSelf &&
                                                                p.gameObject != player &&
                                                                p.GetComponent<SpriteRenderer>() != null &&
                                                                p.GetComponent<SpriteRenderer>().enabled
                                                            );
        Debug.Log(allPlayers);
        Debug.Log(alivePlayer);
        if (alivePlayer != null)
        {
            player.transform.position = alivePlayer.transform.position;

            if (sprite != null) sprite.enabled = true;
            if (col != null) col.enabled = true;
            if (move != null) move.enabled = true;
            PlayerHealth health = player.GetComponent<PlayerHealth>();
            if (health != null)
            {
                health.ResetHealth();
            }
            
        }
        else
        {
            // Nenhum jogador vivo encontrado → mudar para a cena de Game Over
            SceneManager.LoadScene("GameOverScreen"); // Substitui com o nome real da sua cena
        }


    }
    
}
