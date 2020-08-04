using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void OnHitTarget(Transform target, ProjectileController projectile);

public class ProjectileController : MonoBehaviour
{
    private Transform target;
    private OnHitTarget onHitTarget;

[Header("Projectile attributes")]
    public float speed = 30f;

    // Set target for projectile to attack.
    public void SetTarget(Transform _target)
    {
        target = _target;
    }

    public void SetOnHitTarget(OnHitTarget _onHitTarget)
    {
        onHitTarget = _onHitTarget;
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
        if (onHitTarget != null)
        {
            // Invoke on hit target behaviour requested by tower.
            onHitTarget.Invoke(target, this);
        }
        else
        {
            // No on hit behaviour, just destroy yourself.
            Destroy(gameObject);
        }
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
