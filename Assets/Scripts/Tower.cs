using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower
{
    public Transform currentTarget;
    public List<Transform> nearbyTargets;

    private TowerSchematic towerSchematic;

    public Tower(TowerSchematic _towerSchematic)
    {
        towerSchematic = _towerSchematic;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTargetEnterRange(Transform target)
    {

    }

    public void OnTargetExitRange(Transform target)
    {

    }
}
