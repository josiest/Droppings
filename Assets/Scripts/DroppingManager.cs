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
 *  public void CreateDropping(Vector3 pos)
 *      Create a Dropping at the given position.
 *
 *  public bool Contains(Vector3 pos)
 *      Determine if the given position is the location of a dropping.
 */
public class DroppingManager {

    List<GameObject> droppings;
    
    public Sprite droppingSprite;
    public int Count { get => droppings.Count; }

    /** public DroppingManager()
     * Create a new DroppingManager with no Droppings.
     */
    public DroppingManager() {
        droppings = new List<GameObject>();
    }

    /** public void CreateDropping(Vector3 pos)
     * Create a Dropping at the given position.
     *
     * Parameters:
     *  pos - position to create dropping at.
     *
     * Preconditions:
     *  pos is not already a space with a dropping, and there is still room
     *  on the board for a dropping.
     */
    public void CreateDropping(Vector3 pos) {
        int i = droppings.Count;
        GameObject dropping = new GameObject("Dropping"+(i+1),
                                             typeof(SpriteRenderer));

        dropping.transform.position = pos;
        SpriteRenderer pooRend = dropping.GetComponent<SpriteRenderer>();
        pooRend.sprite = droppingSprite;

        droppings.Add(dropping);
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

    public static string TestConstructor() {
        DroppingManager test = new DroppingManager();
        if (test.droppings == null) {
            return "Test Failed: droppings never initialized.";
        }
        if (test.droppings.Count > 0) {
            return "Test Failed: droppings isn't empty.";
        }
        return "Test Passed";
    }

    public static string TestCreateDropping(Sprite droppingSprite) {

        DroppingManager dm = new DroppingManager();
        dm.droppingSprite = droppingSprite;

        dm.CreateDropping(Vector3.zero);
        GameObject dropping = dm.droppings[dm.droppings.Count-1];

        SpriteRenderer pooRend = dropping.GetComponent<SpriteRenderer>();
        if (pooRend == null) {
            return "Test Failed: null renderer";
        }
        if (pooRend.sprite != droppingSprite) {
            return "Test Failed: incorrect sprite";
        }
        Vector3 error = dropping.transform.position - Vector3.zero;
        if (Vector3.Magnitude(error) > 0.0001) {
            return "Test Failed: incorrect position for zero";
        }

        dm.CreateDropping(Vector3.right);
        dropping = dm.droppings[dm.droppings.Count-1];

        error = dropping.transform.position - Vector3.right;
        if (Vector3.Magnitude(error) > 0.0001) {
            return "Test Failed: incorrect position for right";
        }

        Vector3 test = new Vector3(20, 5, 0);
        dm.CreateDropping(test);
        dropping = dm.droppings[dm.droppings.Count-1];

        error = dropping.transform.position - test;
        if (Vector3.Magnitude(error) > 0.0001) {
            return "Test Failed: incorrect position for {20, 5, 0}";
        }

        return "Test Passed";
    }
}
