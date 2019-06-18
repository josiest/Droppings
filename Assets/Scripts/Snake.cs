using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/** Snake
 * Provide information and control about the snake's movement
 *
 * Constructor Summary
 *
 *  public Snake() - Create a new snake.
 *
 * Method Summary
 *
 *  public void AddBody(Vector3 pos)
 *      Create a snake body GameObject and add it to the snake.
 *
 *  public void Move(Vector3 dir)
 *      Move the snake one unit in the direction of dir.
 *
 *  public bool Eats(Vector3 pos)
 *      Determine if the snake has eaten the object at the given position.
 *      
 *  public bool Contains(Vector3 pos)
 *      Determine if the snake's body contains the given position.
 *
 *  public void Digest()
 *      Set a bowel timer for the snake to be able to poop.
 *
 *  public bool Poops()
 *      Determine if the snake needs to poop.
 */
public class Snake
{
    /** public Snake()
     * Create a new snake.
     *
     * New snakes have bodies 4 units long.
     */
    public Snake() {
        // TODO: Implement
    }

    /** public void AddBody(Vector3 pos)
     * Create a snake body GameObject and add it to the snake.
     *
     * Parameters
     *  pos - position of the body part.
     *
     * Preconditions
     *  pos doesn't overlap any other body parts, and continues from the tail
     *  of the snake.
     */
    public void AddBody(Vector3 pos) {
        // TODO: Implement
    }

    /** public void Move(Vector3 dir)
     * Move the snake one unit in the direction of dir.
     *
     * Parameters:
     *  dir - direction to move in. 3rd dimension is ignored.
     *
     * Preconditions
     *  dir is within the set {left, right, up, down}
     *
     * Postconditions:
     *  All parts of the snake are continuous and have not made any sudden
     *  jumps in position.
     */
    public void Move(Vector3 dir) {
        // TODO: Implement
    }

    /** public bool Eats(Vector3 pos)
     * Determine if the snake has eaten the object at the given position.
     *
     * Parameters:
     *  pos - position to compare
     *
     * Postconditions:
     *  Eats will only compare the given position with the head of the snake.
     */
    public bool Eats(Vector3 pos) {
        // TODO: Implement
        return false;
    }

    /** public bool Contains(Vector3 pos)
     * Determine if the snake's body contains the given position.
     *
     * Parametrs:
     *  pos - position to compare.
     *
     * Postconditions:
     *  Contains will compare the given position with the entire body of
     *  the snake.
     */
    public bool Contains(Vector3 pos) {
        // TODO: Implement
        return false;
    }

    /** public void Digest()
     * Set a bowel timer for the snake to be able to poop.
     *
     * Postconditions
     *  If a bowel timer is already ticking, Digest() doesn't overwrite that
     *  timer but instead adds another one. The timer should count for as many
     *  frames as the snake is long minus one.
     */
    public void Digest() {
        // TODO: Implement
    }

    /** public bool Poops()
     * Determine if the snake needs to poop.
     *
     * Postconditions
     *  A snake will poop if a bowel timer has reached 0. This method will
     *  also move bowel timers forward one tick.
     */
    public bool Poops() {
        // TODO: Implement
        return false;
    }
}
