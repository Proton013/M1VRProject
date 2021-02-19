using System;
using UnityEngine;
using UnityEngine.Serialization;
using Object = UnityEngine.Object;

public class TargetHitController : MonoBehaviour
{
    [SerializeField] private int lifespan;

    public void TakeDamage()
    {
        // red effect
        Renderer renderer = gameObject.GetComponent<Renderer>();
        renderer.material.color = Color.red;
        
        lifespan--;
        if (lifespan <= 0)
        {
            Object.Destroy(transform.gameObject);
        }
    }
}