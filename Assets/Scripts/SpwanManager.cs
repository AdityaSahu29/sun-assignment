using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpwanManager : MonoBehaviour
{
    public GameObject spherePrefab;
    public int minSphereCount = 5;
    public int maxSphereCount = 10;

    void Start()
    {
        int sphereCount = Random.Range(minSphereCount, maxSphereCount + 1);

        for (int i = 0; i < sphereCount; i++)
        {
            Vector3 randomPosition = new Vector3(Random.Range(-13f, 13f),Random.Range(-3f, 3f),0f);
            Instantiate(spherePrefab, randomPosition, Quaternion.identity);
        }
    }
}

