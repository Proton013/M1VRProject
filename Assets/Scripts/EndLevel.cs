using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EndLevel
{
    public static Transform CurrentLevel;
    public static Transform ProjectileParent;

    public static void CheckEnd(bool hasSpawn)
    {
        Debug.Log("name : " + CurrentLevel.name + " child count : " + CurrentLevel.childCount);
        // only Tmp object remaining, ready to go to next level
        if (hasSpawn && CurrentLevel.childCount == 1)
        {
            // destroy all projectiles
            foreach (Transform projectile in ProjectileParent)
            {
                Object.Destroy(projectile.gameObject);
            }
            
            foreach (Transform child in CurrentLevel)
            {
                Object.Destroy(child.gameObject);
            }
        }
    }
}
