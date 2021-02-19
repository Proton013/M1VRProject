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

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Target"))
        {
            _projectileTransform.gameObject.layer = LayerMask.NameToLayer("Default");
            TargetHitController hitTarget = other.gameObject.GetComponent<TargetHitController>();
            hitTarget.TakeDamage();
        }
    }
}