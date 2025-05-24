using UnityEngine;

public class GameData : MonoBehaviour
{
    [SerializeField] private float score;
    [SerializeField] private float activatedBeacons;

    public float GetScore() => score;
    
    public float SetScore(float value) => score+=value;

    public float ResetScore() => score=0;

    public float GetBeacons() => activatedBeacons;
    
    public float SetBeacons() => activatedBeacons++;

    public float ResetBeacons() => activatedBeacons=0;
    
    static GameData _instance;

    public static GameData Instance => _instance;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

}
