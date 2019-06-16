using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/** GameController
 * General control of the entire structure of the game.
 *
 * Field Summary
 *
 * Public Fields
 *  Vector2 Bounds       - x and y boundaries of the game board
 *
 * Private Fields
 *  Snake snake                 - player
 *  GameObject food             - food game object
 *  DroppingManager droppings   - manage the droppings in the game
 *
 * Method Summary
 *
 *  void GenerateFood()
 *      Generate food in a random position on the board.
 *
 *  bool InBounds(Vector3 pos)
 *      Determine if the given position is in the bounds of the game board.
 */
public class GameController : MonoBehaviour
{
    Snake snake;
    GameObject food;
    DroppingManager droppings;

    public Vector2 Bounds;

    /** void Start()
     * Initialize the Game controller
     */
    void Start() {
        // TODO: Implement
    }

    /** void GenerateFood()
     * Generate food in a random position on the board.
     *
     * Postconditions
     *  Food will not be superimposed with any part of the snake or any
     *  droppings, Food will be within the bounds of the board.
     */
    public void GenerateFood() {
        // TODO: Implement
    }

    /** public bool InBounds(Vector3 pos)
     * Determine if the given position is in the bounds of the game board.
     *
     * Parameters
     *  pos - position to compare
     */
    public bool InBounds(Vector3 pos) {
        return pos.x >= 0 && pos.x < Bounds.x &&
            pos.y >= 0 && pos.y < Bounds.y;
    }

    /** public int TilesLeft()
     * Return the amount of empty tiles left on the board.
     */
    public int TilesLeft() {
        // TODO: Implement
        return 0;
    }
}
