using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class TargetSpawner : MonoBehaviour
{
    [SerializeField] private GameObject targetPrefab;
    [SerializeField] private int targetNum;
    [SerializeField] private float spawnInterval;
    [SerializeField] private Transform targetDirection;
    [SerializeField] private List<bool> isTargetExistList;
    [SerializeField] private List<Transform> spawnPos;

    private void Awake()
    {
        isTargetExistList = Enumerable.Repeat(false, spawnPos.Count).ToList();
        StartCoroutine(StartSpawn());
    }

    IEnumerator StartSpawn()
    {
        while (targetNum > 0)
        {
            int index;
            if (IsAllTargetExist()) yield return null;
            do
            {
                index = Random.Range(0, spawnPos.Count);
            } while (isTargetExistList[index]);

            isTargetExistList[index] = true;
            Instantiate(targetPrefab, spawnPos[index].position, targetDirection.rotation, gameObject.transform);
            yield return new WaitForSeconds(spawnInterval);
            targetNum--;
        }
    }

    private bool IsAllTargetExist()
    {
        foreach (var isExist in isTargetExistList)
        {
            if (!isExist) return false;
        }

        return true;
    }
}
