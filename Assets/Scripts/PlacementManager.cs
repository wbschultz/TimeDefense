using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;

public class PlacementManager : MonoBehaviour
{
    [SerializeField]
    SpriteRenderer ghostSprite;
    [SerializeField]
    private int width = 48;
    [SerializeField]
    private int height = 36;
    [SerializeField]
    private float cellSize = 1f;

    private Grid grid;
    private GameObject player;

    private bool buildMode = false;
    private TowerSchematic selectedTower;
    private readonly Color buildModeWhite = new Color(1f, 1f, 1f, 0.4f);    // color for valid placement
    private readonly Color buildModeRed = new Color(1f, 0f, 0f, 0.3f);      // color for invalid placement

    private void Awake()
    {
        grid = new Grid(width, height, cellSize, transform.position);
    }

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (buildMode)
        {
            Vector3 playerToMouse;
            if (player == null)
            {
                // if can't find player object, place anywhere and log error
                Debug.LogError("Can't find obj in scene with tag 'Player'!");
                playerToMouse = UtilsClass.GetMouseWorldPosition();
            } else
            {
                // get distance vector from player to mouse
                playerToMouse = UtilsClass.GetMouseWorldPosition() - player.transform.position;
            }
            
            // reduce magnitude of vector to 3, or current magnitude, whichever is shorter
            playerToMouse = (Mathf.Min(3f, playerToMouse.magnitude) * playerToMouse.normalized) + player.transform.position + new Vector3(0.5f, 0.5f, 0);
            // fix ghost sprite (preview) to the mouse position
            ghostSprite.gameObject.transform.position =
                grid.SnapPosition(playerToMouse // position in grid
                + transform.position); // plus offset of transform

            // check value in grid first
            if (grid.GetValue(ghostSprite.transform.position) == 0)
            {   // if placeable:
                // set ghost sprite to green
                ghostSprite.color = buildModeWhite;
                // if player clicks in valid location, build tower
                if (Input.GetMouseButton(0))
                {
                    PlaceTower();
                }
            } else
            {
                //set ghost sprite to red
                ghostSprite.color = buildModeRed;
                // stop building on click
                if (Input.GetMouseButton(0))
                    CancelPlacing();
                
            }
        } 
    }

    /// <summary>
    /// start build mode for specific tower
    /// </summary>
    /// <param name="prefabToPlace"></param>
    public void StartPlacing(TowerSchematic prefabToPlace)
    {
        selectedTower = prefabToPlace;
        ghostSprite.gameObject.SetActive(true);
        //set sprite source
        ghostSprite.sprite = selectedTower.towerSprite;
        buildMode = true;
    }

    /// <summary>
    /// Cancel build mode
    /// </summary>
    private void CancelPlacing()
    {
        buildMode = false;
        selectedTower = null;
        ghostSprite.gameObject.SetActive(false);
        ghostSprite.sprite = null;
    }

    private void PlaceTower()
    {
        // set ghost sprite to inactive
        ghostSprite.gameObject.SetActive(false);
        // turn off build mode
        buildMode = false;
        //instantiate
        GameObject tower = Instantiate(selectedTower.GetTowerPrefab(), ghostSprite.transform.position, Quaternion.identity);
        //set value in grid (placeable)
        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                grid.SetValue(tower.transform.position + new Vector3(x, y, 0), 1);
            }
        }

    }
}
