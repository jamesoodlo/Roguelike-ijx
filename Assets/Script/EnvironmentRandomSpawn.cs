using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentRandomSpawn : MonoBehaviour
{
    public int numOfObstacles;
    public Transform[] spawnPoint;
    public Transform[] getSpawnPoint;
    public GameObject[] objPrefabs;

    void Start()
    {
        getSpawnPoint = GetRandomSubsetArray(spawnPoint, numOfObstacles);

        // Check if there are enough spawn points and object prefabs.
        if (getSpawnPoint.Length >= numOfObstacles && objPrefabs.Length > 0)
        {
            InstantiateObjectsAtRandomPoints(numOfObstacles);
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
            int randomPrefabIndex = Random.Range(0, objPrefabs.Length);
            GameObject instantiatedObject = Instantiate(objPrefabs[randomPrefabIndex], getSpawnPoint[i].position, Quaternion.identity);
        }
    }
}
