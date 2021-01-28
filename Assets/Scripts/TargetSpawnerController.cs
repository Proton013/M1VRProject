using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetSpawnerController : MonoBehaviour
{
    [SerializeField] private Transform previousLevel = default;
    [SerializeField] private Transform currentLevel = default;
    [SerializeField] private GameObject targetPrefab = default;
    
    private bool _hasSpawn = false;
    private float _targetHeight;
    private Quaternion _rotation = Quaternion.Euler(-90, 0, 0); // adjust rotation from blender

    private void Awake()
    {
        _targetHeight = targetPrefab.GetComponent<Renderer>().bounds.size.y;
        
        GameObject empty = new GameObject();
        empty.name = "Tmp";
        empty.transform.parent = currentLevel;
    }
    
    private void Update()
    {
        if (previousLevel.childCount == 0 && _hasSpawn == false)
        {
            LevelSpawns();
        }
    }

    private void LevelSpawns()
    {   // fixed targets (not moving)
        Vector3 position = new Vector3(-8.5f, _targetHeight/2 + 0.01f, 0);
        GameObject clone = Instantiate(targetPrefab, position, _rotation, currentLevel);
        clone.AddComponent<Rigidbody>(); clone.AddComponent<BoxCollider>();
        
        position = new Vector3(-5f, _targetHeight/2 + 0.01f, 2);
        clone = Instantiate(targetPrefab, position, _rotation, currentLevel);
        clone.AddComponent<Rigidbody>(); clone.AddComponent<BoxCollider>();
        
        position = new Vector3(-5f, _targetHeight/2 + 0.01f, -2);
        clone = Instantiate(targetPrefab, position, _rotation, currentLevel);
        clone.AddComponent<Rigidbody>(); clone.AddComponent<BoxCollider>();

        _hasSpawn = true;
    }
}
