using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Manages Placing and spawning in a given Tower and setting the preview tower object.
/// </summary>
public class TowerPlacement : MonoBehaviour
{
    [Header("Components")]
    public LayerMask tileLayerMask;             // Identify and maintain a reference to the tiles Layer.
    public PreviewTower previewTower;           // Reference the PreviewTower object.
    private TowerData towerToPlaceData;         // Reference the Data for the Tower being placed.
    private TowerTile currentSelectedTowerTile; // Reference the Towertile where the Player is attempting to place a Tower.
    private Camera camera;                      // Reference the Scene Camera.

    [Header("Variables")]
    public float towerPlaceYOffset = 0.1f;      // Ensure that the TowerPreview is not skipping into the TowerTile.
    private bool isPlacingTower;                // Bool to track if the Player is currently attempting to place a Tower.

    private void Awake()
    {
        // Set the Camera reference.
        camera = Camera.main;
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// Gets the information from the TowerUIButton so that the data for the Tower the Player wants to place can be transferred to the TowerPreview. 
    /// </summary>
    /// <param name="towerData"></param>
    public void SelectTowerToPlace(TowerData towerData)
    {

    }

    private void PlaceTower()
    {

    }

    /// <summary>
    /// Cancels placing the Tower, wither by Player input or because the TowerPlacement failed.
    /// Removes the TowerPreview and cancels the cash spending etc, like nothing ever happened.
    /// </summary>
    private void CancelTowerPlacement()
    {
        towerToPlaceData = null;                    // Null the towerToPlaceData
        isPlacingTower = false;                     // Set isPlacingTower to false as the Player is no longer placing a Tower.
        currentSelectedTowerTile = null;            // No Tower is being placed as such no TowerTile is selected.
        previewTower.gameObject.SetActive(false);   // Disable the TowerPreview.
    }
}