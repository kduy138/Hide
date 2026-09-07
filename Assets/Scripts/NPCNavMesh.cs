using UnityEngine;
using UnityEngine.AI;

public class NPCNavMesh : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;
    [SerializeField]
    private Transform destination;

    private bool canMove = false;
    private Animator animator;

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        DialogueManager.instance.OnDialogueStarted += DialogueManager_OnDialogueStarted;
        DialogueManager.instance.OnDialogueEnded += DialogueManager_OnDialogueEnded;
    }

    private void DialogueManager_OnDialogueEnded(object sender, System.EventArgs e)
    {
        int currentDialogueSceneIdx = GameManager.instance.GetCurrentDialogueSceneIdx();

        switch (currentDialogueSceneIdx)
        {
            case (int)DialogueScene.SceneIndex.Scene_1:
                break;
            case (int)DialogueScene.SceneIndex.Scene_2:
                break;
            case (int)DialogueScene.SceneIndex.Scene_3:
                canMove = true;
                
                break;
        }
    }

    private void DialogueManager_OnDialogueStarted(object sender, System.EventArgs e)
    {
        int currentDialogueSceneIdx = GameManager.instance.GetCurrentDialogueSceneIdx();

        switch(currentDialogueSceneIdx)
        {
            case (int)DialogueScene.SceneIndex.Scene_1:
                break;
            case (int)DialogueScene.SceneIndex.Scene_2:
                break;
            case (int)DialogueScene.SceneIndex.Scene_3:
                break;
        }
    }

    private void Update()
    {
        if (!canMove) return;

        navMeshAgent.destination = destination.position;
        bool isMoving = navMeshAgent.velocity.magnitude > 0.1f;
        animator.SetBool("Move", isMoving);
    }
}
