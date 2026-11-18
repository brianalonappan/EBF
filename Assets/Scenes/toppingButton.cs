using UnityEngine;

public class toppingButton : MonoBehaviour{
    public string topping;
    public GameObject toppingVisual;
    public toppingController game;

    public void ClickTopping(){
        game.PickTopping(topping, toppingVisual);
    }
}
