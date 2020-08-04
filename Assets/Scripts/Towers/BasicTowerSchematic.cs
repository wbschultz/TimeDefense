using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Provides overrided tower defaults and behaviour for basic towers.
[CreateAssetMenu(fileName = "BasicTowerSchematic", menuName = "TowerSchematics/Basic")]
public class BasicTowerSchematic : TowerSchematic
{
    [Header("Basic Tower Defaults")]
    public GameObject projectilePrefab;

    // Basic tower specific functionality for shooting targets.
    public override void ShootTarget(Transform target, Transform projectileSpawn)
    {
        if (projectileSpawn && target)
        {
            // Create tower's projectile.
            GameObject projectileGO = Instantiate(projectilePrefab, projectileSpawn.position, projectileSpawn.rotation);
            ProjectileController projectile = projectileGO.GetComponent<ProjectileController>();

            // Set target for projectile to hit.
            if (projectile)
            {
                projectile.SetTarget(target);
                projectile.SetOnHitTarget(this.HitTarget);
            }
        }
    }

    public override void HitTarget(Transform target, ProjectileController projectile)
    {

        // Hit enemy with damage and status effects.
        Enemy enemy = target.gameObject.GetComponent<Enemy>();
        int enemyHp = enemy.GotHit(this.towerDamage, this.statusEffect, statusDuration);
        UnityEngine.Debug.Log("HitTarget() hp: " + enemyHp);
        Destroy(projectile.gameObject);

    }
}
