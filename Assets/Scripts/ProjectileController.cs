using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    [SerializeField] private GameObject projectilePrefab = default;
    [SerializeField] private Transform projectileParent = default;
    [SerializeField] private GameObject tip = default;
    
    [SerializeField] private Transform projectileOrigin; // Typically, player torso or shoulder
    // if head : other process (meeting point between head ray and wand ray)
    [SerializeField] private float speedModifier = 1200;
    
    public void SpawnProjectiles()
    {
        Vector3 tipPos = tip.transform.position;
        // Vector3 projectileDirection = (tipPos - projectileOrigin.position).normalized; // calculate direction between tip and fixed point on player
        Vector3 projectileDirection = tip.transform.right;
            
        GameObject spawnedProjectile = Instantiate(
            projectilePrefab,
            tipPos,
            Quaternion.identity,
            projectileParent
        );
        spawnedProjectile.layer = LayerMask.NameToLayer("Projectile");
        spawnedProjectile.GetComponent<Rigidbody>().AddForce(projectileDirection * speedModifier);
    }
}
