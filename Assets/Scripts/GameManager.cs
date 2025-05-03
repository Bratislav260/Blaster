using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public GameObject gameOverScreen;
    [SerializeField] private Bootstrap bootstrap;


    public void Awake()
    {
        Instance = this;
    }

    public void GameReStart()
    {
        gameOverScreen.SetActive(false);
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void GameOver()
    {
        bootstrap.enabled = false;
        SoundSystem.Instance.StopLastPlayedMusics();
        Time.timeScale = 0;
        gameOverScreen.SetActive(true);
    }
}
