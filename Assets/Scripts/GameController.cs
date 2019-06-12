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
    Vector2 bounds;
    Snake snake;
    GameObject food;
    List<GameObject> droppings;

    /** void GenerateFood()
     * Generate food in a random position on the board.
     *
     * Postconditions
     *  Food will not be superimposed with any part of the snake or any
     *  droppings, Food will be within the bounds of the board.
     */
    void GenerateFood() {
        // TODO: Implement
    }

    /** bool InBounds(Vector3 pos)
     * Determine if the given position is in the bounds of the game board.
     *
     * Parameters
     *  pos - position to compare
     */
    bool InBounds(Vector3 pos) {
        // TODO: Implement
        return false;
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
    bool InPoop(Vector3 pos) {
        // TODO: Implement
        return false;
    }
}
