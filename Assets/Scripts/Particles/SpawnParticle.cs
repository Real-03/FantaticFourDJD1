using UnityEngine;

public class SpawnParticle : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    /// <summary>
    /// Spawn Particles 
    /// Direction Default
    /// </summary>
    /// <param name="particle">GameObject of particle</param>
    /// <param name="particleSpawn">Transform to spawn particle</param>
    public void SpawnParticles(GameObject particle,Transform particleSpawn)
    {
        Vector3 rotation = GetRadius(particle.transform);
        Instantiate(particle,particleSpawn.position,Quaternion.Euler(rotation.x,rotation.y,rotation.z));
    }
    /// <summary>
    /// Spawn Particles 
    /// Directtion change on X axys
    /// </summary>
    /// <param name="particle">GameObject of particle</param>
    /// <param name="particleSpawn">Transform to spawn particle</param>
    /// <param name="direction">Float to tell direction</param>
    public void SpawnParticlesX(GameObject particle,Transform particleSpawn, float direction)
    {
        Vector3 rotation = GetRadius(particle.transform);
        Instantiate(particle,particleSpawn.position,Quaternion.Euler(rotation.x*direction,rotation.y,rotation.z));
    }
    /// <summary>
    /// Spawn Particles 
    /// Directtion change on Y axys
    /// </summary>
    /// <param name="particle">GameObject of particle</param>
    /// <param name="particleSpawn">Transform to spawn particle</param>
    /// <param name="direction">Float to tell direction</param>
    public void SpawnParticlesY(GameObject particle,Transform particleSpawn, float direction)
    {
        Vector3 rotation = GetRadius(particle.transform);
        Instantiate(particle,particleSpawn.position,Quaternion.Euler(rotation.x,rotation.y*direction,rotation.z));
    }
    
    /// <summary>
    /// Method to get radius from one transform
    /// </summary>
    /// <param name="transform">Transform to get radius</param>
    /// <returns></returns>
    private static Vector3 GetRadius(Transform transform)
    {
        return transform.rotation.eulerAngles;
    }
}
