using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

/// <summary>
/// Script for manageing the Game.
/// </summary>
public class GameManager : MonoBehaviour
{
    [Header("GameData")]
    public int playerHealthMax;                 // The maximum health the player can have in the game.
    public int currentPlayerHealth;             // The current health the player has.
    public int currentPlayerCash;               // The cuttent cash available to the player.
    public int playerStartCash;                 // The amount of cash the player starts the game with.

    [Header("Components")]
    public TextMeshProUGUI healthAndMoneyText;  // The text component that displays the Players Health and Cash.
    public EnemyPath enemyPath;                 // The waypoints that the enemies follow.

    [Header("Events")]
    public UnityEvent onEnemyDestroyed;         // Event called when a enemy is destoyed.
    public UnityEvent onPlayerCashChanged;      // Event called when the player cash changes.


    // Ensure that GameManager is created as a Singleton.
    public static GameManager instance;     // Reference the GameManager.

    /// <summary>
    /// Using Awake make sure that GameManager is created as a Singleton.
    /// Awake is called before "Start" methods
    /// </summary>
    private void Awake()
    {
        instance = this;
    }

    /// <summary>
    /// Update the UI to display the Players current health and the Players current available cash.
    /// </summary>
    private void UpdateHealthAndCashText()
    {
        healthAndMoneyText.text = $"Health: {currentPlayerHealth}/{playerHealthMax}\n Cash: ${currentPlayerCash}";
    }

    /// <summary>
    /// Add cash to the player and update the game UI.
    /// </summary>
    /// <param name="amount">The amount of cash the Player has earned.</param>
    public void AddCash(int amount)
    {
        currentPlayerCash += amount;
        UpdateHealthAndCashText();

        onPlayerCashChanged.Invoke();
    }

    /// <summary>
    /// When the Player spends cash, subtract cash from the Player.
    /// </summary>
    /// <param name="amount">The amount of cash the Player has spend/looses.</param>
    public void TakeCash(int amount)
    {
        currentPlayerCash -= amount;
        UpdateHealthAndCashText();

        onPlayerCashChanged.Invoke();
    }

    /// <summary>
    /// Subtract from the Players CurrentHealth when the Player takes damage.
    /// </summary>
    /// <param name="amount">Amount of damage the Player takes.</param>
    public void TakeDamage(int amount)
    {
        currentPlayerHealth -= amount;
        UpdateHealthAndCashText();

        // Check if the Players health has reached 0.
        if (currentPlayerHealth <= 0)
        {
            GameOver();
        }
    }

    /// <summary>
    /// If the Player has no more health left the Player looses the game.
    /// </summary>
    private void GameOver()
    {

    }

    /// <summary>
    /// When the Player has met the requirements to win the game, the Player wins the game.
    /// </summary>
    private void GameWin()
    {

    }

    /// <summary>
    /// OnEnemyDestroyed ties into the onEnemyDestroyed event, and is called when an enemy is destroyed.
    /// </summary>
    public void OnEnemyDestroyed()
    {

    }
}