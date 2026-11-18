using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class toppingController : MonoBehaviour{
    public GameObject toppingPanel;
    public bool openOnStart = true;
    public TMP_Text instructTxt;
    public TMP_Text mesgTxt;
    public List<string> neededToppings = new List<string>{"Sprinkles", "Cherry"};
    public GameObject[] allVisuals;

    List<string> added = new List<string>();
    string step = "closed";

    void Start(){
        if(openOnStart){
            OpenMinigame();
        } else {
            CloseMinigame();
        }
    }

    public void OpenMinigame(){
        toppingPanel.SetActive(true);
        added.Clear();
        foreach(GameObject v in allVisuals){
            v.SetActive(false);
        }
        step = "pick";
        mesgTxt.text = "";
        UpdateInstruct();
    }

    public void CloseMinigame(){
        toppingPanel.SetActive(false);
        step = "closed";
    }

    public bool IsDone(){
        return step == "done";
    }

    public void PickTopping(string topping, GameObject visual){
        if(step != "pick"){
            return;
        }
        if(!neededToppings.Contains(topping)){
            mesgTxt.text = "Wrong topping!";
            return;
        }
        if(added.Contains(topping)){
            mesgTxt.text = "Already added!";
            return;
        }
        added.Add(topping);
        if(visual != null){
            visual.SetActive(true);
        }
        if(added.Count == neededToppings.Count){
            step = "done";
            instructTxt.text = "";
            mesgTxt.text = "Toppings done!";
        } else {
            mesgTxt.text = "Added " + topping + "!";
            UpdateInstruct();
        }
    }

    void UpdateInstruct(){
        List<string> left = new List<string>();
        foreach(string t in neededToppings){
            if(!added.Contains(t)){
                left.Add(t);
            }
        }
        instructTxt.text = "Add: " + string.Join(", ", left);
    }
}
