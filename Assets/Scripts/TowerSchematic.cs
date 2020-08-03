using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TowerSchematic : ScriptableObject
{
    public string towerName;
    public string towerDescription;
    public int towerCost;
    public int towerDamage;
    public float towerRange;
    public float towerFireRate;
    public GameObject towerPrefab;

    public abstract void ShootTarget(Transform target, Transform tower);

    public abstract Transform GetInRangeTarget(Transform tower, List<Transform> inRangeTargets);

    public virtual GameObject BuildTower()
    {
        return Instantiate(towerPrefab);
    }

    public virtual GameObject BuildTower(Transform parent)
    {
        return Instantiate(towerPrefab, parent);
    }

    public virtual GameObject BuildTower(Vector3 position, Quaternion rotation)
    {
        return Instantiate(towerPrefab, position, rotation);
    }

    public virtual GameObject BuildTower(Vector3 position, Quaternion rotation, Transform parent)
    {
        return Instantiate(towerPrefab, position, rotation, parent);
    }

    public virtual void DestroyTower(GameObject tower)
    {
        Destroy(tower);
    }

    public virtual bool IsTargetInRange(Transform tower, Transform target)
    {
        // TODO
        return true;
    }

    public Transform GetClosestInRangeTarget(Transform tower, List<Transform> inRangeTargets)
    {
        // TODO
        return tower;
    }

    public int TowerCost(int amount)
    {
        return towerCost * amount;
    }
}
