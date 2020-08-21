using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SortOrderByYpos : MonoBehaviour
{
    private void OnEnable()
    {
        OnChangePos();
    }

    private void Update()
    {
        if (transform.hasChanged)
        {
            OnChangePos();
        }
    }

    /// <summary>
    /// update sort order of sprite renderer
    /// </summary>
    private void OnChangePos()
    {
        GetComponent<SpriteRenderer>().sortingOrder = (int)(transform.position.y*-10);
    }
}
