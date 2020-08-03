using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BasicTowerSchematic", menuName = "TowerSchematics/Basic")]
public class BasicTowerSchematic : TowerSchematic
{
    [Header("Basic Tower Defaults")]
    public GameObject bulletPrefab;

    public override void ShootTarget(Transform target, Transform projectileSpawn)
    {
        if (projectileSpawn && target)
        {
            UnityEngine.Debug.Log("ShootTarget(): " + target.name);
            GameObject projectileGO = Instantiate(bulletPrefab, projectileSpawn.position, projectileSpawn.rotation);
            ProjectileController projectile = projectileGO.GetComponent<ProjectileController>();

            if (projectile)
            {
                projectile.SetTarget(target);
            }
        }
    }
}
