using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower
{
    private TowerSchematic towerSchematic;
    private Transform towerTransform;

    public Transform currentTarget;
    public List<Transform> inRangeTargets = new List<Transform>();
    public float towerRange { get { return towerSchematic.towerRange; } }
    public float towerFireRate { get { return towerSchematic.towerFireRate; } }

    public Tower(TowerSchematic _towerSchematic, Transform _towerTransform)
    {
        towerSchematic = _towerSchematic;
        towerTransform = _towerTransform;
    }

    public void OnTargetEnterRange(Transform target)
    {
        if (!inRangeTargets.Contains(target))
        {
            inRangeTargets.Add(target);

            if (!currentTarget)
            {
                currentTarget = target;
            }
        }
    }

    public void OnTargetExitRange(Transform target)
    {
        inRangeTargets.Remove(target);

        // Current target out of range, update current target;
        if (target == currentTarget)
        {
            GetNewTarget();
        }
    }

    public void ShootTarget(Transform target, Transform projectileSpawn)
    {
        towerSchematic.ShootTarget(target, projectileSpawn);
    }

    public bool Shoot()
    {
        if (currentTarget)
        {
            towerSchematic.ShootTarget(currentTarget, towerTransform);
            return true;
        }

        return false;
    }

    public void AquireTarget()
    {

    }

    public void GetNewTarget()
    {
        Transform newTarget = towerSchematic.GetInRangeTarget(towerTransform, inRangeTargets);

        if (newTarget)
        {
            currentTarget = newTarget;
        }
        else
        {
            currentTarget = null;
        }
    }

    public Transform GetCurrentTarget()
    {
        return currentTarget;
    }
}
