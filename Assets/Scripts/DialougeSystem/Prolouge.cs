using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class Prolouge : MonoBehaviour
{
    public event EventHandler OnPrologueStarted;
    public event EventHandler OnPrologueEnded;

    [Header("References")]
    [SerializeField]
    private TextMeshProUGUI prologueTxt;

    [Header("Settings")]
    [SerializeField]
    private float prolougeTextSpeed;

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

    private void Start()
    {
        GameInput.instance.OnPrologue += GameInput_OnPrologue;
        StartPrologue(prologueLines);
    }

    private void OnDestroy()
    {
        GameInput.instance.OnPrologue -= GameInput_OnPrologue;
    }

    private void GameInput_OnPrologue(object sender, EventArgs e)
    {
        if (isPrologueFinished) return;

        if (isTyping)
        {
            StopCoroutine(typingCoroutine);
            ShowEntirePrologue(prologueLines[currentIndex]);
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
                GameManager.instance.SetGameState(GameManager.State.Dialogue);
                OnPrologueEnded?.Invoke(this, EventArgs.Empty);
            }
        }
    }

    public void StartPrologue(DialogueLine[] newPrologueLines)
    {
        if (newPrologueLines == null || newPrologueLines.Length == 0) return;

        OnPrologueStarted?.Invoke(this, EventArgs.Empty);
        isPrologueFinished = false;
        prologueLines = newPrologueLines;
        currentIndex = 0;
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

    private void ShowEntirePrologue(DialogueLine dialogueLine)
    {
        prologueTxt.text = dialogueLine.dialougeTxt;
    }

    public bool IsPrologueFinished()
    {
        return isPrologueFinished;
    }
}
