using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 3f;
    public int maxHp = 100;
    public int currentHp;
    public StatusConditionList statusConditions;
    public List<StatusCondition> effectiveStatuses;
    private List<StatusCondition> statuses = new List<StatusCondition>();
    private float speedModifier = 1f;
    private int waypointIndex = 0;
    private Transform target;


    private void Start()
    {
        // Select enemy's target from first waypoint.
        target = Waypoints.waypoints[0];
        currentHp = maxHp;
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        float currentSpeed = speed * speedModifier;

        if (statuses.Contains(statusConditions.stasis))
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
            if (statuses.Contains(statusConditions.rewind))
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
        if (waypointIndex >= Waypoints.waypoints.Length - 1)
        {
            // Enemy reached destination.
            Destroy(gameObject);
            return;
        }

        // Not yet reached destination, go to next waypoint.
        waypointIndex++;
        target = Waypoints.waypoints[waypointIndex];
    }

    private void GoToEarlierWaypoint()
    {
        if (waypointIndex == 0)
        {
            // Reached starting waypoint, nothing else to do but wait.
            return;
        }

        waypointIndex--;
        target = Waypoints.waypoints[waypointIndex];
    }

    private void ActivateStatusEffect(StatusCondition status)
    {
        if (status == statusConditions.slow)
        {
            // Enemy slowed, slow their speed.
            speedModifier = status.modifier;
        }
        else if (status == statusConditions.rewind)
        {
            // Enemy rewinded, go to previous waypoint.
            waypointIndex--;
            if (waypointIndex <= 0) waypointIndex = 0;

            target = Waypoints.waypoints[waypointIndex];
        }

        // Add status to enemy's status conditions.
        statuses.Add(status);
    }

    private void RemoveStatusEffect(StatusCondition status)
    {
        if (status == statusConditions.slow)
        {
            // Slow is removed, reset enemy speed.
            speedModifier = 1f;
        } 
        else if (status == statusConditions.rewind)
        {
            // Rewind is removed, enemy should go to next waypoint.
            waypointIndex++;
            if (waypointIndex >= Waypoints.waypoints.Length - 1)
            {
                waypointIndex = Waypoints.waypoints.Length - 1;
            }

            target = Waypoints.waypoints[waypointIndex];
        }

        // Remove status condition from enemy.
        statuses.Remove(status);
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

    public IEnumerator ApplyStatusCondition(StatusCondition status, float duration)
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
