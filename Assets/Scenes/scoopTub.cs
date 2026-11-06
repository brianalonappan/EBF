using UnityEngine;
using UnityEngine.UI;

public class scoopTub : MonoBehaviour{
    public string flavor;
    public Color flavorColor = Color.white;

    public scoopController game;

    void Start(){
        // color the tub button to match its flavor
        GetComponent<Image>().color = flavorColor;
    }

    public void ClickTub(){
        game.PickTub(flavor, flavorColor, GetComponent<RectTransform>());
    }
}