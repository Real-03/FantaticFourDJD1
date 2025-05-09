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
        else if (collision.CompareTag("Thing")) // Apenas os jogadores ativam o respawn
        {
            StartCoroutine(RespawnPlayer(collision.gameObject));
            PlayerMovement playerMovement = collision.gameObject.GetComponent<PlayerMovement>();
        }
    }

    private IEnumerator RespawnPlayer(GameObject player)
    {
        player.SetActive(false);
    yield return new WaitForSeconds(respawnDelay);

    if (player.CompareTag("Player"))
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        GameObject alivePlayer = players.FirstOrDefault(p => p.activeSelf);

        if (alivePlayer != null)
        {
            player.transform.position = alivePlayer.transform.position;
        }
        else
        {
            player.transform.position = Spawn.transform.position;
        }

        player.SetActive(true);
    }
    else if (player.CompareTag("Thing"))
    {
        GameObject[] things = GameObject.FindGameObjectsWithTag("Thing");
        GameObject aliveThing = things.FirstOrDefault(p => p.activeSelf);

        if (aliveThing != null)
        {
            player.transform.position = aliveThing.transform.position;
        }
        else
        {
            player.transform.position = Spawn.transform.position;
        }

        player.SetActive(true);
    }
        
    }
}
