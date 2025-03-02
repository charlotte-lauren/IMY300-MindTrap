using UnityEngine;

public class Player : MonoBehaviour
{
    public int playerID; // Unique ID for each player (1, 2, 3, 4)
    public string playerColor; // Color of the player token (e.g., "Red", "Blue")
    public int currentPosition = 1; // Current position on the board (starts at 0)

    private Transform[] boardPositions; // Array to store all board positions

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
    }


    public void Move(int spaces)
    {
        currentPosition += spaces; // Update the player's position

        // Clamp the position to 100 (the maximum position)
        if (currentPosition > 100)
        {
            currentPosition = 100;
        }

        // Check for snakes and ladders
        currentPosition = GameManager.Instance.CheckForSnakesAndLadders(currentPosition);

        // Move the player token to the new position
        Vector3 newPosition = boardPositions[currentPosition - 1].position;

        // Add an offset based on the player's ID to avoid overlapping
        switch (playerID)
        {
            case 1:
                newPosition += new Vector3(-0.1f, 0.1f, 0); // Top-left offset for Player 1
                break;
            case 2:
                newPosition += new Vector3(0.1f, 0.1f, 0); // Top-right offset for Player 2
                break;
            case 3:
                newPosition += new Vector3(-0.1f, -0.2f, 0); // Bottom-left offset for Player 3
                break;
            case 4:
                newPosition += new Vector3(0.1f, -0.1f, 0); // Bottom-right offset for Player 4
                break;
        }
        transform.position = newPosition;

        Debug.Log("Player " + playerID + " moved to position " + currentPosition);
    }

    // Method to move player to a new position (called by GameController)
    public void MoveToPosition(Vector3 newPosition)
    {
        transform.position = newPosition;
    }
}
