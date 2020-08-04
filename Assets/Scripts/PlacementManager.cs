using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;

public class PlacementManager : MonoBehaviour
{
    [SerializeField]
    TestGrid gridToPlaceOn;
    [SerializeField]
    SpriteRenderer ghostSprite;

    private bool buildMode;
    private TowerSchematic selectedTower;

    private void Awake()
    {
        buildMode = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (buildMode)
        {
            ghostSprite.gameObject.transform.position = gridToPlaceOn.SnapPosition(UtilsClass.GetMouseWorldPosition() + new Vector3 (0.5f, 0.5f, 0f));
            if (Input.GetMouseButton(0))
            {
                PlaceTower();
            }
        } 
    }

    public void StartPlacing(TowerSchematic prefabToPlace)
    {
        selectedTower = prefabToPlace;
        ghostSprite.gameObject.SetActive(true);
        //set sprite source
        ghostSprite.sprite = selectedTower.towerSprite;
        buildMode = true;
    }

    private void PlaceTower()
    {
        // check value in grid first
        // if placeable:
        // set sprite to inactive
        ghostSprite.gameObject.SetActive(false);
        // turn off build mode
        buildMode = false;
        //instantiate
        GameObject tower = Instantiate(selectedTower.GetTowerPrefab(), ghostSprite.transform.position, Quaternion.identity);
        //set value in grid (placeable)
    }
}
