using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    /// <summary>
    /// Enum that lets the Player change the Towers target priority.
    /// </summary>
    public enum TowerTargetPriority
    {
        First,
        Last,
        Close,
        Strong
    }

    [Header("Tower Information")]
    private List<Enemy> currentEnemiesInRange = new List<Enemy>();  // A list of all the enemies currently within the range of the tower.
    private Enemy currentEnemy;                                     // The current target enemy.
    public TowerTargetPriority targetPriority;                      // The towers current targetpriority.
    public bool rotateTowardsTarget;                                // Bool that enables a Tower to rotate towards a target.

    [Header("Attacking")]
    private float lastAttackTime;                                   // Time since the towers last attack.
    public GameObject projectilePrefab;                             // Reference the projectile prefab for the Towers projectile.
    public Transform projectileSpawnPosition;                       // Reference the position where to spawn the towers projectile.

    [Header("Tower Stats")]
    public int projectileDamage;                                    // The damage the Tower can do.
    public float rateOfFire;                                        // The Towers rate of fire.
    public float projectileSpeed;                                   // The speed of the towers projectile.
    public float towerRange;                                        // The range of the Tower.


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Check if enough time has passed since the Towers last attack to see if the Tower can attack again.
        if (Time.time - lastAttackTime > rateOfFire)
        {
            // Reset the lastAttackTime.
            lastAttackTime = Time.time;

            // Set the current target of the Tower.
            currentEnemy = GetTargetEnemy();

            // If the Tower has a target then attack with the Tower.
            if (currentEnemy != null)
            {
                TowerAttack();
            }
        }
    }

    /// <summary>
    /// Get an Enemy for the Tower to target.
    /// </summary>
    /// <returns>Return an Enemy object, based on the Towers targetpriority.</returns>
    private Enemy GetTargetEnemy()
    {
        // Remove all "Null" enemies (enemies that has been killed by other towers)
        currentEnemiesInRange.RemoveAll(x => x == null);

        // Chack that there are enemies within range.
        if (currentEnemiesInRange.Count == 0)
        {
            return null;
        }

        // Check if there are only 1 enemy within the range of the tower.
        if (currentEnemiesInRange.Count == 1)
        {
            return currentEnemiesInRange[0];
        }

        // Return a target enemy based on the targetpriority setting of the tower.
        switch(targetPriority)
        {
            case TowerTargetPriority.First:
                {
                    // If the tower is targeting the first enemy within range,
                    // simply return the first enemy in the currentEnemiesInRange list,
                    // as it is the first enemy to have entered the towers range.
                    return currentEnemiesInRange[0];
                }
            case TowerTargetPriority.Last:
                {
                    // If the Tower is targeting the last Enemy within range,
                    // simply return the last Enemy in the currentEnemiesInRange list,
                    // as it is the last Enemy to have entered the towers range.
                    return currentEnemiesInRange[currentEnemiesInRange.Count - 1];
                }
            case TowerTargetPriority.Close:
                {
                    // If the tower is targeting the closest Enemy within range,
                    Enemy closestEnemy = null;  // Prepare a field to reference the Enemy closest to the tower.
                    float distance = 99;        // Field to hold a reference to the distance between the Tower and the Enemy.

                    // Loop through the enemies in the currentEnemiesInRange list.
                    for (int x = 0; x < currentEnemiesInRange.Count; x++)
                    {
                        float tempDistance = (transform.position - currentEnemiesInRange[x].transform.position).sqrMagnitude;

                        // Check if the tempdistance is smaller (and therefore closer) than the privious closest Enemy.
                        if (tempDistance < distance)
                        {
                            closestEnemy = currentEnemiesInRange[x];
                            distance = tempDistance;
                        }
                    }
                    return closestEnemy;
                }
            case TowerTargetPriority.Strong:
                {
                    Enemy strongestEnemy = null;    // Prepare a field to reference the Enemy with the most health within range of the Tower.
                    int strongestHealth = 0;        // Field that references the health value of the strongest Enemy.

                    // Loop through each of the enemies in the towers currentEnemiesInRange list.
                    foreach (Enemy enemy in currentEnemiesInRange)
                    {
                        // Check if the current enemy has more health than the privious strongest Enemy.
                        if (enemy.enemyCurrentHealth > strongestHealth)
                        {
                            strongestEnemy = enemy;
                            strongestHealth = enemy.enemyCurrentHealth;
                        }
                    }
                    // Return the strongest Enemy found in the towers currentEnemiesInRange list.
                    return strongestEnemy;
                }
        }
        return null;
    }

    /// <summary>
    /// The Tower attacks the Towers current target enemy from the Towers currentEnemiesInRange list.
    /// </summary>
    private void TowerAttack()
    {
        // Check if the Tower is supposed to rotate towards the Towers target.
        if (rotateTowardsTarget == true)
        {
            // If rotateTowardsTarget is true rotate the Tower.
            transform.LookAt(currentEnemy.transform);
            // Lock the y-rotation.
            transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
        }


        // Instantiate and maintain a reference to the towers projectile.
        GameObject towerProjectile = Instantiate(projectilePrefab, projectileSpawnPosition.position, Quaternion.identity);
        // Make sure that the towerProjectile has a reference to the Projectile script/Component.
        towerProjectile.GetComponent<Projectile>().Initialize(currentEnemy, projectileDamage, projectileSpeed);
    }

    /// <summary>
    /// If an Enemy enters the Collider set as a Trigger on the Tower, add the Enemy to the Towers currentEnemiesInRange list.
    /// </summary>
    /// <param name="other">References the GameObject entering the Towers Trigger Collider,</param>
    private void OnTriggerEnter(Collider other)
    {
        // Check if the object entering the Towers Collider is an Enemy.
        if (other.CompareTag("Enemy"))
        {
            // Add the Enemy Object to the Towers currentEnemiesInRange list.
            currentEnemiesInRange.Add(other.GetComponent<Enemy>());
        }
    }

    /// <summary>
    /// If an Enemy exits the Collider set as a Trigger on the Tower, remove the Enemy from the Towers currentEnemiesInRange list.
    /// </summary>
    /// <param name="other">References GameObject exiting the Towers Trigger Collider.</param>
    private void OnTriggerExit(Collider other)
    {
        // Check if the object exiting the Towers Collider is an Enemy.
        if (true)
        {
            // Remove the Enemy object from the Towers currentEnemiesInRange list.
            currentEnemiesInRange.Remove(other.GetComponent<Enemy>());
        }
    }
}