using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetSpawnerControllerLevel2 : MonoBehaviour
{
    [SerializeField] private Transform previousLevel = default;
    [SerializeField] private Transform currentLevel = default;
    [SerializeField] private GameObject targetPrefab = default;
    
    private bool _hasSpawn = false;
    private float _targetHeight;
    private Quaternion _rotation = Quaternion.Euler(-90, 0, 0); // adjust rotation from blender

    private GameObject _movingTargetRight;
    private GameObject _movingTargetLeft;
    private bool _rightIsGoingUp = true;
    private bool _leftIsGoingUp = false;

    private float _speed = 1f;

    private void Awake()
    {
        EndLevel.CurrentLevel = currentLevel;
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
        
        EndLevel.CheckEnd(_hasSpawn);
    }

    private void LevelSpawns()
    {
        // fixed
        Vector3 position = new Vector3(-8.5f, _targetHeight/2 + 0.01f, 0);
        GameObject clone = Instantiate(targetPrefab, position, _rotation, currentLevel);
        clone.layer = LayerMask.NameToLayer("Target");
        
        // moving front-back
        position = new Vector3(-7f, _targetHeight/2 + 0.01f, 2);
        _movingTargetRight = Instantiate(targetPrefab, position, _rotation, currentLevel);
        _movingTargetRight.layer = LayerMask.NameToLayer("Target");
        
        position = new Vector3(0f, _targetHeight/2 + 0.01f, -2);
        _movingTargetLeft = Instantiate(targetPrefab, position, _rotation, currentLevel);
        _movingTargetLeft.layer = LayerMask.NameToLayer("Target");
        
        _hasSpawn = true;
    }
    
    private void MovingTargets()
    {
        Vector3 right = _movingTargetRight.transform.position;
        Vector3 left = _movingTargetLeft.transform.position;

        // Limits verification
        if (right.x <= -7) _rightIsGoingUp = true;
        else if (right.x >= 0) _rightIsGoingUp = false;
        
        if (left.x <= -7) _leftIsGoingUp = true;
        else if (left.x >= 0) _leftIsGoingUp = false;
        
        // Movement handling
        if (right.x < 0 && _rightIsGoingUp == true)
            right.Set(right.x + _speed * Time.deltaTime, right.y, right.z);
        else if (right.x > -7 && _rightIsGoingUp == false)
            right.Set(right.x - _speed * Time.deltaTime, right.y, right.z);

        if (left.x < 0 && _leftIsGoingUp == true)
            left.Set(left.x + _speed * Time.deltaTime, left.y, left.z);
        else if (left.x > -7 && _leftIsGoingUp == false)
            left.Set(left.x - _speed * Time.deltaTime, left.y, left.z);
        
        // Assignation to reference
        _movingTargetRight.transform.position = right;
        _movingTargetLeft.transform.position = left;
    }
    
}
