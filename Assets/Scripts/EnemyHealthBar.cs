using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 
/// </summary>
public class EnemyHealthBar : MonoBehaviour
{
    [Header("Variables")]
    private Enemy enemy;            // Reference the Enemy related to the healthbar.
    private int enemyStartHealth;   // The health the Enemy starts with.

    [Header("Components")]
    public Image healthMarker;      // Reference the healthbars healthmarker.
    public Gradient colorGradient;  // Reference the color gradient of the healthmarker.
    private Camera camera;          // Reference the Scene camera.

    public void Initialize(Enemy enemy)
    {
        // Set the reference to the Enemy object.
        this.enemy = enemy;
        // Set the health that the Enemy starts with.
        enemyStartHealth = enemy.enemyMaxHealth;

        // Set the camera reference.
        camera = Camera.main;


    }

    // Update is called once per frame
    void Update()
    {
        // Check if the Enemy is still alive.
        if (enemy != null)
        {
            // If the Enemy still exists then set the healthmarker amount.
            healthMarker.fillAmount = (float)enemy.enemyCurrentHealth / (float)enemyStartHealth;
            // Set the healthmarker color.
            healthMarker.color = colorGradient.Evaluate(healthMarker.fillAmount);

            // Make the healthbar follow the enemy around the map.
            transform.position = camera.WorldToScreenPoint(enemy.transform.position) + new Vector3(0, Screen.height / 30.0f);
        }
        else
        {
            // If the Enemy has been destroyed destroy the healthbar as well.
            Destroy(this.gameObject);
        }
    }
}