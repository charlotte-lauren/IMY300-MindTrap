using UnityEngine;
using System.Collections.Generic;

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
    [SerializeField] private AudioClip snakeSound;
    
    [SerializeField] private AudioClip ladderSound;

    // private Dictionary<int, List<int>> positionToPlayers = new Dictionary<int, List<int>>(); // Tracks players on each position

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

        // Initialize the position tracking for all players
        foreach (var player in players)
        {
            // UpdatePlayerPosition(player.playerID, player.currentPosition);
            currentPlayerIndex = (currentPlayerIndex + 1) % players.Length;
            Debug.Log("It's now Player " + (currentPlayerIndex + 1) + "'s turn.");
        }
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
                
                // sound
                SoundFXManager.instance.PlaySoundFXClip(ladderSound, transform, 1f);

                return ladder.endPosition;
            }
        }

        // Check for snakes
        foreach (var snake in snakes)
        {
            if (snake.startPosition == position)
            {
                Debug.Log("Snake found! Moving from " + position + " to " + snake.endPosition);
                
                // sound
                SoundFXManager.instance.PlaySoundFXClip(snakeSound, transform, 1f);

                return snake.endPosition;
            }
        }

        // No snake or ladder found
        return position;
    }

    // // Returns the number of players on a given position
    // public int GetPlayersOnPosition(int position)
    // {
    //     if (positionToPlayers.ContainsKey(position))
    //     {
    //         return positionToPlayers[position].Count;
    //     }
    //     return 0;
    // }

    // // Updates the player's position in the tracking system
    // public void UpdatePlayerPosition(int playerID, int newPosition)
    // {
    //     // Remove the player from their old position
    //     foreach (var key in positionToPlayers.Keys)
    //     {
    //         if (positionToPlayers[key].Contains(playerID))
    //         {
    //             positionToPlayers[key].Remove(playerID);
    //             break;
    //         }
    //     }

    //     // Add the player to the new position
    //     if (!positionToPlayers.ContainsKey(newPosition))
    //     {
    //         positionToPlayers[newPosition] = new List<int>();
    //     }
    //     positionToPlayers[newPosition].Add(playerID);
    // }
}