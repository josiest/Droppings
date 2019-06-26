using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TestUtil {
    public static string ParseResults(string name, IEnumerable<string> tests) {

        return tests
            .Where(message => !String.IsNullOrEmpty(message))
            .Aggregate("Testing "+name,
                       (message, line) => message + "\n\t" + line,
                       (message) => {

                if (!message.Contains("\n")) {
                    message += "\n\tTest Passed!";
                }
                return message;
            });

    }

    public static IEnumerable<string> Vector2_Test(

            IEnumerable<Vector2> tests, IEnumerable<Vector2> expectedv,
            Func<Vector2, Vector2> testFunc) {

        string failMsg = "Test Failed: input {0} gave {1}, expected {2}.";
        return tests.Zip(expectedv, (test, expected) => {

            Vector2 actual = testFunc(test);
            string message = String.Format(failMsg, test, actual, expected);

            float error = (actual - expected).sqrMagnitude;
            return error < 0.001 ? "" : message;
        });
    }
}
