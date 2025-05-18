using System.Collections;
using UnityEngine;
using UnityEngine.Animations;

public class BeaconScript : MonoBehaviour
{
    [Header("Deteção de Jogadores")]
    public Transform detectPoint;
    public Vector2 detectSize = new Vector2(3f, 0.5f);
    public LayerMask playerLayer;

    [Header("Efeitos")]
    //public GameObject Beam;
    //public GameObject Beacon;
    public Animator animator;

    private bool isActivating = false;
    private bool Activated = false;

    void Update()
    {
        Collider2D[] players = Physics2D.OverlapBoxAll(detectPoint.position, detectSize, 0f, playerLayer);
        int playerCount = players.Length;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            BeaconActivator();
        }
    }

    private void BeaconActivator()
    {
        isActivating = true;

        if (animator != null)
        {
            animator.SetTrigger("isActivating");
            animator.SetBool("Activated", true);
            Activated = true;
        }

        // Desativa todos os colliders do Beacon
        foreach (BoxCollider2D col in GetComponents<BoxCollider2D>())
        {
            col.enabled = false;
        }
    }



    private void OnDrawGizmosSelected()
    {
        if (detectPoint == null) return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(detectPoint.position, detectSize);
    }
}
