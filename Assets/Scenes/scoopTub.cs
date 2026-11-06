
using UnityEngine;

public class scoopTub : MonoBehaviour{
    public string flavor;

    public scoopController game;
    public void ClickTub(){
        game.PickTub(flavor, GetComponent<RectTransform>());
    }
}
