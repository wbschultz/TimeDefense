using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerController : MonoBehaviour
{
    [Header("Tower default behaviour")]
    public bool buildTower = false;
    public TowerSchematic towerSchematic;
    // Tower behaviour and attributes.
    private Tower tower;
    private Transform projectileSpawn;
    // Tower's shooting properties.
    private float shootTimer = 0f;
    private float shootInterval;

    // Called before first frame update, initialize controller settings
    void Start()
    {
        // Initialize shoot timer and create tower of towerSchematic type.
        shootTimer = shootInterval = 1 / towerSchematic.towerFireRate;
        if (towerSchematic)
        {
            tower = new Tower(towerSchematic, transform, buildTower);
        }

        // Get tower projectile spawn location from first child.
        if (transform.childCount > 0) projectileSpawn = transform.GetChild(0);

        // Setup tower range w/collider dimensions using schematic range.
        CircleCollider2D towerRange = GetComponent<CircleCollider2D>();
        if (towerRange)
        {
            towerRange.radius = tower.towerRange;
        }
    }

    // Called once per frame
    void Update()
    {
        ShootIfTargetInRange();
        shootTimer += Time.deltaTime;
    }

    // Shoot targets in range of tower
    private void ShootIfTargetInRange()
    {
        // Get tower's current target.
        Transform currentTarget = tower.GetCurrentTarget();

        // Shoot target if timer is up and it exists.
        if (currentTarget && shootTimer >= shootInterval)
        {
            tower.ShootTarget(currentTarget, projectileSpawn);
            shootTimer = 0f;
        }

    }

    // Called when target enters tower range.
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            // Target entered tower range, let tower know.
            UnityEngine.Debug.Log("OnTriggerExit2D: " + other.gameObject.name);
            tower.OnTargetEnterRange(other.transform);
        }
    }

    // Called when target exits tower range.
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            // Target exited tower range, let tower know.
            UnityEngine.Debug.Log("OnTriggerExit2D: " + other.name);
            tower.OnTargetExitRange(other.transform);
        }
    }

    private void OnMouseDown()
    {
        UnityEngine.Debug.Log("TowerController::OnMouseDown()");
    }
}
