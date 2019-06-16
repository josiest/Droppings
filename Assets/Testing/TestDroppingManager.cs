using UnityEngine;

public class TestDroppingManager : MonoBehaviour
{
    public Sprite droppingSprite;

    public void Start() {
        string message = "Testing DroppingManager\n\tTesting Constructor:\n";
        message += "\t\t" + DroppingManager.TestConstructor() + "\n";

        message += "\tTesting CreateDropping\n";
        message += "\t\t" + DroppingManager.TestCreateDropping(droppingSprite)
                   + "\n";

        Debug.Log(message);
    }
}
