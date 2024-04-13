using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;
    private int score = 0;
    private int bestScore = 0;
    private int totalObjectLife = 0;

    [Header("In Game")]
    public Text ballCounter;
    public Text ObjectLife;
    public Text scoreCounter;

    [Header("In Game Menu")]
    public Text inGameMenuScoreCounter;
    public Text inGameMenuBestScore;

    [Header("In Finish Menu")]
    public Text finishMenuBallCounter;
    public Text finishMenuHitCounter;
    public Text finishMenuBestScore;
    public Text finishMenuScore;

    int ballLeft, objectLifeLeft;


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
        if (score > bestScore)
        {
            bestScore = score;
            inGameMenuBestScore.text = "" + score;
            finishMenuBestScore.text = "" + score;

            PlayerPrefs.SetInt("househighscore", bestScore);
        }
    }

    /*
    void OnDestroy()
    {
        PlayerPrefs.SetInt("househighscore", bestScore);
        PlayerPrefs.Save();
    }
    */

    public void UpdateScore(int amount)
    {
        score += amount;
        if (scoreCounter != null)
        {
            scoreCounter.text = score.ToString();
            inGameMenuScoreCounter.text = score.ToString();
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
            objectLifeLeft = amount;
        }

    }

    public int BallLeft() { return ballLeft; }
    public int ObjectLifeLeft() { return objectLifeLeft; }

}