using UnityEngine;

public class ProjectileCollisionController : MonoBehaviour
{
    private Transform _projectileTransform;
    private Rigidbody _projectileRigidbody;

    private void Start()
    {
        _projectileTransform = transform;
        _projectileRigidbody = GetComponent<Rigidbody>();
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("ooo   ooo   ooo");
        if (other.gameObject.layer == LayerMask.NameToLayer("Target"))
        {
            // disappear after adding force to other
            
            _projectileRigidbody.isKinematic = true;
            // _projectileRigidbody.detectCollisions = false;
            _projectileTransform.parent = other.transform;
        }
    }
}