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
    public float towerPlaceYOffset = 0.15f;      // Ensure that the TowerPreview is not skipping into the TowerTile.
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
        if (isPlacingTower)
        {
            // Set a reference for a Ray variable which is later used for a Raycast.
            // Set the position to be the same a the Mousecursor
            Ray ray = camera.ScreenPointToRay(Mouse.current.position.ReadValue());
            // Store information about what the raycast has hit.
            RaycastHit raycastHit;

            // Check if the Raycast hits a TowerTile.
            if (Physics.Raycast(ray, out raycastHit, 99, tileLayerMask))
            {
                // Get the TowerTile component of whatever object the raycast is hitting.
                currentSelectedTowerTile = raycastHit.collider.GetComponent<TowerTile>();
                // Move the PreviewTower so othat it is on the TowerTile that has been selected.
                previewTower.transform.position = currentSelectedTowerTile.transform.position + new Vector3(0, towerPlaceYOffset, 0);
            }
            else
            {
                // If the RayCast does not hit a TowerTile set the currentSelectedTowerTile to null.
                currentSelectedTowerTile = null;
                // Move the PreviewTower out of Player view. NOTE; Disabling the PreviewTower object is a heavier operation,
                previewTower.transform.position = new Vector3(0, 999, 0);
            }

            // Check for if the left mosebutton is pressed AND make sure that a TowerTile is selected AND that the TowerTile does not already have a Tower on it.
            if (Mouse.current.leftButton.isPressed && currentSelectedTowerTile != null && currentSelectedTowerTile.tower == null)
            {
                // Place the Tower on the currentSelectedTowerTile.
                PlaceTower();
            }

            // Check if the right mousebutton is pressed.
            if (Mouse.current.rightButton.isPressed)
            {
                // Cancel the Tower placement.
                CancelTowerPlacement();
            }
        }
    }

    /// <summary>
    /// Gets the information from the TowerUIButton so that the data for the Tower the Player wants to place can be transferred to the TowerPreview. 
    /// </summary>
    /// <param name="towerData"></param>
    public void SelectTowerToPlace(TowerData towerData)
    {
        towerToPlaceData = towerData;   // Set the data for the Tower being placed.
        isPlacingTower = true;          // As the Player is placing a Tower set the isPlacingTower bool to reflect this.

        // Activate the Preview Tower.
        previewTower.gameObject.SetActive(true);
        // Set the data for the PreviewTower.
        previewTower.SetPreviewTower(towerData);
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