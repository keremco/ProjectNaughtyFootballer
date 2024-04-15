using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;
    private int score = 0;
    private int bestScore = 0;
    private int lastScoreBall = 0;
    private int totalBall = 0;
    private int totalObjectLife = 0;

    [Header("In Game")]
    public Text ballCounter;
    public Text ObjectLife;
    public Text scoreCounter;

    [Header("In Game Menu")]
    public Text inGameMenuBestScore;

    [Header("In Finish Menu")]
    public Text finishMenuBallCounter;
    public Text finishMenuHitCounter;
    public Text finishMenuBestScore;
    public Text finishMenuScore;

    int ballLeft;
    bool gameOver;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

    }
    private void Start()
    {

        bestScore = PlayerPrefs.GetInt("househighscore", bestScore);
        
        inGameMenuBestScore.text = bestScore.ToString();
        finishMenuBestScore.text = bestScore.ToString();

    }

    private void Update()
    {
        if (score > bestScore && gameOver == true)
        {
            bestScore = score;
            inGameMenuBestScore.text = "" + score;
            finishMenuBestScore.text = "" + score;

            lastScoreBall = totalBall - ballLeft;
            
            PlayerPrefs.SetInt("houseHighscoreBall", lastScoreBall);
            PlayerPrefs.SetInt("househighscore", bestScore);
        }

        

    }

    public void UpdateScore(int amount)
    {
        score += amount;
        if (scoreCounter != null)
        {
            scoreCounter.text = score.ToString();
            finishMenuScore.text = score.ToString();
        }
    }

    public void UpdateBall(int amount)
    {
        ballCounter.text = amount.ToString();
        finishMenuBallCounter.text = amount.ToString();

        ballLeft = amount;

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
            finishMenuHitCounter.text = totalObjectLife.ToString();
        }

    }
    public void SetBallBegin(int amount) 
    {
        totalBall = amount;
    }

    public void GameOver(bool finish)
    {
        gameOver = finish;
    }
    public int gameStart() { return score;  }
    public int BallLeft() { return ballLeft; }
    public int ObjectLifeLeft() { return totalObjectLife; }

}