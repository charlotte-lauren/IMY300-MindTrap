using UnityEngine;

public class Player : MonoBehaviour
{
    public int playerID; // Unique ID for each player (1, 2, 3, 4)
    public string playerColor; // Color of the player token (e.g., "Red", "Blue")
    public int currentPosition = 1; // Current position on the board (starts at 0)

    void Start()
    {
        // Assign a tag based on the playerID
        gameObject.tag = "Playerrrr" + playerID;

        // Log the player's details for debugging
        Debug.Log("Player " + playerID + " (" + playerColor + ") initialized at position " + currentPosition);
    }
}