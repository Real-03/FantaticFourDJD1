using System;
using TMPro;
using UnityEngine;

public class UpdateScoreUI : MonoBehaviour
{
    [SerializeField]private TextMeshProUGUI scoreText;
    private GameData gameData;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        scoreText = GetComponent<TextMeshProUGUI>();
        gameData = FindFirstObjectByType<GameData>();
        ScoreChangeUI();
    }

    public void ScoreChangeUI()
    {
        scoreText.text = gameData.GetScore().ToString ()+ " PTS";
    }
}
