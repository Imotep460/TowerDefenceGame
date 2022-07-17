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
    public TextMeshProUGUI healthAndCashText;   // The text component that displays the Players Health and Cash.
    public EnemyPath enemyPath;                 // The waypoints that the enemies follow.
    public TowerPlacement towerPlacement;       // Reference the TowerPlacement script.
    public EndScreenUIController endScreen;     // Reference the EndScreen prefab.
    public WaveSpawner waveSpawner;             // Reference the WaveSpawner.

    [Header("Events")]
    public UnityEvent onEnemyDestroyed;         // Event called when a enemy is destoyed.
    public UnityEvent onPlayerCashChanged;      // Event called when the player cash changes.


    // Ensure that GameManager is created as a Singleton.
    public static GameManager instance;         // Reference the GameManager.

    /// <summary>
    /// Using Awake make sure that GameManager is created as a Singleton.
    /// Awake is called before "Start" methods
    /// </summary>
    private void Awake()
    {
        // Set the Instance reference.
        instance = this;
    }

    private void Start()
    {
        // Set the Player current cash to be the Player Start cash.
        currentPlayerCash = playerStartCash;
        // Make sure that the health and cash text matches on game start.
        UpdateHealthAndCashText();
    }

    /// <summary>
    /// Update the UI to display the Players current health and the Players current available cash.
    /// </summary>
    private void UpdateHealthAndCashText()
    {
        healthAndCashText.text = $"Health: {currentPlayerHealth} / {playerHealthMax}\nCash: ${currentPlayerCash}";
    }

    /// <summary>
    /// Add cash to the player and update the game UI.
    /// </summary>
    /// <param name="amount">The amount of cash the Player has earned.</param>
    public void AddCash(int amount)
    {
        // Award the currentPlayerCash with the amount the Player is awarded with for killing an Enemy object.
        currentPlayerCash += amount;
        // Update the Player healthAndCashText so that the UI reflects the correct amount of available cas´h that the Player has.
        UpdateHealthAndCashText();
        // Invoke the onPlayerCashChanged event.
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
    /// Activate and set the EndScreen.
    /// </summary>
    private void GameOver()
    {
        // Activate the EndScreen object.
        endScreen.gameObject.SetActive(false);
        // Set the EndScreen data.
        endScreen.SetEndScreen(false, waveSpawner.currentWave);
    }

    /// <summary>
    /// When the Player has met the requirements to win the game, the Player wins the game.
    /// Activate and set the EndScreen.
    /// </summary>
    private void GameWin()
    {
        // Activate the EndScreen object.
        endScreen.gameObject.SetActive(true);
        // Set the EndScreen data.
        endScreen.SetEndScreen(true, waveSpawner.currentWave);
    }

    /// <summary>
    /// OnEnemyDestroyed ties into the onEnemyDestroyed event, and is called when an enemy is destroyed.
    /// </summary>
    public void OnEnemyDestroyed()
    {
        // Check if there are enemies left AND that the Player is on the games last wave.
        if (waveSpawner.remainingEnemies == 0 && waveSpawner.currentWave == waveSpawner.waves.Length)
        {
            // Call the win game method, when the Player has killed all enemies and the Player has reached the final wave of the Game.
            GameWin();
        }
    }
}