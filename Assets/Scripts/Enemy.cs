using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Implements the Enemy Stats, and localises the Enemys path.
/// Implements Enemy movement.
/// Implements Enemy ability to damage the Player.
/// Implements method so that the Enemy can be damaged.
/// </summary>
public class Enemy : MonoBehaviour
{
    [Header("Enemy Stats")]
    public int enemyMaxHealth;      // The maximum health that the enemy can have.
    public int enemyCurrentHealth;  // The current health that the enemy has.
    public int enemyDamage;         // The damage that the enemy can do to the Player.
    public int cashOnDeath;         // The amount of cash that the Player gets for destroying the enemy.
    public float enemyMovespeed;    // The movementspeed of the enemy.

    /// <summary>
    /// To shrink down the code the enemy references the path the enemy takes on the map.
    /// </summary>
    [Header("Enemy Path")]
    private Transform[] path;       // The array storing the Path for the enemy.
    private int currentWaypoint;    // The current waypoint the enemy is moving towards.

    // Start is called before the first frame update
    void Start()
    {
        // Get the path for the enemy.
        path = GameManager.instance.enemyPath.waypoints;
    }

    // Update is called once per frame
    void Update()
    {
        MoveAlongEnemyPath(); // Move the Enemy object.
    }

    /// <summary>
    /// Move the Enemy object along the Enemys path.
    /// If the Enemy object reaches the final waypoint damage the Player.
    /// </summary>
    private void MoveAlongEnemyPath()
    {
        // Check if the enemy has reached the final waypoint.
        // AKA check if the enemys currentwaypoint is the last in the list.
        if (currentWaypoint < path.Length)
        {
            // If the enemy has not reached the final waypoint move towards the next waypoint in the list.
            transform.position = Vector3.MoveTowards(transform.position, path[currentWaypoint].position, enemyMovespeed * Time.deltaTime);

            // check if the enemy has reached the next waypoint.
            if (transform.position == path[currentWaypoint].position)
            {
                // If the enemy has reached the waypoint set the new waypoint.
                currentWaypoint++;
            }
        }
        else
        {
            // If the enemy has reached the final waypoint damage the Player.
            GameManager.instance.TakeDamage(enemyDamage);
            // After damaging the Player Invoke the onEnemyDestroyed event.
            GameManager.instance.onEnemyDestroyed.Invoke();
            // Then destroy the Enemy Gameobject.
            Destroy(this.gameObject);
        }
    }

    /// <summary>
    /// When the Enemy is hit by a Tower damage the Enemy.
    /// </summary>
    /// <param name="Amount">The amount of damage the Enemy takes.</param>
    public void TakeDamage(int Amount)
    {
        enemyCurrentHealth -= Amount; // Subtract from the Enemys currentHealth the amount of damage the Enemy is taking.

        // Check if the Enemys health has reached 0.
        if (enemyCurrentHealth <= 0)
        {
            // Give cash to the Player as a reward for killing the Enemy.
            GameManager.instance.AddCash(cashOnDeath);
            // Invoke the onEnemyDestroyed event.
            GameManager.instance.onEnemyDestroyed.Invoke();
            // Destroy the Enemy Gameobject.
            Destroy(this.gameObject);
        }
    }
}