using UnityEngine;
using UnityEngine.Serialization;

public class TargetHitController : MonoBehaviour
{
    private Transform _target;
    
    [SerializeField] private int lifespan = 4;
    
    void Start()
    {
        _target = transform;
    }

    
    void Update()
    {
        if (lifespan <= 0)
        {
            // TODO : add effect
            GameObject.Destroy(_target.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("aaaaaaaaaaaaaaaaaaaaaaaaaaaaa");
        if (other.gameObject.layer == LayerMask.NameToLayer("Projectile"))
        {
            // TODO : add effect on hit (ex : red filter material)
            lifespan--;
            other.gameObject.layer = default; // used projectile won't hit (-1HP) on touch
        } 
    }
}