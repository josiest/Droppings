using System.Linq;
using System.Collections.Generic;
using System;
using System.IO;
using UnityEngine;

public class ScoreBoard : MonoBehaviour {
    string scorePath = "";
    List<string> names;
    List<int> scores;
    string failureMessage = "";

    /** ScoreBoard()
     * Create a new ScoreBoard object
     * 
     * Reads from Scores.csv. If it can't read from Scores.csv, ToString will
     * display an error message instead of high scores.
     */
    public ScoreBoard() {
        // Create the path to load from Scores.csv
        try {
            scorePath = Path.Combine(Application.dataPath, "Scores.csv");

        // Temporarily disable warning: unused variable declaration
        #pragma warning disable 0168
        // In case Unity dataPath is null for some reason
        } catch (ArgumentNullException e) {
        #pragma warning restore 0168

            failureMessage = "Could not find Scores.csv:\n" +
                             "Unity data path is null!";

        // Temporarily disable warning: unused variable declaration
        #pragma warning disable 0168
        // In case path contains ilegal characters for some reason
        } catch (ArgumentException e) {
        #pragma warning restore 0168

            failureMessage = "Could not find Scores.csv:\n" +
                             "Incompatible Unity data path:\n" +
                             Application.dataPath;
        }

        names = new List<string>();
        scores = new List<int>();
        LoadScores();
    }

    /** public string ToString()
     * Return the string representation of the scoreboard
     *
     * Has the format:
     *
     * High Scores
     *
     * name1: score1
     * name2: score2
     * ...
     * nameN: scoreN
     *
     * where nameN and scoreN are names that users have inputed and their
     * respective scores
     *
     * Exceptions:
     *  ArgumentNullException - if names or scores is somehow null
     */
    public override string ToString() {

        // If ScoreBoard failed to read the scores, dsiplay error message
        if (!String.IsNullOrEmpty(failureMessage)) {
            return failureMessage;
        }

        return names
            // Put each name and respective score together
            .Zip(scores, (name, score) => name+": "+score)
            // Combine all the name-score lines together
            .Aggregate("High Scores\n\n", (str, line) => str+line+"\n")
            .TrimEnd('\n');
    }

    /** public void UpdateScores(int i, string name, int score)
     * Update the scoreboard with name and score.
     *
     * Parameters
     *  i        - index of the score on the scoreboard
     *  name     - name of the player with the high score
     *  score    - respective score of the player
     *
     * Adds the name and the score at index i. If the length of the scores is
     * greater than 10 after adding, the last score will be dropped.
     *
     * Assumes:
     *  i is in [0, 9]
     */
    public void UpdateScores(int i, string name, int score) {
        names.Insert(i, name);
        scores.Insert(i, score);

        // Remove last score if scores has more than 10 scores
        int n = scores.Count;
        if (n > 10) {
            names.RemoveAt(n-1);
            scores.RemoveAt(n-1);
        }
    }

    /** public int ScoreIndex(int score)
     * Give the index in the score list of this score
     *
     * Parameters
     *  score - to compare with list of scores
     *
     * Returns
     *  an integer from 0-9 correspodning to the index of the score list
     *  the score should be placed in. If the score is not a high score,
     *  returns 0.
     */
    public int ScoreIndex(int score) {
        // range of indices (from 1 to 10)
        return Enumerable.Range(1, 10)
            // grab the first index which has lower score than current
            .FirstOrDefault(i => {
                return (i-1) < scores.Count && score > scores[i-1];
            }) - 1;
    }

    /** void LoadScores()
     * Load the scores from the csv file
     *
     * Loads the scores from Assets/Scores.csv and replaces whatever is stored
     * in names and scores with the new scores.
     *
     * Side Effects:
     *  names and scores are cleared if score file is successfully opened.
     *  If not, or if scores contains a non-integer value, ToString will
     *  display an error message.
     */
    void LoadScores() {
        // store file in a string so the file can be closed right away
        string scoreStr = "";
        try {
            // Open and read the scores file
            using (StreamReader scoreStream = new StreamReader(scorePath)) {
                scoreStr = scoreStream.ReadToEnd();
            }
        #pragma warning disable 0168
        } catch (FileNotFoundException e) {
        #pragma warning restore 0168

            failureMessage = "Could not find Scores.csv in data path:\n" +
                             Application.dataPath;
            return;

        #pragma warning disable 0168
        } catch (DirectoryNotFoundException e) {
        #pragma warning restore 0168

            failureMessage = "Could not find data path:\n" +
                             Application.dataPath;
            return;
        }

        // Reset names and scores
        names.Clear();
        scores.Clear();

        // Split the file by line
        string[] lines = scoreStr.Split(new Char[] {'\n'});

        foreach (string line in lines) {

            // Split each line by comma
            string[] lineVals = line.Split(new Char[] {','});

            // In case Scores.csv is empty
            if (String.IsNullOrEmpty(lineVals[0])) {
                break;
            }

            // second column contains scores so parse to int and save
            // to scores array
            int nextScore;
            if (!Int32.TryParse(lineVals[1], out nextScore)) {
                failureMessage = "Could not parse score for " + lineVals[0] +
                                 ": " + lineVals[1];
                return;
            }
            scores.Add(nextScore);

            // first column contains names so save to names array
            names.Add(lineVals[0]);
        }
    }

    void WriteScores() {
        try {
            // Combine the names and scores into a string
            string scoreStr = names
                // Put each name and score together, delimited by comma
                .Zip(scores, (name, score) => name+", "+score)
                // Put all of the lines together into one string
                .Aggregate((str, line) => str+line+"\n")
                .TrimEnd('\n');

            // Write to Scores.csv
            File.WriteAllText(scorePath, scoreStr);

        // If there are no scores, don't do anything
        #pragma warning disable 0168
        } catch (InvalidOperationException e) {
        #pragma warning restore 0168
            return;

        // Something else went wrong
        } catch (Exception e) {
            Debug.Log("Something went wrong when saving scores.\n"
                      +e.ToString());
        }
    }
}
