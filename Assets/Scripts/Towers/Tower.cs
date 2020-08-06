using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower
{
    // Required settings to initialize tower.
    private TowerSchematic towerSchematic;
    private Transform towerTransform;
    // Tower Target information.
    private List<Transform> currentTargets = new List<Transform>();
    public List<Transform> inRangeEnemies = new List<Transform>();
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
        if (!inRangeEnemies.Contains(target))
        {
            // Only add target if not already added to in range list.
            inRangeEnemies.Add(target);

            if (currentTargets.Count == 0)
            {
                // If tower isn't targeting anyone yet, attack new target.
                GetNewTargets();
            }
        }
    }

    // Get tower's current target.
    public List<Transform> GetCurrentTargets()
    {
        return currentTargets;
    }

    // Call when a target exits tower range.
    public void OnTargetExitRange(Transform target)
    {
        inRangeEnemies.Remove(target);

        // Current target out of range, update current targets.
        if (currentTargets.Contains(target))
        {
            GetNewTargets();
        }
    }

    // Have tower shoot target while spawning projecting at this location.
    public void ShootTargets(List<Transform> targets, List<Transform> projectileSpawns)
    {
        towerSchematic.ShootTargets(targets, projectileSpawns);
    }

    // Have tower choose new target from in range targets.
    private void GetNewTargets()
    {
        // Choose target based of tower types behaviour.
        List<Transform> newTargets = towerSchematic.ChooseInRangeTargets(towerTransform, inRangeEnemies);
        currentTargets = newTargets;
    }
}
