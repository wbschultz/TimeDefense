using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void OnHitTargets(List<Transform> targets, ProjectileController projectile);

public abstract class ProjectileController : MonoBehaviour
{
    protected List<Transform> targets = new List<Transform>();
    protected OnHitTargets onHitTargets;

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
    protected virtual void HitTargets()
    {
        UnityEngine.Debug.Log("onHitTargets");

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

    // Seek out targets until they are hit.
    protected abstract void SeekTargets();
}
