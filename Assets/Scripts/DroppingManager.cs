using System.Collections.Generic;
using UnityEngine;

/** DroppingManager
 * Create and maintain snake droppings.
 *
 * Field Summary
 *
 *  List<GameObject> droppings  - snake dropping objects
 *
 * Constructor Summary
 *
 *  public DroppingManager()
 *      Create a new DroppingManager with no Droppings.
 *
 * Method Summary
 *
 *  public GameObject CreateDropping(Vector3 pos)
 *      Create a Dropping at the given position.
 *
 *  public bool Contains(Vector3 pos)
 *      Determine if the given position is the location of a dropping.
 */
public class DroppingManager {

    List<GameObject> droppings;

    /** public DroppingManager()
     * Create a new DroppingManager with no Droppings.
     */
    public DroppingManager() {
        // TODO: Implement
    }

    /** public GameObject CreateDropping(Vector3 pos)
     * Create a Dropping at the given position.
     *
     * Parameters:
     *  pos - position to create dropping at.
     *
     * Preconditions:
     *  pos is not already a space with a dropping.
     */
    public GameObject CreateDropping(Vector3 pos) {
        // TODO: Implement
        return null;
    }

    /** bool Contains(Vector3 pos)
     * Determine if the given position is the location of a dropping.
     *
     * Parameters
     *  pos - position to compare
     *
     * Postconditions
     *  Contains compares pos with every dropping on the game board.
     */
    public bool Contains(Vector3 pos) {
        // TODO: Implement
        return false;
    }
}
