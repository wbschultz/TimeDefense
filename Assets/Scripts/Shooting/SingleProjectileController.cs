using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SingleProjectileController : ProjectileController
{
    [Header("Projectile attributes")]
    public float speed = 30f;

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
            // Close enough to target, count it as a hit.
            HitTargets();
            return;
        }

        // Continue chasing target if not close enough.
        transform.Translate(targetDir.normalized * distanceThisFrame, Space.World);
    }
}
