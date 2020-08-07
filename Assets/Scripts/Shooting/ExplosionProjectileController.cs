using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionProjectileController : ProjectileController
{
    public float explosionRadius = 3f;
    private List<Transform> hitTargets = new List<Transform>();
    // Update is called once per frame
    private void Update()
    {
        if (targets.Count == 0 || targets[0] == null)
        {
            // Projectile has no target, no need to exist anymore.
            Destroy(gameObject);
            return;
        }

        SeekTargets();
    }

    // Seek your prey projectile like a shark.
    protected override void SeekTargets()
    {
        // Determine direction and distance to target.
        Vector3 targetDir = targets[0].position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;

        if (targetDir.magnitude <= distanceThisFrame)
        {
            // Close enough to target, explode and hit targets in range.
            GetTargetsInExplosion();
            HitTargets(hitTargets);
            return;
        }

        // Continue chasing target if not close enough.
        transform.Translate(targetDir.normalized * distanceThisFrame, Space.World);
    }

    // Get all targets that are caught in explosion.
    private void GetTargetsInExplosion()
    {
        UnityEngine.Debug.Log("GetTargetsInExplosion");
        // Add each target in explosion radius to list of hit targets.
        Collider2D[] targetsInExplosion = Physics2D.OverlapCircleAll(transform.position, explosionRadius, LayerMask.GetMask("Enemy"));
        foreach(Collider2D targetCollider in targetsInExplosion)
        {
            hitTargets.Add(targetCollider.transform);
        }
    }
}
