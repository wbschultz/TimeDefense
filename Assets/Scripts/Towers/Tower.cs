using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower
{
    // Required settings to initialize tower.
    private TowerSchematic towerSchematic;
    private Transform towerTransform;
    // Tower Target information.
    private Transform currentTarget;
    public List<Transform> inRangeTargets = new List<Transform>();
    // Tower stats.
    public float towerRange { get { return towerSchematic.towerRange; } }
    public float towerFireRate { get { return towerSchematic.towerFireRate; } }

    /**
     * <param name="_towerSchematic">Tower schematic to determine type of tower.</param>
     * <param name="_towerTransform">Tower's transform.</param>
     * <param name="buildTower">Whether to play build tower effects.</param>
     */
    public Tower(TowerSchematic _towerSchematic, Transform _towerTransform, bool buildTower = false)
    {
        towerSchematic = _towerSchematic;
        towerTransform = _towerTransform;

        if (buildTower)
        {
            towerSchematic.BuildTower(towerTransform.gameObject);
        }
    }

    // Call when new target enters tower range.
    public void OnTargetEnterRange(Transform target)
    {
        if (!inRangeTargets.Contains(target))
        {
            // Only add target if not already added to in range list.
            inRangeTargets.Add(target);

            if (!currentTarget)
            {
                // If tower isn't targeting anyone yet, attack new target.
                currentTarget = target;
            }
        }
    }

    // Get tower's current target.
    public Transform GetCurrentTarget()
    {
        return currentTarget;
    }

    // Call when a target exits tower range.
    public void OnTargetExitRange(Transform target)
    {
        inRangeTargets.Remove(target);

        // Current target out of range, update current target;
        if (target == currentTarget)
        {
            GetNewTarget();
        }
    }

    // Have tower shoot target while spawning projecting at this location.
    public void ShootTarget(Transform target, Transform projectileSpawn)
    {
        towerSchematic.ShootTarget(target, projectileSpawn);
    }

    // Have tower choose new target from in range targets.
    private void GetNewTarget()
    {
        // Choose target based of tower types behaviour.
        Transform newTarget = towerSchematic.ChooseInRangeTarget(towerTransform, inRangeTargets);

        if (newTarget)
        {
            // Received target, shoot it.
            currentTarget = newTarget;
        }
        else
        {
            // No target in range, don't shoot.
            currentTarget = null;
        }
    }
}
