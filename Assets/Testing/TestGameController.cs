using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestGameController : MonoBehaviour
{
    public GameController gc;
    delegate bool TestFunction();

    void Start()
    {
        TestFunction[] test = {
            // TestInBounds
            () => {
                // Assume bounds are 10x10
                int[] X = {3, 0, 5, 200};
                int[] Y = {3, 9, -1, 0};
                bool[] expected = {true, true, false, false};

                for (int i = 0; i < expected.Length; i++) {
                    bool actual = gc.InBounds(new Vector3(X[i], Y[i], 0));
                    if (actual != expected[i]) {
                        return false;
                    }
                }
                return true;
            }
        };

        string[] funcName = {"InBounds"};

        for (int i = 0; i < test.Length; i++) {
            Debug.Log("Testing "+funcName[i]);
            if (test[i]()) {
                Debug.Log("\tTest Passed!");
            } else {
                Debug.Log("\tTest Failed!");
            }
        }
    }
}
