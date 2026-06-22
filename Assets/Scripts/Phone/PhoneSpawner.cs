using System.Collections.Generic;
using UnityEngine;

public class PhoneSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject phonePrefab;
    [SerializeField]
    private List<PhoneSpawnPoint> spawnPoints;

    private void Start()
    {
        SpawnPhone();
    }

    public void SpawnPhone()
    {
        if (spawnPoints == null || spawnPoints.Count == 0)
        {
            Debug.LogError("There is no spawn point in the scene!");
            return;
        }

        int randomInd = Random.Range(0, spawnPoints.Count);
        PhoneSpawnPoint selectedSpawnPoint = spawnPoints[randomInd];

        Instantiate(phonePrefab, selectedSpawnPoint.transform.position, selectedSpawnPoint.transform.rotation);
    }
}
