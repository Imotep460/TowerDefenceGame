using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Setup backend code for a scriptable object that can hold the data for the individual towers in the game.
/// Scriptableobjects store data, they can therefore be considered "datacontainers".
/// </summary>
[CreateAssetMenu(fileName = "Tower Data", menuName = "New Tower Data")]
public class TowerData : ScriptableObject
{
    [Header("Tower Stats")]
    public string displayName;          // The name of the Tower.
    public int towerCost;               // The cost of the Tower.
    public float towerRange;            // The range of the Tower.
    public Sprite towerIcon;            // The sprite icon for the Tower.
    public GameObject towerPrefab;      // The prefab to spawn when initiallizing the Tower.
}