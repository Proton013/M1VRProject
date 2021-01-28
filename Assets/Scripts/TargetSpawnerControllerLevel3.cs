using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetSpawnerControllerLevel3 : MonoBehaviour
{
    [SerializeField] private Transform previousLevel = default;
    [SerializeField] private Transform currentLevel = default;
    [SerializeField] private GameObject targetPrefab = default;
    
    private bool _hasSpawn = false;
    private float _targetHeight;
    private Quaternion _rotation = Quaternion.Euler(-90, 0, 0); // adjust rotation from blender

    private GameObject _movingTargetBack;
    private GameObject _movingTargetMid;
    private GameObject _movingTargetFront;
    private bool _backIsGoingPos = true;
    private bool _midIsGoingPos = true;
    private bool _frontIsGoingPos = true;

    private float _speed = 1f;

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
            LevelSpawns();
        
        if (currentLevel.childCount > 1)
            MovingTargets();
    }

    private void LevelSpawns()
    {
        // All moving
        Vector3 position = new Vector3(-8, _targetHeight/2 + 0.01f, -2);
        _movingTargetBack = Instantiate(targetPrefab, position, _rotation, currentLevel);
        _movingTargetBack.AddComponent<Rigidbody>(); _movingTargetBack.AddComponent<BoxCollider>();
        
        position = new Vector3(-4, _targetHeight/2 + 0.01f, 2);
        _movingTargetMid = Instantiate(targetPrefab, position, _rotation, currentLevel);
        _movingTargetMid.AddComponent<Rigidbody>(); _movingTargetMid.AddComponent<BoxCollider>();
        
        position = new Vector3(0, _targetHeight/2 + 0.01f, 0);
        _movingTargetFront = Instantiate(targetPrefab, position, _rotation, currentLevel);
        _movingTargetFront.AddComponent<Rigidbody>(); _movingTargetFront.AddComponent<BoxCollider>();
        
        _hasSpawn = true;
    }
    
    private void MovingTargets()
    {
        Vector3 back = _movingTargetBack.transform.position;
        Vector3 mid = _movingTargetMid.transform.position;
        Vector3 front = _movingTargetFront.transform.position;

        // Limits verification
        if (back.z <= -2) _backIsGoingPos = true;
        else if (back.z >= 2) _backIsGoingPos = false;
        
        if (mid.z <= -2) _midIsGoingPos = true;
        else if (mid.z >= 2) _midIsGoingPos = false;
        
        if (front.z <= -2) _frontIsGoingPos = true;
        else if (front.z >= 2) _frontIsGoingPos = false;
        
        // Movement handling
        if (back.z < 2 && _backIsGoingPos)
            back.Set(back.x, back.y, back.z + _speed * Time.deltaTime);
        else if (back.z > -2 && !_backIsGoingPos)
            back.Set(back.x, back.y, back.z - _speed * Time.deltaTime);

        if (mid.z < 2 && _midIsGoingPos)
            mid.Set(mid.x, mid.y, mid.z + _speed * Time.deltaTime);
        else if (mid.z > -2 && !_midIsGoingPos)
            mid.Set(mid.x, mid.y, mid.z - _speed * Time.deltaTime);
        
        if (front.z < 2 && _frontIsGoingPos)
            front.Set(front.x, front.y, front.z + _speed * Time.deltaTime);
        else if (front.z > -2 && !_frontIsGoingPos)
            front.Set(front.x, front.y, front.z - _speed * Time.deltaTime);
        
        // Assignation to reference
        _movingTargetBack.transform.position = back;
        _movingTargetMid.transform.position = mid;
        _movingTargetFront.transform.position = front;
    }
}