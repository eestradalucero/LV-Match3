using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreMockup : MonoBehaviour
{
    [SerializeField] Text scoreText;
    private int totalScore;

    public void AddScore(int score, int team){
        var multiplier = team % 2 == 0 ? 1 : -1;
        totalScore += score * multiplier;
        scoreText.text = totalScore.ToString("0"); 
    }

}
