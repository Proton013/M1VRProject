using System;
using UnityEngine;
using UnityEngine.Serialization;
using Object = UnityEngine.Object;

public class TargetHitController : MonoBehaviour
{
    [SerializeField] private int lifespan;

    private Renderer _targetRenderer;
    private Color _defaultColor;

    private float _colorCount = 0f;
    private bool _hasTakenDamage = false;

    private void Start()
    {
        _targetRenderer = gameObject.transform.GetChild(0).GetComponent<Renderer>();
        _defaultColor = _targetRenderer.material.color;
    }

    private void Update()
    {
        if (_colorCount > 0.2f) // for 0.1s target's color is red
        {
            _targetRenderer.material.color = _defaultColor;
            _hasTakenDamage = false;
        }
        if (_hasTakenDamage) _colorCount += Time.deltaTime;
    }

    public void TakeDamage()
    {
        // Effect on hit
        _hasTakenDamage = true;
        _colorCount = 0f;
        _targetRenderer.material.color = Color.red;
        
        lifespan--;
        if (lifespan <= 0)
        {
            Object.Destroy(transform.gameObject);
        }
    }
}