using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EffectiveStatuses", menuName = "Types/StatusConditionList")]
public class StatusConditionList : ScriptableObject
{
    private HashSet<StatusCondition> statuses = new HashSet<StatusCondition>();
    public List<StatusCondition> statusConditions = new List<StatusCondition>();

    private void OnEnable()
    {
        // Add all status conditions to hash set for quick searching.
        foreach(StatusCondition statusCondition in statusConditions)
        {
            statuses.Add(statusCondition);
        }
    }

    private void OnDisable()
    {
        // Clear status conditions.
        statuses.Clear();
    }

    public bool ContainsStatus(StatusCondition status)
    {
        return statuses.Contains(status);
    }
}
