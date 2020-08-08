using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 3f;
    public int maxHp = 100;
    public int currentHp;
    public int attackDmg = 2;
    public StatusConditionIndex statusConditionIndex;
    public List<StatusCondition> effectiveStatuses;
    private List<StatusCondition> statuses = new List<StatusCondition>();
    private float speedModifier = 1f;
    private int waypointIndex = 0;
    private Transform[] waypoints = new Transform[0];
    private Transform target;


    private void Start()
    {
        UnityEngine.Debug.Log("Enemy::Start()");
        // Select enemy's target from first waypoint.
        currentHp = maxHp;
    }

    private void Update()
    {
        Move();
    }

    public void SetWaypoints(Transform[] _waypoints)
    {
        UnityEngine.Debug.Log("Enemy::SetWaypoints()");
        waypoints = _waypoints;
        if (waypoints.Length > 0) target = waypoints[0];
    }

    public int GotHit(int damage, StatusCondition appliedCondition, float duration = 0f)
    {
        // Reduce hp by damage taken
        currentHp = currentHp - damage;

        if (currentHp <= 0)
        {
            // Enemy just died.
            Destroy(gameObject);
        }
        else
        {
            // Apply status effects if any.
            StartCoroutine(ApplyStatusCondition(appliedCondition, duration));
        }

        return currentHp;
    }

    private void Move()
    {
        float currentSpeed = speed * speedModifier;

        if (statuses.Contains(statusConditionIndex.stasis))
        {
            // Enemy is stopped, it can't move.
            return;
        }

        // Move to current waypoint.
        transform.position = Vector2.MoveTowards(transform.position, target.position, currentSpeed * Time.deltaTime);

        // If reached waypoint, switch to next waypoint.
        float distanceToWaypoint = Vector2.Distance(transform.position, target.position);
        if (distanceToWaypoint <= 0.1f)
        {
            if (statuses.Contains(statusConditionIndex.rewind))
            {
                // Enemy is rewinded, go backwards.
                GoToEarlierWaypoint();
            }
            else
            {
                // Enemy is not rewinded, go forward.
                GoToLaterWaypoint();
            }
        }
    }

    private void GoToLaterWaypoint()
    {
        if (waypointIndex >= waypoints.Length - 1)
        {
            // Enemy reached destination.
            // Do attack
            CentralCore core = FindObjectOfType<CentralCore>();
            // Apply effect
            core.DamageCore(attackDmg);
            // 
            Destroy(gameObject);
            return;
        }

        // Not yet reached destination, go to next waypoint.
        waypointIndex++;
        target = waypoints[waypointIndex];
    }

    private void GoToEarlierWaypoint()
    {
        if (waypointIndex == 0)
        {
            // Reached starting waypoint, nothing else to do but wait.
            return;
        }

        waypointIndex--;
        target = waypoints[waypointIndex];
    }

    private void ActivateStatusEffect(StatusCondition status)
    {
        if (status == statusConditionIndex.slow)
        {
            // Enemy slowed, slow their speed.
            speedModifier = status.modifier;
        }
        else if (status == statusConditionIndex.rewind)
        {
            // Enemy rewinded, go to previous waypoint.
            waypointIndex--;
            if (waypointIndex <= 0) waypointIndex = 0;

            target = waypoints[waypointIndex];
        }

        // Add status to enemy's status conditions.
        statuses.Add(status);
    }

    private void RemoveStatusEffect(StatusCondition status)
    {
        if (status == statusConditionIndex.slow)
        {
            // Slow is removed, reset enemy speed.
            speedModifier = 1f;
        } 
        else if (status == statusConditionIndex.rewind)
        {
            // Rewind is removed, enemy should go to next waypoint.
            waypointIndex++;
            if (waypointIndex >= waypoints.Length - 1)
            {
                waypointIndex = waypoints.Length - 1;
            }

            target = waypoints[waypointIndex];
        }

        // Remove status condition from enemy.
        statuses.Remove(status);
    }

    private IEnumerator ApplyStatusCondition(StatusCondition status, float duration)
    {
        if (effectiveStatuses.Contains(status) && !statuses.Contains(status))
        {
            // If status is effective and not already applied then apply it.
            ActivateStatusEffect(status);
        }
        else
        {
            // Status not effective or already applied, don't do anything.
            yield break;
        }

        // Wait for duration of status effect before removing it.
        yield return new WaitForSeconds(duration);
        RemoveStatusEffect(status);
    }
}
