using UnityEngine;
using System.Linq;
using System.Collections;
public class OutLimitsRespawn : MonoBehaviour
{
    public float respawnDelay = 2f; // Tempo antes do respawn
    public GameObject Spawn;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) // Apenas os jogadores ativam o respawn
        {
            StartCoroutine(RespawnPlayer(collision.gameObject));

        }
        else if (collision.CompareTag("Beast")) // Apenas os jogadores ativam o respawn
        {
            StartCoroutine(RespawnPlayer(collision.gameObject));
            PlayerMovement playerMovement = collision.gameObject.GetComponent<PlayerMovement>();
        }
    }

    private IEnumerator RespawnPlayer(GameObject player)
    {
        player.SetActive(false); // Desativa o jogador temporariamente
        yield return new WaitForSeconds(respawnDelay);
        
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        GameObject alivePlayer = players.FirstOrDefault(p => p.activeSelf);

        if (alivePlayer != null)
        {
            player.transform.position = alivePlayer.transform.position;
            player.SetActive(true);
        }
        else
        {
           player.transform.position = Spawn.transform.position;
        }
        
        GameObject[] players1 = GameObject.FindGameObjectsWithTag("Beast");
        GameObject alivePlayer1 = players1.FirstOrDefault(p => p.activeSelf);

        if (alivePlayer1 != null)
        {
            player.transform.position = alivePlayer1.transform.position;
            player.SetActive(true);
        }
        else
        {
            player.transform.position = Spawn.transform.position;
        }

        
    }
}
