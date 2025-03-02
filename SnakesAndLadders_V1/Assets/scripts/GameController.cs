using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour
{
    public Player[] players; // Reference to all players
    public Dice dice; // Reference to the dice object
    public int currentPlayerIndex = 0; // Track which player's turn it is
    public bool isPlayerMoving = false; // Flag to check if any player is moving

    void Start()
    {
        // Initialize game setup
        SetPlayerTurn(0); // Set the first player to play
    }

    void Update()
    {
        // Check if the current player has finished moving
        if (!isPlayerMoving && !dice.isRolling)
        {
            if (Input.GetKeyDown(KeyCode.Space)) // Press Space to roll the dice
            {
                RollDice();
            }
        }
    }

    public void SetPlayerTurn(int playerIndex)
    {
        currentPlayerIndex = playerIndex;
        Debug.Log("It is now Player " + (currentPlayerIndex + 1) + "'s turn.");
    }


    void RollDice()
    {
        // If dice isn't rolling, roll it for the current player
        dice.GetComponent<Dice>().OnMouseDown(); // This will call the dice roll logic
    }

    public void OnDiceRolled(int diceResult)
    {
        // Move the current player
        MovePlayer(diceResult);

        // Wait until player finishes moving before ending the turn
        isPlayerMoving = true;
        StartCoroutine(MovePlayerCoroutine(diceResult));
    }

    IEnumerator MovePlayerCoroutine(int diceResult)
    {
        // Get the current player
        Player currentPlayer = players[currentPlayerIndex];

        // Loop to animate player movement
        int targetPosition = currentPlayer.currentPosition + diceResult;
        if (targetPosition > 100) targetPosition = 100; // Restrict to max board position

        // Smoothly move the player across the tiles
        while (currentPlayer.currentPosition < targetPosition)
        {
            currentPlayer.currentPosition++;
            currentPlayer.transform.position = GetBoardPosition(currentPlayer.currentPosition);
            yield return new WaitForSeconds(0.1f); // Adjust movement speed
        }

        // Once the player reaches their destination
        CheckForSpecialTiles(currentPlayer);

        // End the turn
        EndTurn();
    }

    void MovePlayer(int diceResult)
    {
        // Tell the dice when it's rolled
        dice.SetDiceResult(diceResult);


        // After dice is rolled, pass control to GameManager to move player
        OnDiceRolled(diceResult);
    }

    void CheckForSpecialTiles(Player player)
    {
        // Check if player landed on a snake or ladder (this will depend on your board layout)
        if (player.currentPosition == 10) // Example: Custom snake or ladder check
        {
            player.currentPosition = 25; // Ladder up
        }
        else if (player.currentPosition == 30) // Example: Snake down
        {
            player.currentPosition = 5;
        }

        // Update the player's position after special tile check
        player.transform.position = GetBoardPosition(player.currentPosition);
    }

    void EndTurn()
    {
        // Move to the next player
        currentPlayerIndex = (currentPlayerIndex + 1) % players.Length;

        // Reset flags
        isPlayerMoving = false;
    }

    Vector3 GetBoardPosition(int position)
    {
        // Find the corresponding GameObject (empty) based on the position
        GameObject positionObject = GameObject.Find("Position" + position); // Finds the Empty GameObject by name

        if (positionObject != null)
        {
            return positionObject.transform.position; // Return the world position of the Empty object
        }
        else
        {
            Debug.LogError("Position" + position + " not found!");
            return Vector3.zero; // Return a default position if not found, // position is now (0, 0, 0)
        }
    }

}
