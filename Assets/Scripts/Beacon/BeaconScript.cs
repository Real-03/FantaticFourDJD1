using System.Collections;
using UnityEngine;
using UnityEngine.Animations;

public class BeaconScript : MonoBehaviour
{
    private Animation BeamAnim;
    private Animation BeaconAnim;
    public GameObject[] Beacons;

    public void OnStart()
    {
        BeamAnim = GetComponent<Animation>();
        BeaconAnim = GetComponent<Animation>();
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            StartCoroutine(BeamActive());
            BeaconAnim.Play();
        }
    }

    IEnumerator BeamActive()
    {
        yield return new WaitForSeconds(0.2f);
        BeamAnim.Play();
        yield return new WaitForSeconds(0.2f);
        BeamAnim.Stop();
    }
}
