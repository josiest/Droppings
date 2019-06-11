using System.Linq;
using System.Collections.Generic;
using System;
using System.IO;
using UnityEngine;

public class ScoreBoard {
    string scorePath = "";
    List<string> names;
    List<int> scores;

    /** ScoreBoard()
     * Create a new ScoreBoard object
     *
     */
    public ScoreBoard() {
        scorePath = Path.Combine(Application.dataPath, "Scores.csv");
        names = new List<string>();
        scores = new List<int>();
        LoadScores();
    }

    public override string ToString() {
        return names
            .Zip(scores, (name, hiscore) => name+": "+hiscore)
            .Aggregate("High Scores\n\n", (str, line) => str+line+"\n")
            .TrimEnd('\n');
    }

    public void UpdateScores(int i, string name, int score) {
        names.Insert(i, name);
        scores.Insert(i, score);

        int n = scores.Count;
        if (n > 10) {
            names.RemoveAt(n-1);
            scores.RemoveAt(n-1);
        }
    }

    public int ScoreIndex(int score) {
        return Enumerable.Range(0, 10)
            .DefaultIfEmpty(-1)
            .FirstOrDefault(i => i < scores.Count() && score > scores[i]);
    }

    void LoadScores() {
        string scoreStr = "";
        try {
            // Open and read the scores file
            using (StreamReader scoreStream = new StreamReader(scorePath)) {
                scoreStr = scoreStream.ReadToEnd();

            }
        } catch (Exception e) {
            Debug.Log("Something went wrong. "+e.ToString());
            return;
        }

        // Reset score arrays
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
                Debug.Log("Couldn't Parse Score for "+lineVals[0]);
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
                .Zip(scores, (name, hiscore) => name+", "+hiscore)
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
