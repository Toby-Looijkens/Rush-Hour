using UnityEngine;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    [SerializeField] GameObject panel;
    [SerializeField] GameObject winScreen;
    [SerializeField] GameObject lossScreen;
    public void StartLevel(string level)
    {
        SceneManager.LoadScene(level);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void OpenWinScreen()
    {
        panel.SetActive(true);
        winScreen.SetActive(true);
    }

    public void OpenLossScreen()
    {
        panel.SetActive(true);
        lossScreen.SetActive(true);
    }
}
