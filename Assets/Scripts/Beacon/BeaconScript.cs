using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Animations;

public class BeaconScript : MonoBehaviour
{
    [Header("Animation Settings")]
    [SerializeField] private GameObject BeaconOff;
    [SerializeField] private GameObject BeaconOn;
    [SerializeField] private GameObject BeamOn;
    [SerializeField] private GameObject Beam; //Objeto do raio

    [Header("Beacon Detection System Settings")]
    [SerializeField] private Transform BeaconCheck;
    [SerializeField] private float BeaconCheckRadius = 2.0f;
    [SerializeField] private LayerMask PlayerLayer; //Layer do jogador
    private SpriteRenderer spriteRenderer;
    private bool isActivated = false;
    private bool isActivating = false;

    void Start()
    {
        
    }

    void Update()
    {
        ComputeBeaconState();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Beam.SetActive(true);
            Beam.SetActive(false);
            BeaconOff.SetActive(false);
            BeaconOn.SetActive(true);
        }
    }

    void ComputeBeaconState()
    {
        Collider2D collider = Physics2D.OverlapCircle(BeaconCheck.position, BeaconCheckRadius, PlayerLayer);
        isActivated = collider != null;
    }

    void OnDrawGizmos()
    {
        if (BeaconCheck == null) return;
        Gizmos.color = new Color(1, 0, 0, 0.5f);
        Gizmos.DrawWireSphere(BeaconCheck.position, BeaconCheckRadius);
    }
}
