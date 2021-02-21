using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetSpawnerControllerLevel2 : MonoBehaviour
{
    [SerializeField] private Transform previousLevel = default;
    [SerializeField] private Transform currentLevel = default;
    [SerializeField] private GameObject targetPrefab = default;

    private bool _updatedEndLevel = false;
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
        _targetHeight = targetPrefab.GetComponent<Renderer>().bounds.size.y;

        GameObject empty = new GameObject {name = "Tmp"};
        empty.transform.parent = currentLevel;
    }
    
    private void Update()
    {
        if (previousLevel.childCount == 0 && _hasSpawn == false)
            LevelSpawns();

        if (currentLevel.childCount > 1)
        {
            if (_movingTargetRight != null) 
                MovingTargets(ref _movingTargetRight, ref _rightIsGoingUp);
            
            if (_movingTargetLeft != null) 
                MovingTargets(ref _movingTargetLeft, ref _leftIsGoingUp);
        }
        
        if (!_updatedEndLevel && _hasSpawn) 
            EndLevel.CurrentLevel = currentLevel;
        else
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
    
    private void MovingTargets(ref GameObject target, ref bool isGoingUp)
    {
        Vector3 pos = target.transform.position;

        // Limits verification
        if (pos.x <= -7f) isGoingUp = true;
        else if (pos.x >= 0f) isGoingUp = false;
        
        // Movement handling
        if (pos.x < 0f && isGoingUp == true)
            pos.Set(pos.x + _speed * Time.deltaTime, pos.y, pos.z);
        else if (pos.x > -7f && isGoingUp == false)
            pos.Set(pos.x - _speed * Time.deltaTime, pos.y, pos.z);
        
        // Assignation to reference
        target.transform.position = pos;
    }
    
}
