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
    [SerializeField] private List<Transform> spawnPos;
    [SerializeField] private List<Soldier> targetStats;

    // If the target is not shot and killed during this time, it will disappear automatically
    public float targetLifeTime;

    private void Awake()
    {
        if(targetNum > spawnPos.Count) Debug.LogError("target number must be less than or equal to spawn position size");
        targetStats = Enumerable.Repeat<Soldier>(null, spawnPos.Count).ToList();
        StartCoroutine(StartSpawn());
    }

    private void Update()
    {
        // Check if target is dead
        for (int i = 0; i < targetStats.Count; i++)
        {
            if(targetStats[i] == null) continue;

            if (targetStats[i].state.Equals(ETarget.Dead)) targetStats[i] = null;
        }
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
            } while (targetStats[index] != null);
            
            GameObject target = Instantiate(targetPrefab, spawnPos[index].position, targetDirection.rotation, gameObject.transform);
            targetStats[index] = target.GetComponent<Soldier>();
            Debug.Log("target index " + index + "  " +  targetStats[index]);
            targetStats[index].targetLifeTime = targetLifeTime;
            yield return new WaitForSeconds(spawnInterval);
            targetNum--;
        }
    }

    private bool IsAllTargetExist()
    {
        foreach (var targetStat in targetStats)
        {
            if (targetStats == null) return false;
        }
        return true;
    }
}
