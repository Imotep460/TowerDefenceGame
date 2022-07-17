using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controls an Explosion created by Tower projectiles.
/// </summary>
public class Explosion : MonoBehaviour
{
    [Header("Explosion Variables")]
    public float explosionRange;        // The range of the Explosion.
    public int explosionDamage;         // The damage of the Explosion.
    public LayerMask enemyLayerMask;    // Reference a Layermask so that the Explosion can identify the objects within its range.

    // Start is called before the first frame update
    void Start()
    {
        // Set the range/scale of the explosion.
        transform.localScale = Vector3.one * explosionRange;
        // Damage the Enemies within the range of the Explosion.
        DamageEnemies();
        // Start the ExplosionAnimation Corutine.
        StartCoroutine(ExplosionAnimation());
    }

    /// <summary>
    /// Damage enemies within the range of the Explosion.
    /// </summary>
    private void DamageEnemies()
    {
        // Get all the enemies in range of the explosion.
        Collider[] enemiesInRange = Physics.OverlapSphere(transform.position, explosionRange, enemyLayerMask);

        for (int x = 0; x < enemiesInRange.Length; x++)
        {
            // Damage the current Enemy.
            enemiesInRange[x].GetComponent<Enemy>().TakeDamage(explosionDamage);
        }
    }

    /// <summary>
    /// Control the Explosion Animation.
    /// Scales the Explosion towards 0.
    /// </summary>
    /// <returns></returns>
    private IEnumerator ExplosionAnimation()
    {
        // Add a small delay.
        yield return new WaitForSeconds(0.2f);

        while (transform.localScale.x != 0.0f)
        {
            // Scale the Explosion down towards zero.
            transform.localScale = Vector3.MoveTowards(transform.localScale, Vector3.zero, Time.deltaTime * 3);
            yield return null;
        }

        // Destroy the Explosion GameObject.
        Destroy(this.gameObject);
    }
}