using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    private Transform target;
    [Header("Projectile attributes")]
    public float speed = 30f;

    // Set target for projectile to attack.
    public void SetTarget(Transform _target)
    {
        target = _target;
    }

    // Update is called once per frame
    private void Update()
    {
        if (!target)
        {
            // Projectile has no target, no need to exist anymore.
            Destroy(gameObject);
            return;
        }

        SeekTarget();
    }

    // Projectile hit target, it's a helluva thing.
    private void HitTarget()
    {
        Destroy(gameObject);
    }

    // Seek your prey projectile like a shark.
    private void SeekTarget()
    {
        // Determine direction and distance to target.
        Vector3 targetDir = target.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;

        if (targetDir.magnitude <= distanceThisFrame)
        {
            // Close enough to target, count it as a hit.
            HitTarget();
            return;
        }

        // Continue chasing target if not close enough.
        transform.Translate(targetDir.normalized * distanceThisFrame, Space.World);
    }
}
