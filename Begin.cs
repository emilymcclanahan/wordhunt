using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Begin : MonoBehaviour
{
    public InputField inputField;

    public void StartGame() {
        string inputString = inputField.text;
        if (int.TryParse(inputString, out int inputInt)) {
            if (inputInt > 0) {
                Timer.instance.timeRemaining = inputInt;
                gameObject.SetActive(false);
                Timer.instance.timerIsRunning = true;
            }
        }
    }
}
