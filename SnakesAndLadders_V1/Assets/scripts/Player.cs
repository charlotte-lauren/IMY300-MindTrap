using UnityEngine;
using System.Collections.Generic;

public class Player : MonoBehaviour
{
    public int playerID; // Unique ID for each player (1, 2, 3, 4)
    public string playerColor; // Color of the player token (e.g., "Red", "Blue")
    public int currentPosition = 1; // Current position on the board (starts at 1)

    private Transform[] boardPositions; // Array to store all board positions
    private static Dictionary<int, List<Player>> playersOnPosition = new Dictionary<int, List<Player>>(); // Track players on each position

    void Start()
    {
        // Assign a tag based on the playerID
        gameObject.tag = "Player" + playerID;

        // Log the player's details for debugging
        Debug.Log("Player " + playerID + " (" + playerColor + ") initialized at position " + currentPosition);

        // Find all board positions
        boardPositions = new Transform[100];
        for (int i = 1; i <= 100; i++)
        {
            boardPositions[i - 1] = GameObject.Find("BoardPositions/Position" + (i)).transform;
        }

        // Initialize the player's position in the dictionary
        if (!playersOnPosition.ContainsKey(currentPosition))
        {
            playersOnPosition[currentPosition] = new List<Player>();
        }
        playersOnPosition[currentPosition].Add(this);
    }

    public void Move(int spaces)
    {
        // Remove the player from the current position in the dictionary
        playersOnPosition[currentPosition].Remove(this);

        currentPosition += spaces; // Update the player's position

        // Clamp the position to 100 (the maximum position)
        if (currentPosition > 100)
        {
            currentPosition = 100;
        }

        // Check for snakes and ladders
        currentPosition = GameManager.Instance.CheckForSnakesAndLadders(currentPosition);

        // Add the player to the new position in the dictionary
        if (!playersOnPosition.ContainsKey(currentPosition))
        {
            playersOnPosition[currentPosition] = new List<Player>();
        }
        playersOnPosition[currentPosition].Add(this);

        // Move the player token to the new position
        Vector3 newPosition = boardPositions[currentPosition - 1].position;

        // Adjust the position based on the number of players on the same position
        int playerCount = playersOnPosition[currentPosition].Count;
        if (playerCount == 1)
        {
            // Center the player if alone
            newPosition += Vector3.zero;
        }
        else
        {
            // Calculate the offset based on the player's ID and the number of players on the same position
            float offsetX = 0f;
            float offsetY = 0f;

            switch (playerID)
            {
                case 1:
                    offsetX = -0.065f;
                    offsetY = 0.065f;
                    break;
                case 2:
                    offsetX = 0.065f;
                    offsetY = 0.065f;
                    break;
                case 3:
                    offsetX = -0.065f;
                    offsetY = -0.065f;
                    break;
                case 4:
                    offsetX = 0.065f;
                    offsetY = -0.065f;
                    break;
            }

            // Adjust the offset based on the number of players
            newPosition += new Vector3(offsetX, offsetY, 0);
        }

        transform.position = newPosition;

        Debug.Log("Player " + playerID + " moved to position " + currentPosition);
    }
}