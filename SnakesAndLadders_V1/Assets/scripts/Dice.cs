using UnityEngine;

public class Dice : MonoBehaviour
{
    public int diceResult { get; private set; } // Stores the result of the dice roll
    public bool isRolling { get; private set; } // Tracks if the dice is currently rolling

    private void OnMouseDown()
    {
        if (!isRolling) // Ensure the dice isn't already rolling
        {
            RollDice();
        }
    }

    private void RollDice()
    {
        isRolling = true;
        diceResult = Random.Range(1, 7); // Generate a random number between 1 and 6
        Debug.Log("Dice rolled: " + diceResult);

        // Play dice roll animation (we'll implement this later)
        // For now, just log the result

        // Simulate a short delay to mimic rolling
        Invoke("FinishRoll", 1.5f); // Wait 1.5 seconds before finishing the roll
    }

    private void FinishRoll()
    {
        isRolling = false;
        Debug.Log("Dice roll finished. Result: " + diceResult);

        // Notify the game manager or player to move (we'll implement this later)
    }
}