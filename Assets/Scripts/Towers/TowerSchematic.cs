using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Provides default properties and behaviour for all towers.
public abstract class TowerSchematic : ScriptableObject
{
    [Header("Tower Specs")]
    public string towerName;
    public string towerDescription;
    public int towerCost;
    public int towerDamage;
    public float towerRange;
    public float towerFireRate;
    [Header("Tower Status Effects")]
    public float statusDuration;
    public StatusCondition statusEffect;
    [Header("Tower Appearance")]
    public Sprite towerSprite;
    public GameObject towerPrefab;
    [Header("Tower Upgrades")]
    public List<TowerSchematic> towerUpgrades;


    /***************************************************************************
     * Required Tower behaviour to implement
     ***************************************************************************/
    /**
     * Have tower shoot target.
     * <param name="target">Target for tower to shoot.</param>
     * <param name="projectileSpawn">Location to spawn projectile at.</param>
     */
    public abstract void ShootTargets(List<Transform> targets, List<Transform> projectileSpawns);

    public abstract void HitTargets(List<Transform> targets, ProjectileController projectile);

    /***************************************************************************
     * Default (Overridable) Tower behaviour
     **************************************************************************/
    // Default behaviour for build animation/effects for building tower.
    public virtual void BuildTower(GameObject towerGO)
    {
        if (towerGO)
        {
            // Just display default tower sprite for now.
            SpriteRenderer towerRenderer = towerGO.GetComponentInChildren<SpriteRenderer>();    // Find SpriteRenderer and update sprite
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

    // Play tower audio affect.
    public virtual void PlayTowerSound(AudioSource towerSound)
    {
        if (towerSound)
        {
            towerSound.Play();
        }
    }

    // Default behaviour for targeting in range targets.
    public virtual List<Transform> ChooseInRangeTargets(Transform tower, List<Transform> inRangeTargets)
    {
        if (inRangeTargets.Count > 0)
        {
            // Target first enemy to enter tower range.
            List<Transform> targets = new List<Transform>();
            targets.Add(inRangeTargets[0]);
            return targets;
        }
        else
        {
            // No in range targets, no target.
            return new List<Transform>();
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
    public virtual Transform GetClosestInRangeTarget(Transform tower, List<Transform> targets, float range)
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
    public virtual Transform GetClosestTarget(Transform tower, List<Transform> targets)
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

    /***************************************************************************
     * Default basic Tower behaviour
     **************************************************************************/
    // Calculate cost of building {amount} towers.
    public int TowerCost(int amount)
    {
        return towerCost * amount;
    }

    // Get tower prefab.
    public GameObject GetTowerPrefab()
    {
        return towerPrefab;
    }
}
