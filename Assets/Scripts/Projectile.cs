 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Script that controls the behavior of the projectiles fired på the Towers in the Game.
/// </summary>
public class Projectile : MonoBehaviour
{
    [Header("Components")]
    private Enemy targetEnemy;              // Reference the projectiles target.
    public GameObject hitSpawnPrefab;       // Reference any prefabs that trigger when a projectile hits an Enemy (Explosions, particles etc.).

    [Header("Projectile Stats")]
    private int projectileDamage;           // The damage that the projectile does to the target enemy object.
    private float projectileMovementSpeed;  // The movementspeed of the projectile.

    /// <summary>
    /// Initialize the projectile object.
    /// </summary>
    /// <param name="target">The projectiles target.</param>
    /// <param name="damage">The damage the projectile will do to the enemy,</param>
    /// <param name="movementSpeed">The speed of the projectile.</param>
    public void Initialize(Enemy target, int damage, float movementSpeed)
    {
        this.targetEnemy = target;
        this.projectileDamage = damage;
        this.projectileMovementSpeed = movementSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        // Check and make sure that the projectile has a target.
        if (targetEnemy != null)
        {
            // Move the projectile towards the target object.
            transform.position = Vector3.MoveTowards(transform.position, targetEnemy.transform.position, projectileMovementSpeed * Time.deltaTime);

            // Adjust the projectile object so that it is facing the target object.
            transform.LookAt(targetEnemy.transform);

            // Check if the projectile is close enough to damage the target object.
            if (Vector3.Distance(transform.position, targetEnemy.transform.position) < 0.2f)
            {
                // Damage the enemy object.
                targetEnemy.TakeDamage(projectileDamage);

                // Check if the projectile has a hitSpawnPrefab to instantiate, and immediately instantiate the hitSpawnPrefab if the projectile has one.
                if (hitSpawnPrefab != null)
                {
                    Instantiate(hitSpawnPrefab, transform.position, Quaternion.identity);
                }

                // After damage calculation and spawning of any and all hitSpawnPrefabs destroy the projectile object.
                Destroy(this.gameObject);
            }
        }
        else
        {
            // If the projectile does not have a target destroy the projectile object.
            Destroy(this.gameObject);
        }
    }
}