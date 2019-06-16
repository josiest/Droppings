using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TestGameController : MonoBehaviour
{
    public GameController gc;
    delegate string TestFunction();

    void Start()
    {
        TestFunction[] tests = {
            // TestInBounds
            () => {
                // Assume bounds are 10x10
                int[] X = {3, 0, 5, 200};
                int[] Y = {3, 9, -1, 0};
                bool[] expected = {true, true, false, false};

                for (int i = 0; i < expected.Length; i++) {
                    bool actual = gc.InBounds(new Vector3(X[i], Y[i], 0));
                    if (actual != expected[i]) {
                        return "Test Failed!";
                    }
                }
                return "Test Passed";
            }
        };

        string[] funcNames = {"InBounds"};

        string message = tests
            .Zip(funcNames, (test, name) => {
                return "\n\tTesting "+name+"\n\t\t"+test();
            })
            .Aggregate("Testing GameController:",
                       (msg, testResult) => msg + testResult);

        Debug.Log(message);
    }
}
