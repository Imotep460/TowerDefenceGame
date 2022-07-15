using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Setup the preview tower data.
/// </summary>
public class PreviewTower : MonoBehaviour
{
    [Header("Components")]
    public Transform rangeObject;       // Reference the PreviewTowers range indicator.

    /// <summary>
    /// Set the Preview with the data of the Tower being placed.
    /// </summary>
    /// <param name="towerData">The data for the Tower being placed.</param>
    public void SetPreviewTower(TowerData towerData)
    {
        // Set the scale of the range indicator on the PreviewTower.
        rangeObject.localScale = new Vector3(towerData.towerRange * 2, 0.001f, towerData.towerRange * 2);
    }
}