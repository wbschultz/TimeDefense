using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StatusConditionList", menuName = "Types/StatusConditionList")]
public class StatusConditionList : ScriptableObject
{
    public StatusCondition stasis;
    public StatusCondition slow;
    public StatusCondition rewind;
}
