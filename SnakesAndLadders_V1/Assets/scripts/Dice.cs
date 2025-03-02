using UnityEngine;

public class Dice : MonoBehaviour
{
    public int DiceResult { get;  set; } // Stores the result of the dice roll
    public bool isRolling { get; private set; } // Tracks if the dice is currently rolling
    private GameController gameController;

    void Start()
    {
        gameController = FindFirstObjectByType<GameController>(); // Get reference to the GameController
    }

    public void OnMouseDown()
    {
        if (!isRolling) // Ensure the dice isn't already rolling
        {
            RollDice();
        }
    }
    public void SetDiceResult(int result)
    {
        DiceResult = result;
    }


    private void RollDice()
    {
        isRolling = true;
        DiceResult = Random.Range(1, 7); // Generate a random number between 1 and 6
        Debug.Log("Dice rolled: " + DiceResult);

        // Play dice roll animation (optional, can be implemented later)

        // Simulate a short delay to mimic rolling
        Invoke("FinishRoll", 1.5f); // Wait 1.5 seconds before finishing the roll
    }

    private void FinishRoll()
    {
        isRolling = false;
        Debug.Log("Dice roll finished. Result: " + DiceResult);

        // Notify the GameController to move the player
        gameController.OnDiceRolled(DiceResult);
    }
}
