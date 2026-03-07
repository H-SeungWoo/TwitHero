using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class VNDialogue : MonoBehaviour
{
    [Header("UI")] 
    public Image characterImage;
    public TMP_Text nameText;
    public TMP_Text dialogueText;

    [Header("Dialogue Data")] public VNLine[] lines;

    [Header("Typing")] public float typingSpeed = 0.03f;

    [Header("Next Scene")] public VNSceneEndTrigger vnSceneEndTrigger;

    private int index = 0;
    private bool isTyping = false;
    private Coroutine typingCoroutine;
    
    
    void Start()
    {
        if (lines.Length > 0)
        {
            ShowLine();
        }        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && index < lines.Length) 
        {
            if (lines.Length == 0) return;
            if (isTyping)
            {
                CompleteCurrentLine();
            }
            else
            {
                index++;

                if (index == lines.Length)
                {
                    EndDialogue();
                }
                else
                {
                    ShowLine();
                }
            }
        }
    }

    void ShowLine()
    {
        VNLine line = lines[index];

        if (nameText != null) nameText.text = line.name;
        if (characterImage != null) characterImage.sprite = line.characterSprite;
        if (typingCoroutine != null)  StopCoroutine(typingCoroutine);
        typingCoroutine = StartCoroutine(TypeLine(line.dialogue));
    }

    private IEnumerator TypeLine(string text)
    {
        isTyping = true;
        dialogueText.text = "";

        foreach (char c in text)
        {
            dialogueText.text += c;
            yield return new WaitForSeconds(typingSpeed);
        }

        isTyping = false;
        typingCoroutine = null;
    }

    void CompleteCurrentLine()
    {
        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
        }

        dialogueText.text = lines[index].dialogue;
        isTyping = false;
        typingCoroutine = null;
    }

    void EndDialogue()
    {
        if (vnSceneEndTrigger != null)
        {
            vnSceneEndTrigger.OnVNFinished();
        }
    }
}
