using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;

public class PlacementManager : MonoBehaviour
{
    [SerializeField]
    GameObject prefabToPlace;
    [SerializeField]
    TestGrid gridToPlaceOn;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartPlacing()
    {
        if (prefabToPlace.GetComponent<PlaceableObject>())
        {
            GameObject placeable = Instantiate(prefabToPlace, UtilsClass.GetMouseWorldPosition(), Quaternion.identity);
            placeable.GetComponent<PlaceableObject>().grid = gridToPlaceOn;

        } else
        {
            Debug.LogError("Instantiating unplaceable object");
        }
        
    }
}
