using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TowerSchematic : ScriptableObject
{
    [Header("Tower Defaults")]
    public string towerName;
    public string towerDescription;
    public int towerCost;
    public int towerDamage;
    public float towerRange;
    public float towerFireRate;
    public Sprite towerSprite;
    public GameObject towerPrefab;

    /***************************************************************************
     * Required Tower behaviour to implement
     ***************************************************************************/
    public abstract void ShootTarget(Transform target, Transform projectileSpawn);


    /***************************************************************************
     * Default (Overridable) Tower behaviour
     **************************************************************************/
    // Default behaviour for build animation/effects for building tower.
    public virtual void BuildTower(GameObject towerGO)
    {
        if (towerGO)
        {
            // Just display default tower sprite for now.
            SpriteRenderer towerRenderer = towerGO.GetComponent<SpriteRenderer>();
            if (towerRenderer)
            {
                towerRenderer.sprite = towerSprite;
            }
        }
    }

    // Default behaviour for destroying tower animation/effects
    public virtual void DestroyTower(GameObject tower)
    {
        Destroy(tower);
    }

    // Default behaviour for targeting in range targets.
    public virtual Transform ChooseInRangeTarget(Transform tower, List<Transform> inRangeTargets)
    {
        if (inRangeTargets.Count > 0)
        {
            // Target first enemy to enter tower range.
            return inRangeTargets[0];
        }
        else
        {
            // No in range targets, no target.
            return null;
        }
    }

    // Default behaviour for determining if a target is in range.
    public virtual bool IsTargetInRange(Transform tower, Transform target, float range)
    {
        float targetDistance = GetTargetDistance(tower, target);

        if (targetDistance < range)
        {
            // Target is in range.
            return true;
        } else
        {
            // Target is out of range.
            return false;
        }
    }

    // Default behaviour to calculate distance from tower to target.
    public virtual float GetTargetDistance(Transform tower, Transform target)
    {
        if (tower && target)
        {
            return Vector3.Distance(tower.position, target.position);
        }

        // Invalid arguments return maximum distance.
        return Mathf.Infinity;
    }

    // Find closest in range target from list of targets.
    public Transform GetClosestInRangeTarget(Transform tower, List<Transform> targets, float range)
    {
        // Get closest target to tower and its distance.
        Transform closestTarget = GetClosestTarget(tower, targets);
        float closestDistance = GetTargetDistance(tower, closestTarget);


        if (IsTargetInRange(tower, closestTarget, range))
        {
            // Closest target is in range
            return closestTarget;
        } else
        {
            // No target is in range
            return null;
        }
    }

    // Find closest target to tower from list of targets.
    public Transform GetClosestTarget(Transform tower, List<Transform> targets)
    {
        Transform closestTarget = null;
        float closestTargetDistance = Mathf.Infinity;

        // Find closest target from target list.
        foreach (Transform target in targets)
        {
            float targetDistance = GetTargetDistance(tower, target);

            if (targetDistance < closestTargetDistance)
            {
                closestTargetDistance = targetDistance;
                closestTarget = target;
            }
        }

        return closestTarget;
    }

    // Calculate cost of building {amount} towers.
    public int TowerCost(int amount)
    {
        return towerCost * amount;
    }
}
