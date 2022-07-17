using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

/// <summary>
/// Controls the EndScreen UI object.
/// </summary>
public class EndScreenUIController : MonoBehaviour
{
    [Header("Components")]
    public TextMeshProUGUI headerText;  // Reference the EndScreen headertext object.
    public TextMeshProUGUI bodyText;    // Reference the EndScreen bodytext object.

    /// <summary>
    /// Set the EndScreen variables.
    /// </summary>
    /// <param name="didWin">Bool to determine if the Player won or lost the game.</param>
    /// <param name="roundsSurvived">How many rounds did the Player survive.</param>
    public void SetEndScreen(bool didWin, int roundsSurvived)
    {
        // Set the headertext
        headerText.text = didWin ? "Congratulations You Win!" : "Game Over!";
        // Change the header text color.
        headerText.color = didWin ? Color.green : Color.red;
        // Set the EndScreen body text.
        bodyText.text = $"You survived {roundsSurvived} rounds.";
    }

    /// <summary>
    /// Restart the game so that the Play can replay the game.
    /// Called when the EndScreen "ReplayButton" is clicked.
    /// </summary>
    public void OnPlayAgainButton()
    {
        // Reload the current game scene and reset the level.
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    /// <summary>
    /// Quit the Game when the EndScreen Quit button is clicked.
    /// </summary>
    public void OnQuitButton()
    {
        // Exit the game and close the game application.
        Application.Quit();
    }
}