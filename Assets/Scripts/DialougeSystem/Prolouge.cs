using System;
using System.Collections;
using TMPro;
using UnityEngine;
using static UnityEditor.Rendering.MaterialUpgrader;

public class Prolouge : MonoBehaviour
{
    public event EventHandler OnPrologueStarted;
    public event EventHandler OnPrologueEnded;

    [Header("References")]
    [SerializeField]
    private TextMeshProUGUI prologueTxt;

    [Header("Settings")]
    [SerializeField]
    private float prolougeTextSpeed = 0.05f;

    [Header("Status")]
    [SerializeField]
    private bool isTyping = false;
    [SerializeField]
    private bool isPrologueFinished = true;

    [Header("Dialogue")]
    [SerializeField]
    private DialogueLine[] prologueLines;

    private Coroutine typingCoroutine;
    private int currentIndex = 0;
    private bool justStarted = false;

    private void Start()
    {
        GameInput.instance.OnDialogue += GameInput_OnDialogue;
        StartDialogue(prologueLines);
    }

    private void OnDestroy()
    {
        GameInput.instance.OnDialogue -= GameInput_OnDialogue;
    }

    private void GameInput_OnDialogue(object sender, EventArgs e)
    {
        if (justStarted)
        {
            justStarted = false;
        }

        if (isTyping)
        {
            StopCoroutine(typingCoroutine);
            ShowEntireDialogue(prologueLines[currentIndex]);
            isTyping = false;
        }
        else
        {
            currentIndex++;

            if (currentIndex < prologueLines.Length)
            {
                typingCoroutine = StartCoroutine(TypeLine(prologueLines[currentIndex]));
            }
            else
            {
                prologueTxt.text = "";
                isPrologueFinished = true;
                GameManager.instance.SetGameState(GameManager.State.GamePlaying);
                OnPrologueEnded?.Invoke(this, EventArgs.Empty);
            }
        }
    }

    public void StartDialogue(DialogueLine[] newPrologueLines)
    {
        OnPrologueStarted?.Invoke(this, EventArgs.Empty);
        isPrologueFinished = false;
        prologueLines = newPrologueLines;
        currentIndex = 0;
        justStarted = true;
        typingCoroutine = StartCoroutine(TypeLine(prologueLines[currentIndex]));
    }

    private IEnumerator TypeLine(DialogueLine prologueLine)
    {
        isTyping = true;

        prologueTxt.text = "";

        foreach (char c in prologueLine.dialougeTxt)
        {
            prologueTxt.text += c;
            yield return new WaitForSeconds(prolougeTextSpeed);
        }
        isTyping = false;
    }

    private void ShowEntireDialogue(DialogueLine dialogueLine)
    {
        prologueTxt.text = dialogueLine.dialougeTxt;
    }

    public bool IsDialogueFinished()
    {
        return isPrologueFinished;
    }
}
