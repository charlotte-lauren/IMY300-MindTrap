using UnityEngine;
using System.Collections;

public class Dice : MonoBehaviour
{
    public int diceResult { get; private set; } // Stores the result of the dice roll
    public bool isRolling { get; private set; } // Tracks if the dice is currently rolling

    // Predefined rotations for each face of the dice
    private readonly Vector3[] faceRotations = new Vector3[]
    {
        new Vector3(0, 0, 0),    // Face 1
        new Vector3(-90, 0, 0),   // Face 2
        new Vector3(90, 90, 0),       // Face 3
        new Vector3(-90, -90, 0),   // Face 4
        new Vector3(90, 0, 0),  // Face 5
        new Vector3(180, 0, 0)   // Face 6
    };

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

        // Start the rolling animation
        StartCoroutine(RollingAnimation());

        // Simulate a short delay to mimic rolling
        Invoke("FinishRoll", 1.5f); // Wait 1.5 seconds before finishing the roll
    }

    private IEnumerator RollingAnimation()
    {
        float duration = 1.0f; // Duration of the rolling animation
        float elapsed = 0f;

        while (elapsed < duration)
        {
            // Randomly rotate the dice to simulate rolling
            transform.Rotate(Random.Range(250, 750) * Time.deltaTime, 
                            Random.Range(250, 750) * Time.deltaTime, 
                            Random.Range(250, 750) * Time.deltaTime);
            elapsed += Time.deltaTime;
            yield return null;
        }

        // Align the dice to the correct face after rolling
        AlignDiceToFace();
    }

    private void AlignDiceToFace()
    {
        // Set the dice rotation to the corresponding face based on diceResult
        transform.rotation = Quaternion.Euler(faceRotations[diceResult - 1]);
    }

    private void FinishRoll()
    {
        isRolling = false;
        Debug.Log("Dice roll finished. Result: " + diceResult);

        // Notify the current player to move
        Player currentPlayer = GameManager.Instance.GetCurrentPlayer();
        currentPlayer.Move(diceResult);

        // End the current player's turn
        GameManager.Instance.EndTurn();
    }
}