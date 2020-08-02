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

    public abstract void ShootTarget(Transform target);

    public virtual void BuildTower()
    {
        Instantiate(towerPrefab);
    }

    public virtual void BuildTower(Transform parent)
    {
        Instantiate(towerPrefab, parent);
    }

    public virtual void BuildTower(Vector3 position, Quaternion rotation)
    {
        Instantiate(towerPrefab, position, rotation);
    }

    public virtual void BuildTower(Vector3 position, Quaternion rotation, Transform parent)
    {
        Instantiate(towerPrefab, position, rotation, parent);
    }

    public virtual bool IsTargetInRange(Transform tower, Transform target)
    {
        // TODO
        return true;
    }

    public Transform GetClosestInRangeTarget(Transform tower, List<Transform> nearbyTargets)
    {
        // TODO
        return tower;
    }

    public int TowerCost(int amount)
    {
        return towerCost * amount;
    }
}
