using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetSpawnerControllerLevel3 : MonoBehaviour
{
    [SerializeField] private Transform previousLevel = default;
    [SerializeField] private Transform currentLevel = default;
    [SerializeField] private GameObject targetPrefab = default;
    
    private bool _updatedEndLevel = false;
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
        {
            if (_movingTargetBack != null) 
                MovingTargets(ref _movingTargetBack, ref _backIsGoingPos);
            if (_movingTargetMid != null) 
                MovingTargets(ref _movingTargetMid, ref _midIsGoingPos);
            if (_movingTargetFront != null) 
                MovingTargets(ref _movingTargetFront, ref _frontIsGoingPos);
        }

        
        if (!_updatedEndLevel && _hasSpawn) 
            EndLevel.CurrentLevel = currentLevel;
        else
            EndLevel.CheckEnd(_hasSpawn);
    }

    private void LevelSpawns()
    {
        // All moving
        Vector3 position = new Vector3(-8, _targetHeight/2 + 0.01f, -2);
        _movingTargetBack = Instantiate(targetPrefab, position, _rotation, currentLevel);
        _movingTargetBack.layer = LayerMask.NameToLayer("Target");
        
        position = new Vector3(-4, _targetHeight/2 + 0.01f, 2);
        _movingTargetMid = Instantiate(targetPrefab, position, _rotation, currentLevel);
        _movingTargetMid.layer = LayerMask.NameToLayer("Target");
        
        position = new Vector3(0, _targetHeight/2 + 0.01f, 0);
        _movingTargetFront = Instantiate(targetPrefab, position, _rotation, currentLevel);
        _movingTargetFront.layer = LayerMask.NameToLayer("Target");
        
        _hasSpawn = true;
    }
    
    private void MovingTargets(ref GameObject target, ref bool isGoingUp)
    {
        Vector3 pos = target.transform.position;

        // Limits verification
        if (pos.z <= -2) isGoingUp = true;
        else if (pos.z >= 2) isGoingUp = false;
        
        // Movement handling
        if (pos.z < 2 && isGoingUp)
            pos.Set(pos.x, pos.y, pos.z + _speed * Time.deltaTime);
        else if (pos.z > -2 && !isGoingUp)
            pos.Set(pos.x, pos.y, pos.z - _speed * Time.deltaTime);
        
        // Assignation to reference
        target.transform.position = pos;
    }
    
}