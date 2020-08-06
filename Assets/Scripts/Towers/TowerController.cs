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
    private List<Transform> projectileSpawns = new List<Transform>();
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

        // Get tower projectile spawn locations from children.
        for (int i = 0; i < transform.childCount; i++)
        {
            Transform child = transform.GetChild(i);
            if(child.tag == "ProjectileSpawn")
            {
                projectileSpawns.Add(child);
            }
        }

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
        List<Transform> currentTargets = tower.GetCurrentTargets();

        // Shoot target if timer is up and it exists.
        if (currentTargets.Count > 0 && shootTimer >= shootInterval)
        {
            tower.ShootTargets(currentTargets, projectileSpawns);
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
