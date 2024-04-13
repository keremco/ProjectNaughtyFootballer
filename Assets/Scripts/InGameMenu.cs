using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InGameMenu : MonoBehaviour
{
    [SerializeField] GameObject Panel;
    [SerializeField] GameObject inGameMenu;
    [SerializeField] GameObject FinishMenu;

    int ballLeft, objectLifeLeft;
    bool finished = false;
    bool ballLeftFinish= false;

    private float Cooldown = 2f;
    private float coolDownTimer;

    AudioManager audioManager;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    public void Update()
    {
        ballLeft = GameManager.instance.BallLeft();
        objectLifeLeft = GameManager.instance.ObjectLifeLeft();

        if (Input.GetKey("escape")) PauseOrFinish();

        if (ballLeft == 0 || objectLifeLeft == 0)
        {
            if (ballLeft == 0 && objectLifeLeft != 0) ballLeftFinish = true;
            finished = true;
            coolDownTimer = Cooldown;

            if (coolDownTimer > 0)
            {
                coolDownTimer -= Time.deltaTime;
            }

            if (coolDownTimer < 0)
            {
                coolDownTimer = 0;
                PauseOrFinish();
            }
            
        }
    }
    public void PauseOrFinish()
    {
        if (finished) { 
            FinishMenu.SetActive(true);
            if(ballLeftFinish) audioManager.PlaySFX(audioManager.ballFinish);
            else audioManager.PlaySFX(audioManager.finishGame);
        }
        else { 
            inGameMenu.SetActive(true); 
        }
        Panel.SetActive(true);
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

    }

    public void ToCancel()
    {
        inGameMenu.SetActive(false);
        Panel.SetActive(false);
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void ToRestart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ToMainMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void ExitToDesktop()
    {
        Application.Quit();
    }

}
