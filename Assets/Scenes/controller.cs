
using UnityEngine;
using TMPro;

public class controller : MonoBehaviour{
    public TMP_Text timerTxt;
    public TMP_Text mesgTxt;

    public float timeLeft = 30f;
    public string correctFlavor = "Strawberry";

    bool gameOver = false;

    void Update(){
        if (gameOver)
            return;
        timeLeft -= Time.deltaTime;
        timerTxt.text = "Time: " + Mathf.CeilToInt(timeLeft);
        if (timeLeft <= 0){
            gameOver = true;
            timerTxt.text = "Time: 0";
            mesgTxt.text = "Time's Up!";
        }
    }

    public void CheckTub(string flavor, bool empty){
        if (gameOver)
            return;
        if (empty){
            mesgTxt.text = "This tub is empty!";
        }
        else if (flavor != "Strawberry"){
            mesgTxt.text = "Wrong flavor!";
        }
        else{
            mesgTxt.text = "Correct! You found the ice cream!";
            gameOver = true;
        }
    }
}

