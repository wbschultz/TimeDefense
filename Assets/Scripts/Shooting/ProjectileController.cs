using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void OnHitTargets(List<Transform> targets, ProjectileController projectile);

public class ProjectileController : MonoBehaviour
{
    private List<Transform> targets = new List<Transform>();
    private OnHitTargets onHitTargets;

    [Header("Projectile attributes")]
    public float speed = 30f;

    // Set target for projectile to attack.
    public void SetTargets(List<Transform> _targets)
    {
        targets = _targets;
    }

    public void SetOnHitTargets(OnHitTargets _onHitTargets)
    {
        onHitTargets = _onHitTargets;
    }
    

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

    // Projectile hit target, it's a helluva thing.
    private void HitTargets()
    {
        if (onHitTargets != null)
        {
            // Invoke on hit target behaviour requested by tower.
            onHitTargets.Invoke(targets, this);
        }
        else
        {
            // No on hit behaviour, just destroy yourself.
            Destroy(gameObject);
        }
    }

    // Seek your prey projectile like a shark.
    private void SeekTargets()
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
