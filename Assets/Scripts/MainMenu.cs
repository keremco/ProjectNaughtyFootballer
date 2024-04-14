using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] GameObject DeleteMessageDone;
    int bestScore;
    public void HouseScene()
    {
        SceneManager.LoadScene("House");
    }

    public void MuseumScene()
    {
        SceneManager.LoadScene("Museum");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ResetAll()
    {
        PlayerPrefs.DeleteAll();
        DeleteMessageDone.SetActive(true);
    }

}
