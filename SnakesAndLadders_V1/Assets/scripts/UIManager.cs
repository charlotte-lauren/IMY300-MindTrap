using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
//using TMPro;


public class UIManager : MonoBehaviour
{
    public static UIManager Instance; // Singleton

    // Main Menu UI
    public GameObject mainMenuPanel;
    //public TMP_Dropdown playerCountDropdown;

    // In-Game UI
    public GameObject inGameUIPanel;
    public Text turnIndicator;
    public Text diceResultText;

    // Win Screen UI
    public GameObject winScreenPanel;
    public Text winnerText;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // MAIN MENU FUNCTIONS
    public void StartGame()
    { 
            SceneManager.LoadScene("Game"); // Load the game scene
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    // IN-GAME UI FUNCTIONS
    public void UpdateTurnIndicator(int playerID)
    {
        turnIndicator.text = "Player " + playerID + "'s Turn";
    }

    public void UpdateDiceResult(int result)
    {
        diceResultText.text = "Rolled: " + result;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("Game"); // Restart game
    }

    public void QuitToMenu()
    {
        SceneManager.LoadScene("MainMenu"); // Return to main menu
    }

    // WIN SCREEN FUNCTIONS
    public void ShowWinScreen(int playerID)
    {
        if (winScreenPanel != null)
            winScreenPanel.SetActive(true);

        if (winnerText != null)
            winnerText.text = "Player " + playerID + " Wins!";
    }
}
