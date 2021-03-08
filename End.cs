using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class End : MonoBehaviour
{
    public Text wordsText;
    public Text pointsText;
    public Text scoreText;
    public Text countText;
    public Text highScore;
    public Text highCount;
    public Text highLetters;

    List<string> wordsPlayedSorted = new List<string>();

    void Start()
    {
        //sort words alphabetically
        Play.instance.wordsPlayed.Sort();

        //sort words by score
        for (int i=0; Play.instance.wordsPlayed.Count > 0; i++) {
            int highScore = 0;
            string bestWord = "";
            for (int j=0; j<Play.instance.wordsPlayed.Count; j++) {
                int currentScore = Play.instance.wordsPlayed[j].Length;
                if (currentScore > highScore) {
                    highScore = currentScore;
                    bestWord = Play.instance.wordsPlayed[j];
                }
            }
            wordsPlayedSorted.Add(bestWord);
            Play.instance.wordsPlayed.Remove(bestWord);
        }

        //set high score data
        if (Play.instance.score > PlayerPrefs.GetInt("highScore")) {
            PlayerPrefs.SetInt("highScore", Play.instance.score);
        }
        if (Play.instance.count > PlayerPrefs.GetInt("highCount")) {
            PlayerPrefs.SetInt("highCount", Play.instance.count);
        }
        if (wordsPlayedSorted.Count > 0) {
            if (wordsPlayedSorted[0].Length > PlayerPrefs.GetInt("highLetters")) {
                PlayerPrefs.SetInt("highLetters", wordsPlayedSorted[0].Length);
            }
        }

        //display high score data
        highScore.text = "High Score: " + PlayerPrefs.GetInt("highScore");
        highCount.text = "Most Words Played: " + PlayerPrefs.GetInt("highCount");
        highLetters.text = "Longest Word: " + PlayerPrefs.GetInt("highLetters") + " letters";

        //display words and scores
        scoreText.text = "SCORE: " + Play.instance.score.ToString();
        countText.text = "WORDS: " + Play.instance.count.ToString();
        for (int i=0; i<wordsPlayedSorted.Count; i++) {
            if (i < wordsPlayedSorted.Count-1) {
                wordsText.text += wordsPlayedSorted[i] + "\n";
                pointsText.text += Play.instance.Scorer(wordsPlayedSorted[i]) + "\n";
            }
            else {
                wordsText.text += wordsPlayedSorted[i];
                pointsText.text += Play.instance.Scorer(wordsPlayedSorted[i]);
            }
        }

    }

    public void NewBoard() {
        PlayerPrefs.SetInt("sameBoard", 0);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void SameBoard() {
        PlayerPrefs.SetInt("sameBoard", 1);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
