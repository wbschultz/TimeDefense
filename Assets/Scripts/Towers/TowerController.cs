using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerController : MonoBehaviour
{
    public bool buildTower = false;
    public TowerSchematic towerSchematic;
    private Tower tower;
    private Transform projectileSpawn;

    private float shootTimer = 0f;
    private float shootInterval;

    // Start is called before the first frame update
    void Start()
    {
        // Initialize shoot timer and create tower of towerSchematic type
        shootTimer = shootInterval = 1 / towerSchematic.towerFireRate;
        if (towerSchematic)
        {
            tower = new Tower(towerSchematic, transform, buildTower);
        }

        // Get tower projectile spawn location from first child
        if (transform.childCount > 0) projectileSpawn = transform.GetChild(0);

        // Setup collider dimensions using schematic range
        CircleCollider2D towerRange = GetComponent<CircleCollider2D>();
        if (towerRange)
        {
            towerRange.radius = tower.towerRange;
        }
    }

    // Update is called once per frame
    void Update()
    {
        ShootIfTargetInRange();
        shootTimer += Time.deltaTime;
    }

    private void ShootIfTargetInRange()
    {
        Transform currentTarget = tower.GetCurrentTarget();

        if (currentTarget && shootTimer >= shootInterval)
        {
            tower.ShootTarget(currentTarget, projectileSpawn);
            shootTimer = 0f;
        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            UnityEngine.Debug.Log("OnTriggerExit2D: " + other.gameObject.name);
            tower.OnTargetEnterRange(other.transform);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            UnityEngine.Debug.Log("OnTriggerExit2D: " + other.name);
            tower.OnTargetExitRange(other.transform);
        }
    }
}
