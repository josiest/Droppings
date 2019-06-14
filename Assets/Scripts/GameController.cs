using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/** GameController
 * General control of the entire structure of the game.
 *
 * Field Summary
 *
 *  Vector2 bounds              - x and y boundaries of the game board
 *  Snake snake                 - player
 *  GameObject food             - food game object
 *  List<GameObject> droppings  - snake droppings
 *
 * Method Summary
 *
 *  void GenerateFood()
 *      Generate food in a random position on the board.
 *
 *  bool InBounds(Vector3 pos)
 *      Determine if the given position is in the bounds of the game board.
 *
 *  bool InPoop(Vector3 pos)
 *      Determine if the given position is the location of a dropping.
 */
public class GameController : MonoBehaviour
{
    Snake snake;

    public Vector2 bounds;
    public GameObject food;
    public List<GameObject> droppings;

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

    /** bool InBounds(Vector3 pos)
     * Determine if the given position is in the bounds of the game board.
     *
     * Parameters
     *  pos - position to compare
     */
    public bool InBounds(Vector3 pos) {
        return pos.x >= 0 && pos.x < bounds.x &&
            pos.y >= 0 && pos.y < bounds.y;
    }

    /** bool InPoop(Vector3 pos)
     * Determine if the given position is the location of a dropping.
     *
     * Parameters
     *  pos - position to compare
     *
     * Postconditions
     *  InPoop compares pos with every dropping on the game board.
     */
    public bool InPoop(Vector3 pos) {
        // TODO: Implement
        return false;
    }
}
