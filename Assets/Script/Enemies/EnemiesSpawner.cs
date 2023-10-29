using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesSpawner : MonoBehaviour
{
    public StageData stageData;
    public int numOfEnemies;
    public Transform[] spawnPoint;
    public Transform[] getSpawnPoint;
    public GameObject[] enemiesPrefabs;

    void Start()
    {
        numOfEnemies = stageData.currentStage;

        getSpawnPoint = GetRandomSubsetArray(spawnPoint, numOfEnemies);

        if (getSpawnPoint.Length >= numOfEnemies && enemiesPrefabs.Length > 0)
        {
            InstantiateObjectsAtRandomPoints(numOfEnemies);
        }
        else
        {
            Debug.LogWarning("Not enough spawn points or no object prefabs to instantiate.");
        }
    }

    void Update()
    {

    }

    private T[] GetRandomSubsetArray<T>(T[] sourceArray, int subsetSize)
    {
        if (subsetSize >= sourceArray.Length)
            return sourceArray;

        T[] randomSubset = new T[subsetSize];
        T[] remainingItems = (T[])sourceArray.Clone();

        for (int i = 0; i < subsetSize; i++)
        {
            int randomIndex = Random.Range(0, remainingItems.Length);
            randomSubset[i] = remainingItems[randomIndex];

            // Remove the selected item from the remaining items.
            remainingItems[randomIndex] = remainingItems[remainingItems.Length - 1];
            System.Array.Resize(ref remainingItems, remainingItems.Length - 1);
        }

        return randomSubset;
    }

    private void InstantiateObjectsAtRandomPoints(int numberOfObjects)
    {
        for (int i = 0; i < numberOfObjects; i++)
        {
            int randomPrefabIndex = Random.Range(0, enemiesPrefabs.Length);
            GameObject instantiatedObject = Instantiate(enemiesPrefabs[randomPrefabIndex], getSpawnPoint[i].position, Quaternion.identity);
        }
    }
}
