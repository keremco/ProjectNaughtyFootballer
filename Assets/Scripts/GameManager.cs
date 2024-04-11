using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private int score = 0;
    private int totalObjectLife = 0;
    public Text scoreCounter;
    public Text ballCounter;
    public Text ObjectLife;

    void Awake()
    {
        // GameManager instance'ını ayarla
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    public void UpdateScore(int amount)
    {
        score += amount;
        if (scoreCounter != null)
        {
            scoreCounter.text = score.ToString();
        }
    }

    public void UpdateBall(int amount)
    {
        ballCounter.text = amount.ToString();
    }

    public void TotalObjectLife(int amount)
    {
        if(amount < 3 || amount == 0)
        {
            totalObjectLife--;
        }
        else if (amount == 3)
        {
            totalObjectLife += amount;
        }
        
        if (ObjectLife != null)
        {
            ObjectLife.text = totalObjectLife.ToString();
        }

    }
}