using UnityEngine;

public class VNSceneEndTrigger : MonoBehaviour
{
    public SceneFlowManager sceneFlowManager;
    public GameObject nextPromptUI;

    private bool isVNFinished = false;

    public void OnVNFinished()
    {
        isVNFinished = true;

        if (nextPromptUI != null) nextPromptUI.SetActive(true);
    }

    void Update()
    {
        if (isVNFinished && Input.GetKeyDown(KeyCode.N))
        {
            sceneFlowManager.LoadNextScene();
        }
    }
}
