using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private Bootstrap bootstrap;

    public void ReloadScene()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void GoScene(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }

    public void Resume()
    {
        pauseMenu.SetActive(false);
        bootstrap.enabled = true;
        Time.timeScale = 1f;
    }

    public void Pause()
    {
        pauseMenu.SetActive(true);
        bootstrap.enabled = false;
        Time.timeScale = 0f;
    }
}
