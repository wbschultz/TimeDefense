using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerController : MonoBehaviour
{
    [Header("Tower behaviour")]
    public bool buildTower = false;
    public TowerSchematic towerSchematic;
    // Tower targets and projectile props.
    private List<Transform> projectileSpawns = new List<Transform>();
    private List<Transform> currentTargets = new List<Transform>();
    private List<Transform> inRangeEnemies = new List<Transform>();
    // Tower's firing props.
    private float shootTimer = 0f;
    private float shootInterval;

    void Awake()
    {
        shootTimer = shootInterval = 1 / towerSchematic.towerFireRate;

        // Initialize shoot timer and create tower of towerSchematic type.
        if (towerSchematic && buildTower)
        {
            towerSchematic.BuildTower(transform.gameObject);
        }

        // Setup tower range w/collider dimensions using schematic range.
        CircleCollider2D towerRange = GetComponent<CircleCollider2D>();
        if (towerRange)
        {
            towerRange.radius = towerSchematic.towerRange;
        }

        // Get tower projectile spawn locations from children.
        for (int i = 0; i < transform.childCount; i++)
        {
            Transform child = transform.GetChild(i);
            if (child.tag == "ProjectileSpawn")
            {
                projectileSpawns.Add(child);
            }
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
 
        // Shoot targets if any and timer is up.
        if (currentTargets.Count > 0 && shootTimer >= shootInterval)
        {
            towerSchematic.ShootTargets(currentTargets, projectileSpawns);
            shootTimer = 0f;
        }

    }

    // Called when target enters tower range.
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            // Add target in range enemies.
            Transform enemy = other.transform;
            inRangeEnemies.Add(enemy);
            // Tower isn't targeting anyone yet, attack new target.
            if (currentTargets.Count == 0)
            {
                GetNewTargets();
            }
        }
    }

    // Called when target exits tower range.
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            // Enemy exited range, remove from in range enemies.
            Transform enemy = other.transform;
            inRangeEnemies.Remove(enemy);
            // Current target out of range, update current targets.
            if (currentTargets.Contains(enemy))
            {
                GetNewTargets();
            }
        }
    }

    // Have tower choose new target from in range targets.
    private void GetNewTargets()
    {
        // Choose target based of tower types behaviour.
        List<Transform> newTargets = towerSchematic.ChooseInRangeTargets(transform, inRangeEnemies);
        currentTargets = newTargets;
    }

    private void OnMouseDown()
    {
        UnityEngine.Debug.Log("TowerController::OnMouseDown()");
    }
}
