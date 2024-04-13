using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
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

}
