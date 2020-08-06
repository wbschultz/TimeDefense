using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StatusConditionIndex", menuName = "Types/StatusConditionIndex")]
public class StatusConditionIndex : ScriptableObject
{
    public StatusCondition stasis;
    public StatusCondition slow;
    public StatusCondition rewind;
}
