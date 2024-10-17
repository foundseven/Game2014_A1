using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance {  get; set; }
    [SerializeField]
    TextMeshProUGUI _scoreText;

    int score = 0;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        _scoreText.text = "Score: " + score;
    }
    public void ChangeScore(int amount)
    {
        score += amount;
        UpdateScoreUI();
    }
    public void DecreaseScore(int amount)
    {
        score -= amount;
        UpdateScoreUI();
    }

    public void UpdateScoreUI()
    {
        _scoreText.text = "Score: " + score;
    }

}
