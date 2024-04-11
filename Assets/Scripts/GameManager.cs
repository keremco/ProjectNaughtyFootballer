using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private int score = 0;
    public Text scoreCounter;
    public Text ballCounter;

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
        // Skoru güncelle ve metin nesnesine yansıt
        score += amount;
        if (scoreCounter != null)
        {
            scoreCounter.text = score.ToString();
        }
    }

    public void UpdateBall(int amount)
    {
        ballCounter.text = amount.ToString() + " ball left...";
    }
}