
using UnityEngine;

public class tub : MonoBehaviour{
    public string flavor;
    public bool empty;

    public controller game;
    public void ClickTub(){
        game.CheckTub(flavor, empty);
    }
}
