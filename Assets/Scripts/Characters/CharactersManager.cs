using System.Linq.Expressions;
using UnityEditor;
using UnityEngine;

public class CharactersManager : MonoBehaviour
{
    public static CharactersManager instance { get; private set; }

    [Header("Spawn points")]
    [SerializeField]
    private Transform evelynSpawnPoint;
    [SerializeField]
    private Transform hiroSpawnPoint;
    [SerializeField]
    private Transform raviSpawnPoint;

    [Header("Characters")]
    [SerializeField]
    private GameObject charactersContainer;
    [SerializeField]
    private GameObject evelynSitting;
    [SerializeField]
    private GameObject hiroSitting;
    [SerializeField]
    private GameObject raviSitting;
    [SerializeField]
    private GameObject evelyn;
    [SerializeField]
    private GameObject hiro;
    [SerializeField]
    private GameObject ravi;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        DialogueManager.instance.OnDialogueStarted += DialogueManager_OnDialogueStarted;
        DialogueManager.instance.OnDialogueEnded += DialogueManager_OnDialogueEnded;
    }

    private void DialogueManager_OnDialogueStarted(object sender, System.EventArgs e)
    {
        int currentDialogueSceneIdx = GameManager.instance.GetCurrentDialogueSceneIdx();

        switch (currentDialogueSceneIdx)
        {
            case (int)DialogueScene.SceneIndex.Scene_1:
                break;
            case (int)DialogueScene.SceneIndex.Scene_2:
                //SpawnCharacters(
                //        new CharacterSpawnData { prefab = evelyn, spawnPoint = evelynSpawnPoint },
                //        new CharacterSpawnData { prefab = hiro, spawnPoint = hiroSpawnPoint},
                //        new CharacterSpawnData { prefab = ravi, spawnPoint = raviSpawnPoint}
                //    );
                EnableCharacters(evelyn, hiro, ravi);
                break;
            case (int)DialogueScene.SceneIndex.Scene_3:

                break;
        }
    }

    private void DialogueManager_OnDialogueEnded(object sender, System.EventArgs e)
    {
        int currentDialogueSceneIdx = GameManager.instance.GetCurrentDialogueSceneIdx();

        switch (currentDialogueSceneIdx)
        {
            case (int)DialogueScene.SceneIndex.Scene_1:
                break;
            case (int)DialogueScene.SceneIndex.Scene_2:
                DestroyCharacters(evelynSitting, hiroSitting, raviSitting);
                break;
            case (int)DialogueScene.SceneIndex.Scene_3:
                break;
        }
    }

    private void DestroyCharacters(params GameObject[] destroyCharacters)
    {
       foreach (GameObject c in destroyCharacters)
        {
            if (c != null) Destroy(c);
        }
    }

    private void SpawnCharacters(params CharacterSpawnData[] spawnCharacters)
    {
        foreach (CharacterSpawnData data in spawnCharacters)
        {
            if (data.prefab != null) Spawn(data.prefab, data.spawnPoint);
        }
    }

    private void EnableCharacters(params GameObject[] enableCharacters)
    {
        foreach (GameObject c in enableCharacters)
        {
           if (c != null) c.SetActive(true);
        }
    }

    private void Spawn(GameObject prefab, Transform spawnPoint)
    {
        Instantiate(prefab, spawnPoint.position, spawnPoint.rotation,charactersContainer.transform);
    } 
}
