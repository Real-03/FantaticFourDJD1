using System;
using TMPro;
using UnityEngine;

public class UpdateScoreUI : MonoBehaviour
{
    [SerializeField]private TextMeshProUGUI scoreText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        scoreText = GetComponent<TextMeshProUGUI>();
    }

    public void ScoreChangeUI()
    {
        scoreText.text = GameData.score.ToString ();
    }
}
