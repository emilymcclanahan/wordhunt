using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Play : MonoBehaviour
{
    public static Play instance;

    public Sprite[] sprites;
    public Button[] buttons;
    public Image[] background;
    char[] letters = new char[] {'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z'};
    int[] letterIndeces = new int[16];
    public string lettersOnBoard;

    public Text wordText;
    string wordTemp;
    public int letter;
    public int previousTile;
    public int tile;
    public List<int> tileHistory = new List<int>();
    public bool onBoard;

    public List<string> wordsPlayed = new List<string>();
    public Text counterText;
    public int count;
    public Text scoreText;
    public int score;

    void Awake() {
        instance = this;
    }

    void Start()
    {
        //set word to blank
        wordText.text = "";
        if (PlayerPrefs.GetInt("sameBoard") == 0) {
            //randomly generate letters based on their frequency
            for (int i=0; i<buttons.Length; i++) {
                int random = Random.Range(0,1000);
                int letterIndex;
                if (random < 85) { //A
                    letterIndex = 0;
                }
                else if (random < 105) { //B
                    letterIndex = 1;
                }
                else if (random < 150) { //C
                    letterIndex = 2;
                }
                else if (random < 184) { //D
                    letterIndex = 3;
                }
                else if (random < 296) { //E
                    letterIndex = 4;
                }
                else if (random < 314) { //F
                    letterIndex = 5;
                }
                else if (random < 339) { //G
                    letterIndex = 6;
                }
                else if (random < 369) { //H
                    letterIndex = 7;
                }
                else if (random < 444) { //i
                    letterIndex = 8;
                }
                else if (random < 446) { //j
                    letterIndex = 9;
                }
                else if (random < 457) { //k
                    letterIndex = 10;
                }
                else if (random < 512) { //l
                    letterIndex = 11;
                }
                else if (random < 542) { //m
                    letterIndex = 12;
                }
                else if (random < 608) { //n
                    letterIndex = 13;
                }
                else if (random < 680) { //o
                    letterIndex = 14;
                }
                else if (random < 712) { //p
                    letterIndex = 15;
                }
                else if (random < 714) { //q
                    letterIndex = 16;
                }
                else if (random < 790) { //r
                    letterIndex = 17;
                }
                else if (random < 847) { //s
                    letterIndex = 18;
                }
                else if (random < 917) { //t
                    letterIndex = 19;
                }
                else if (random < 953) { //u
                    letterIndex = 20;
                }
                else if (random < 963) { //v
                    letterIndex = 21;
                }
                else if (random < 976) { //w
                    letterIndex = 22;
                }
                else if (random < 979) { //x
                    letterIndex = 23;
                }
                else if (random < 997) { //y
                    letterIndex = 24;
                }
                else { //z
                    letterIndex = 25;
                }

                buttons[i].gameObject.GetComponent<Image>().sprite = sprites[letterIndex];
                letterIndeces[i] = letterIndex;
                lettersOnBoard += letters[letterIndex];
            }
            PlayerPrefsX.SetIntArray("letterIndeces", letterIndeces);
        }
        else {
            letterIndeces = PlayerPrefsX.GetIntArray("letterIndeces");
            for (int i=0; i<buttons.Length; i++) {
                buttons[i].gameObject.GetComponent<Image>().sprite = sprites[letterIndeces[i]];
                lettersOnBoard += letters[letterIndeces[i]];
            }
            PlayerPrefs.SetInt("sameBoard", 0);
        }
    }

    void Update()
    {
        //user is holding mouse down
        if(Input.GetMouseButton(0) && onBoard){
            //if tile hasn't been used before, add it
            if (buttons[tile].interactable) {
                wordTemp += letters[letter];
                buttons[tile].interactable = false;
                previousTile = tile;
                if (!tileHistory.Contains(tile)) {
                    tileHistory.Add(tile);
                }
            }
            //make sure it isn't the tile we are currently on
            else if (previousTile != tile) {
                //if it is a previous tile, go back to that tile
                for (int i=tileHistory.Count-1; i>=0 ; i--) {
                    if (tile != tileHistory[i]) {
                        buttons[tileHistory[i]].interactable = true;
                        tileHistory.RemoveAt(tileHistory.Count-1);
                        wordTemp = wordTemp.Remove(wordTemp.Length - 1, 1);
                    }
                    else {
                        break;
                    }
                }
            }
        }
        else if (onBoard){ //still on board but not holding down
            //add word to list of played words, add to score, add to word counter
            if (IsValidWord(wordTemp) && IsNewWord(wordTemp)) {
                wordsPlayed.Add(wordTemp);
                score += Scorer(wordTemp);
                scoreText.text = "SCORE: " + score.ToString();
                count++;
                counterText.text = "WORDS: " + count.ToString();
            }

            //reset word
            wordTemp = "";

            //reset tile tile history
            tileHistory = new List<int>();

            //sets all tiles to active
            for(int i=0; i<buttons.Length; i++) {
                buttons[i].interactable = true;
            }
        }
        else { //not on board
            //reset word
            wordTemp = "";

            //reset tile tile history
            tileHistory = new List<int>();

            //sets all tiles to active
            for(int i=0; i<buttons.Length; i++) {
                buttons[i].interactable = true;
            }
        }
        //update the text of the word on screen
        wordText.text = wordTemp;
        if (IsValidWord(wordTemp)) {
            if (IsNewWord(wordTemp)) {
                wordText.color = Color.green;
                for (int i=0; i<buttons.Length; i++) {
                    //check which tiles have been used, highlight them green
                    if (!buttons[i].interactable) {
                        background[i].color = Color.green;
                        buttons[i].gameObject.transform.GetChild(0).GetComponent<Image>().color = Color.green;
                        buttons[i].gameObject.transform.GetChild(1).GetComponent<Image>().color = Color.green;
                    }
                }
            }
            else {
                wordText.color = Color.yellow;
                //check which tiles have been used, highlight them yellow
                for (int i=0; i<buttons.Length; i++) {
                    if (!buttons[i].interactable) {
                        background[i].color = Color.yellow;
                        buttons[i].gameObject.transform.GetChild(0).GetComponent<Image>().color = Color.yellow;
                        buttons[i].gameObject.transform.GetChild(1).GetComponent<Image>().color = Color.yellow;
                    }
                }
            }
        }
        //reset colors
        else {
            wordText.color = Color.white;
            for (int i=0; i<buttons.Length; i++) {
                background[i].color = Color.white;
                buttons[i].gameObject.transform.GetChild(0).GetComponent<Image>().color = Color.white;
                buttons[i].gameObject.transform.GetChild(1).GetComponent<Image>().color = Color.white;
            }
        }
    }

    public bool IsValidWord (string word) {
        if (word == null) {
            return false;
        }
    		if (AllWords.instance.allWords.Contains(word.ToLower()) && word.Length > 2) {
            return true;
        }
        return false;
	  }

    public bool IsNewWord (string word) {
        if (!wordsPlayed.Contains(word)) {
            return true;
        }
        return false;
    }

    public int Scorer(string word) {
        int numLetters = word.Length;
        if (numLetters > 5) {
            return (numLetters - 3) * 400 + 200;
        }
        else if (numLetters > 3) {
            return (numLetters - 3) * 400;
        }
        return 100;
    }
}

// ideas:
// make lines between tiles
// multiplayer mode: input number of players and thats how many times you play. At the end, all scores show up next to each other.
// animate score counter
// make seeds for the same board
// find all possible words
