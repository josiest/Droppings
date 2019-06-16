using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;
using UnityEngine;

public class TestGameController : MonoBehaviour
{
    System.Random rand;

    public GameController gameController;
    delegate string TestFunction();

    void Start()
    {
        rand = new System.Random();
        TestFunction[] tests = {
            () => TestInBounds(),
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

    string TestInBounds() {
        // Test Cases
        Vector2[] bounds = {
            new Vector2(3, 3),      // bounds very small, square
            new Vector2(10, 15),    // bounds regular, m > n
            // bounds arbitrarily large, square
            new Vector2(Int32.MaxValue, Int32.MaxValue),
            new Vector2(30, 10),    // bounds regular, m < n
            new Vector2(50, 50),    // Bounds regular, square
            // bounds arbitrarily large, m > n
            new Vector2(Int32.MaxValue/2, Int32.MaxValue)
        };
        int[] X = {
            1,                              // x in bounds
            0,                              // x just in bounds
            rand.Next(1, Int32.MaxValue-1), // x in bounds
            rand.Next(300, Int32.MaxValue), // x far out of bounds
            rand.Next(0, 50),               // x in bounds
            -1                              // |x| in bounds, negative
        };

        int[] Y = {
            1,                              // y in bounds
            14,                             // y just in bounds
            Int32.MaxValue,                 // y just out of bounds
            0,                              // y just in bounds
            // |y| far out of bounds, negative
            rand.Next(Int32.MinValue, -500),
            rand.Next(1, Int32.MaxValue-1), // y in bounds
        };
        bool[] expected = {true, true, false, false, false, false};

               // Iterate through each test case
        return Enumerable.Range(0, expected.Length)

            /* Transform each index into a string stating if the test
             * passed, or if it failed as well as context
             */
            .Select((i) => {
                gameController.Bounds = bounds[i];
                Vector3 testPoint = new Vector3(X[i], Y[i], 0);

                bool actual = gameController.InBounds(testPoint);
                string message = "Bounds: "+gameController.Bounds.ToString() +
                                 "\n\t\tTest: "+testPoint.ToString() +
                                 "\n\t\tResult: "+actual+" (expected " +
                                 expected[i];

                return actual == expected[i]? "" : message;})

            // Filter out tests that passed
            .Where(message => !String.IsNullOrEmpty(message))

            // Collect all the failures into one string or else Test Passed
            .Aggregate(
                "Test Failed:",
                (message, line) => message + "\n\t\t" + line,
                message => {
                    if (message.Contains("\n")) {
                        return message;
                    }
                    return "Test Passsed!";
                }
            );
    }
}
