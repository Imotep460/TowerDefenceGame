using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Script for setting up a waypoints to create a path for the Enemy to follow.
/// </summary>
public class EnemyPath : MonoBehaviour
{
    [Header("Enemy Waypoints")]
    public Transform[] waypoints; // The path that the enemye characters take,
                                  // index 0 is the beginning of the enemy path,
                                  // and the last waypoint is the enemy goal.
                                  // If an enemy reaches the last waypoint the enemy can damage the Player.

    /// <summary>
    /// Using the build in Unity.MonoBehaviour function OnDrawGizmos(),
    /// draw gizmos on the enemy waypoints so that the waypoints are easily visable in the Unity editor.
    /// </summary>
    private void OnDrawGizmos()
    {
        //Change the color of the Gizmos.
        Gizmos.color = Color.red;
        // Loop through the waypoints.
        for (int x = 0; x < waypoints.Length; x++)
        {
            // Draw a sphere gizmo around the waypoint.
            Gizmos.DrawWireSphere(waypoints[x].position, 0.25f); // The Gizmo is centered at the waypoints x-position. The radius of the Giz<mo is 0.25f.

            // Check if the current waypoint is the end waypoint.
            if (x < waypoints.Length - 1)
            {
                // Draw a line between the current gizmo and the next gizmo in the paypoints list.
                Gizmos.DrawLine(waypoints[x].position, waypoints[x + 1].position);
            }
        }
    }
}