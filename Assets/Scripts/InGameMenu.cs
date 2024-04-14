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
    int finished = 0;
    int escapePress = 0;
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
        
        if (finished == 0)
        {
            if (Input.GetKeyDown("escape") && escapePress == 0)
            {
                escapePress = 1;
                Pause();
                
            }
            else if (Input.GetKeyDown("escape") && escapePress == 1)
            {
                escapePress = 0;
                ToCancel();
            }
        }

        if ((ballLeft == 0 || objectLifeLeft == 0) && Time.time > nextJob)
        {
            if (lateWork == 1) doneOrWhat();
            //Debug.Log("DoneOrWhat");
            nextJob = Time.time + jobDelay;
            lateWork = 1;
        }

    }
    
    public void Pause()
    {

        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Panel.SetActive(true);
        inGameMenu.SetActive(true);
    }

    public void ToCancel()
    {
        inGameMenu.SetActive(false);
        Panel.SetActive(false);
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void doneOrWhat()
    {

        finished = 1;
        if (gameStart == 0 || (ballLeft == 0 && objectLifeLeft > 0))
        {
            ballLeftFinish = true;
        }
        Finish();

        /*
        if (objectLifeLeft == 0 || ballLeft == 0)
        {
            
        }
        */
    }
    public void Finish()
    {

        escapePress = 1;
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Panel.SetActive(true);

        FootballerKidLogo.SetActive(false);
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
