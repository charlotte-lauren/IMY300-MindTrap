using UnityEngine;

[System.Serializable]
public class SnakeOrLadder
{
    public int startPosition; // Start position of the snake or ladder
    public int endPosition;   // End position of the snake or ladder
}

public class GameManager : MonoBehaviour
{
    public static GameManager Instance; // Singleton instance for easy access

    public Player[] players; // Array of all players
    public int currentPlayerIndex = 0; // Index of the current player

    public SnakeOrLadder[] snakes; // Array of snakes
    public SnakeOrLadder[] ladders; // Array of ladders


    private void Awake()
    {
        // Set up the singleton instance
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void StartGame()
    {
        // Initialize the game
        Debug.Log("Game started! Player 1's turn.");
    }

    public void EndTurn()
    {
        // Move to the next player
        currentPlayerIndex = (currentPlayerIndex + 1) % players.Length;
        Debug.Log("It's now Player " + (currentPlayerIndex + 1) + "'s turn.");
    }

    public Player GetCurrentPlayer()
    {
        return players[currentPlayerIndex];
    }

    public int CheckForSnakesAndLadders(int position)
    {
        // Check for ladders
        foreach (var ladder in ladders)
        {
            if (ladder.startPosition == position)
            {
                Debug.Log("Ladder found! Moving from " + position + " to " + ladder.endPosition);
                return ladder.endPosition;
            }
        }

        // Check for snakes
        foreach (var snake in snakes)
        {
            if (snake.startPosition == position)
            {
                Debug.Log("Snake found! Moving from " + position + " to " + snake.endPosition);
                return snake.endPosition;
            }
        }

        // No snake or ladder found
        return position;
    }
}