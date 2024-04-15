using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InGameMenu : MonoBehaviour
{
    [SerializeField] GameObject Panel;
    [SerializeField] GameObject inGameMenu;
    [SerializeField] GameObject FinishMenu;

    [SerializeField] GameObject BallLeftMessage;
    [SerializeField] GameObject GoodJobMessage;
    [SerializeField] GameObject FootballerKidLogo;

    int ballLeft, objectLifeLeft;
    
    bool ballLeftFinish = false;
    bool finished = false;
    bool escapePress = false;
    int gameStart = 0;
    AudioManager audioManager;

    private float nextJob = 1f;
    float jobDelay = 2f;
    int lateWork = 0;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    
    private void Start()
    {
        ballLeft = 1;
        objectLifeLeft = 1;
    }
    

    public void Update()
    {
        ballLeft = GameManager.instance.BallLeft();
        objectLifeLeft = GameManager.instance.ObjectLifeLeft();
        gameStart = GameManager.instance.gameStart();

        

        if (finished == false)
        {
            if (Input.GetKeyDown("escape") && escapePress == false)
            {
                escapePress = true;
                PauseOrFinish();
            }
            else if (Input.GetKeyDown("escape") && escapePress == true)
            {
                escapePress = false;
                ToCancel();

            }
        }

        if ((ballLeft == 0 || objectLifeLeft == 0) && Time.time > nextJob)
        {
            if (lateWork == 1)
            {
                doneOrWhat();
            }
            nextJob = Time.time + jobDelay;
            lateWork = 1;
        }

    }

    public void ToCancel()
    {
        if (Input.GetKeyDown("escape") && escapePress == false && finished == false)
        {
            inGameMenu.SetActive(false);
            Panel.SetActive(false);
        }
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void doneOrWhat()
    {
        if (gameStart == 0 || (ballLeft == 0 && objectLifeLeft > 0))
        {
            ballLeftFinish = true;
        }
        finished = true;
        PauseOrFinish();
    }
    public void PauseOrFinish()
    { 
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        

        if(Input.GetKeyDown("escape") && escapePress == true && finished == false)
        {
            Panel.SetActive(true);
            inGameMenu.SetActive(true);
        } 
        else if (finished == true)
        {
            FootballerKidLogo.SetActive(false);
            Panel.SetActive(true);
            FinishMenu.SetActive(true);

            if (ballLeftFinish)
            {
                BallLeftMessage.SetActive(true);
                audioManager.PlaySFX(audioManager.ballFinish);
            }
            else
            {
                GoodJobMessage.SetActive(true);
                audioManager.PlaySFX(audioManager.finishGame);
                GameManager.instance.GameOver(true);
            }
        }
        
    }

    public void ToRestart()
    {
        lateWork = 0;
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ToMainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Menu");
        
    }

    public void ExitToDesktop()
    {
        Application.Quit();
    }

}
