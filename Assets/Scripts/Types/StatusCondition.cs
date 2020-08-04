using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StatusCondition", menuName = "Types/Statuses")]
public class StatusCondition : ScriptableObject
{
    public string statusName;
    public string statusDescription;
    public float modifier = 1f;
}
