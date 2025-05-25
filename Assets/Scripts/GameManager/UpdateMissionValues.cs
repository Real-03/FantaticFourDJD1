using System;
using TMPro;
using UnityEngine;

public class UpdateMissionValues : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField]private TextMeshProUGUI scoreText;
    private GameData gameData;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        scoreText = GetComponent<TextMeshProUGUI>();
        gameData = FindFirstObjectByType<GameData>();
        MissionChangeUI();
    }

    public void MissionChangeUI()
    {
        scoreText.text = gameData.GetBeacons().ToString ()+ " / 6";
    }
}
