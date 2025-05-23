using UnityEngine;

public class GameData : MonoBehaviour
{
    [SerializeField] private float score;

    public float GetScore() => score;
    
    public float SetScore(float value) => score+=value;
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
