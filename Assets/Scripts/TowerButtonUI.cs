using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class TowerButtonUI : MonoBehaviour
{
    [Header("Components")]
    public TowerData towerData;                 // Reference the data for the Tower object this button is representing.
    public TextMeshProUGUI towerNameText;   // Reference Text Mesh Pro object to display the name of the Tower.
    public TextMeshProUGUI towerCostText;   // Reference Text Mesh Pro object to display the cost of the Tower.
    public Image towerIcon;                 // Reference the Icon that represents this Tower in the UI.
    private Button towerBuyButton;           // Reference the tower buy button for this Tower.

    /// <summary>
    /// Make sure that the TowerButtonUI has a reference to the towerBuyButton.
    /// </summary>
    private void Awake()
    {
        towerBuyButton = GetComponent<Button>();    // Set the Button reference for the towerBuyButton.
    }

    /// <summary>
    /// When the Button is Enabled subscribe to the onPlayerCashChanged Event from the GameManager.
    /// This OnEnable is called before the Awake method.
    /// </summary>
    private void OnEnable()
    {
        // Subscribe to the onPlayerCashChanged Event.
        GameManager.instance.onPlayerCashChanged.AddListener(OnCashChanged);
    }

    /// <summary>
    /// When the Button is Disabled unsubscribe to the onPlayerCashChanged Event from the GameManager.
    /// This OnDisable is called before the Awake method.
    /// </summary>
    private void OnDisable()
    {
        // Unsubscribe to the onPlayerCashChanged Event from the GameManger.
        GameManager.instance.onPlayerCashChanged.RemoveListener(OnCashChanged);
    }

    /// <summary>
    /// 
    /// </summary>
    public void OnClick()
    {

    }

    /// <summary>
    /// Setup an Event that subscribes to the GameManagers onMoneyChanged event.
    /// Disable or Enable the Button based on whether or not the Player has Cash enoúgh to afford the Tower in question. 
    /// </summary>
    private void OnCashChanged()
    {
        // Check with the GameManager to see if the Player has cash enough for the Tower in question.
        // Disable the Button if the Player cannot afford the Tower and Enable the Button if the Player can afford the Tower.
        towerBuyButton.interactable = GameManager.instance.currentPlayerCash >= towerData.towerCost;
    }

    // Start is called before the first frame update
    void Start()
    {
        // Set the UI information to display the correct information for the given Tower.
        towerNameText.text = towerData.displayName;
        towerCostText.text = $"${towerData.towerCost}";
        towerIcon.sprite = towerData.towerIcon;

        // Check on Start if the Player can afford the given Tower, and Disable or Enable the Button based on the result.
        OnCashChanged();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}