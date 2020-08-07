using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void OnHitTargets(List<Transform> targets, ProjectileController projectile);

public abstract class ProjectileController : MonoBehaviour
{
    [Header("Projectile attributes")]
    public float speed = 30f;
    public GameObject projectileImpact;
    protected OnHitTargets onHitTargets;
    protected List<Transform> targets = new List<Transform>();

    // Set target for projectile to attack.
    public void SetTargets(List<Transform> _targets)
    {
        targets = _targets;
    }

    // Set method to call when targets are hit.
    public virtual void SetOnHitTargets(OnHitTargets _onHitTargets)
    {
        onHitTargets = _onHitTargets;
    }

    // When reach/hit targets
    protected virtual void HitTargets(List<Transform> hitTargets)
    {
        UnityEngine.Debug.Log("onHitTargets");

        if (onHitTargets != null)
        {
            // Invoke on hit target behaviour requested by tower.
            if (projectileImpact)
            {
                GameObject effects = Instantiate(projectileImpact, transform.position, transform.rotation);
                Destroy(effects, 2f);
            }
            onHitTargets.Invoke(hitTargets, this);
            
        }
        else
        {
            // No on hit behaviour, just destroy yourself.
            Destroy(gameObject);
        }
    }

    // Seek out targets until they are hit.
    protected abstract void SeekTargets();
}
