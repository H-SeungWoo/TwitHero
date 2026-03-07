using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverUI : MonoBehaviour
{
    public GameObject gameOverPanel;

    private bool isGameOver = false;

    void Start()
    {
        if (gameOverPanel != null) gameOverPanel.SetActive(false);
    }

    void Update()
    {
        if (!isGameOver) return;

        if (Input.GetKeyDown(KeyCode.R))
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    public void ShowGameOver()
    {
        isGameOver = true;

        if(gameOverPanel!=null) gameOverPanel.SetActive(true);
        Time.timeScale = 0f;
    }
}
