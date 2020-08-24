using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using CodeMonkey.Utils;

/// <summary>
/// Class to manage placement of towers, a grid for that placement,
/// and validation of the player-selected location.
/// </summary>
public class PlacementManager : MonoBehaviour
{

    #region publics
    public bool debugGrid;
    public PlayerData dataPlayer;
    public bool buildMode = false;
    #endregion

    #region serialized privates
    [SerializeField]
    SpriteRenderer ghostSprite;
    [SerializeField]
    private int width = 48;
    [SerializeField]
    private int height = 36;
    [SerializeField]
    private float cellSize = 1f;
    #endregion

    #region privates
    private Grid grid;
    private GameObject player;
    private UnityEngine.Tilemaps.Tilemap pathGrid;

    private bool onUpgradeMenu = false;
    private TowerSchematic selectedTower;
    private readonly Color buildModeWhite = new Color(1f, 1f, 1f, 0.4f);    // color for valid placement
    private readonly Color buildModeRed = new Color(1f, 0f, 0f, 0.3f);      // color for invalid placement
    #endregion

    private void Awake()
    {
        grid = new Grid(width, height, cellSize, transform.position, debugGrid);
    }

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        pathGrid = GameObject.FindGameObjectWithTag("Path").GetComponent<UnityEngine.Tilemaps.Tilemap>();
        InvalidatePathPlacement();
    }

    // Update is called once per frame
    void Update()
    {
        if (buildMode)
        {
            Vector3 playerToMouse;
            bool isMouseOverUI = EventSystem.current.IsPointerOverGameObject();

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
            playerToMouse = (Mathf.Min(3f, playerToMouse.magnitude) * playerToMouse.normalized) + player.transform.position;
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
                if (!isMouseOverUI && Input.GetMouseButton(0) )
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
    /// Set value in grid based on whether tile exists on the path grid
    /// </summary>
    private void InvalidatePathPlacement()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Vector3Int tilePosition = Vector3Int.FloorToInt(transform.position + new Vector3(x, y));
                grid.SetValue(x, y, pathGrid.HasTile(tilePosition)?1:0);
            }
        }
    }

    public void OnEnterLeaveUpgradeMenu(bool _onUpgradeMenu)
    {
        onUpgradeMenu = _onUpgradeMenu;
        if (buildMode)
            ghostSprite.gameObject.SetActive(!onUpgradeMenu);
        UnityEngine.Debug.Log("On upgrade menu: " + onUpgradeMenu.ToString());
    }

    /// <summary>
    /// start build mode for specific tower
    /// </summary>
    /// <param name="prefabToPlace"></param>
    public void StartPlacing(TowerSchematic prefabToPlace)
    {
        if (dataPlayer.EnoughMoney(prefabToPlace.towerCost))
        {
            selectedTower = prefabToPlace;
            ghostSprite.gameObject.SetActive(true);
            //set sprite source
            ghostSprite.sprite = selectedTower.towerSprite;
            buildMode = true;
        }
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

    /// <summary>
    /// Build tower at current ghost sprite position
    /// </summary>
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
        dataPlayer.SpendMoney(selectedTower.towerCost);
    }
}
