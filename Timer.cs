using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public static Timer instance;

    public GameObject canvas;
    public GameObject endScreen;

    public float timeRemaining = 91;
    public bool timerIsRunning = false;

    bool firstTime = true;

    void Awake() {
        instance = this;
    }

    void Update()
    {
        if (timerIsRunning) {
            if (timeRemaining >= 0) {
                timeRemaining -= Time.deltaTime;
                float minutes = Mathf.FloorToInt(timeRemaining / 60);
                float seconds = Mathf.FloorToInt(timeRemaining % 60) + 1;
                string minutesString = minutes.ToString();
                string secondsString = seconds.ToString();
                if (minutes < 0) {
                    minutesString = "0";
                }
                if (seconds < 10) {
                    secondsString = "0" + secondsString;
                }
                GetComponent<UnityEngine.UI.Text>().text = minutesString + ":" + secondsString;
            }
            else {
                timeRemaining = 0;
                GetComponent<UnityEngine.UI.Text>().text = "0:00";
                //endScreen.SetActive(true);
                if (firstTime) {
                    var panel = Instantiate(endScreen, canvas.transform);
                    firstTime = false;
                }
            }
        }
    }
}
