using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Provides overrided tower defaults and behaviour for basic towers.
[CreateAssetMenu(fileName = "SingleShotTowerSchematic", menuName = "TowerSchematics/SingleShot")]
public class SingleShotTowerSchematic : TowerSchematic
{
    [Header("Basic Tower Defaults")]
    public GameObject projectilePrefab;

    // Basic tower specific functionality for shooting targets.
    public override void ShootTargets(List<Transform> targets, List<Transform> projectileSpawns)
    {
        if (projectileSpawns.Count > 0 && targets.Count > 0)
        {
            // Create tower's projectile.
            GameObject projectileGO = Instantiate(projectilePrefab, projectileSpawns[0].position, projectileSpawns[0].rotation);
            ProjectileController projectile = projectileGO.GetComponent<ProjectileController>();

            // Set target for projectile to hit.
            if (projectile)
            {
                projectile.SetTargets(targets);
                projectile.SetOnHitTargets(this.HitTargets);
            }
        }
    }

    public override void HitTargets(List<Transform> targets, ProjectileController projectile)
    {

        // Hit enemy with damage and status effects.
        if (targets.Count > 0)
        {
            Enemy enemy = targets[0].gameObject.GetComponent<Enemy>();
            int enemyHp = enemy.GotHit(this.towerDamage, this.statusEffect, statusDuration);
            UnityEngine.Debug.Log("HitTarget() hp: " + enemyHp);
            Destroy(projectile.gameObject);
        }
    }
}
