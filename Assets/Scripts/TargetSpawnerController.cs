using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using UnityEngine;

public class TargetSpawnerController : MonoBehaviour
{
    [SerializeField] private Transform previousLevel = default;
    [SerializeField] private Transform currentLevel = default;
    [SerializeField] private GameObject targetPrefab = default;
    [SerializeField] private Transform ProjectileParent = default;
    
    private bool _hasSpawn = false;
    private float _targetHeight;
    private Quaternion _rotation = Quaternion.Euler(-90, 0, 0); // adjust rotation from blender

    private void Awake()
    {
        EndLevel.CurrentLevel = currentLevel;
        EndLevel.ProjectileParent = ProjectileParent;
        _targetHeight = targetPrefab.GetComponentInChildren<Renderer>().bounds.size.y;

        GameObject empty = new GameObject {name = "Tmp"};
        empty.transform.parent = currentLevel;
    }
    
    private void Update()
    {
        if (previousLevel.childCount == 0 && _hasSpawn == false)
        {
            LevelSpawns();
        }

        EndLevel.CheckEnd(_hasSpawn);
    }

    private void LevelSpawns()
    {   // fixed targets (not moving)
        Vector3 position = new Vector3(-8.5f, _targetHeight/2 + 0.01f, 0);
        GameObject clone = Instantiate(targetPrefab, position, _rotation, currentLevel);
        clone.layer = LayerMask.NameToLayer("Target");
        
        position = new Vector3(-5f, _targetHeight/2 + 0.01f, 2);
        clone = Instantiate(targetPrefab, position, _rotation, currentLevel);
        clone.layer = LayerMask.NameToLayer("Target");
        
        position = new Vector3(-5f, _targetHeight/2 + 0.01f, -2);
        clone = Instantiate(targetPrefab, position, _rotation, currentLevel);
        clone.layer = LayerMask.NameToLayer("Target");

        _hasSpawn = true;
    }
    
}
