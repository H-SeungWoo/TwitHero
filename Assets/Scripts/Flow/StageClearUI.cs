using UnityEngine;
using UnityEngine.SceneManagement;

public class StageClearUI : MonoBehaviour
{
    public GameObject stageClearPanel;

    public string nextSceneName;

    private bool isStageClear = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(stageClearPanel != null) stageClearPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (isStageClear) return;

        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        if (enemies.Length == 0)
        {
            ShowStageClear();
        }
    }

    void LateUpdate()
    {
        if (!isStageClear) return;
        if (Input.GetKeyDown(KeyCode.N))
        {
            Time.timeScale = 1f;

            if (!string.IsNullOrEmpty(nextSceneName))
                SceneManager.LoadScene(nextSceneName);
            else
            {
                Debug.Log("지정된 씬이 없습니다.");
            }
        }
    }

    public void ShowStageClear()
    {
        isStageClear = true;

        if(stageClearPanel != null) stageClearPanel.SetActive(true);

        Time.timeScale = 0f;
    }
}
