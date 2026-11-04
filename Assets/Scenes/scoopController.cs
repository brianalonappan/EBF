using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class scoopController : MonoBehaviour{
    public GameObject scoopPanel;     // parent of all the scooping UI, hidden until the minigame opens
    public bool openOnStart = false;  // turn on to test the minigame by itself

    public TMP_Text instructTxt;
    public TMP_Text mesgTxt;

    public string correctFlavor = "Vanilla";

    public RectTransform scoop;       // scoop image that slides over the tub
    public RectTransform scoopBall;   // ice cream ball on the scoop, grows as you scroll

    public float scoopHeight = 60f;   // how high above the tub the scoop sits
    public float slideRange = 80f;    // how far left/right of the tub center it slides
    public float slideSpeed = 200f;   // how fast it slides
    public float perfectRange = 10f;  // this close to center = Perfect
    public float goodRange = 25f;     // this close to center = Good
    public float scrollNeeded = 10f;  // scroll ticks to fill the scoop

    string step = "closed";           // closed, pick, aim, scoop, done
    RectTransform pickedTub;
    float slideTime = 0f;
    float offset = 0f;
    float scrollAmount = 0f;

    void Start(){
        if (openOnStart){
            OpenMinigame();
        }
        else{
            CloseMinigame();
        }
    }

    // Call this when the player interacts with the scooping station.
    public void OpenMinigame(){
        scoopPanel.SetActive(true);
        step = "pick";
        instructTxt.text = "Click the " + correctFlavor + " tub";
        mesgTxt.text = "";
        scoop.gameObject.SetActive(false);
        scoopBall.localScale = Vector3.zero;
    }

    // Call this to leave the minigame and go back to the kitchen.
    public void CloseMinigame(){
        step = "closed";
        scoopPanel.SetActive(false);
    }

    public bool IsDone(){
        return step == "done";
    }

    void Update(){
        if (Mouse.current == null)
            return;

        if (step == "aim"){
            slideTime += Time.deltaTime;
            offset = Mathf.PingPong(slideTime * slideSpeed, slideRange * 2) - slideRange;
            scoop.position = pickedTub.position + new Vector3(offset, scoopHeight, 0);

            if (Mouse.current.leftButton.wasPressedThisFrame){
                CheckAim();
            }
        }
        else if (step == "scoop"){
            float scroll = Mouse.current.scroll.ReadValue().y;
            if (scroll != 0){
                scrollAmount += Mathf.Min(Mathf.Abs(scroll), 1f);
            }

            float progress = Mathf.Clamp01(scrollAmount / scrollNeeded);
            scoopBall.localScale = Vector3.one * progress;

            if (progress >= 1f){
                step = "done";
                instructTxt.text = "";
                mesgTxt.text = "Scoop done!";
            }
        }
    }

    public void PickTub(string flavor, RectTransform tubRect){
        if (step != "pick")
            return;
        if (flavor != correctFlavor){
            mesgTxt.text = "Wrong flavor!";
            return;
        }

        pickedTub = tubRect;
        slideTime = 0f;
        step = "aim";
        scoop.gameObject.SetActive(true);
        instructTxt.text = "Click when the scoop is centered!";
        mesgTxt.text = "";
    }

    void CheckAim(){
        float distance = Mathf.Abs(offset);

        if (distance <= perfectRange){
            mesgTxt.text = "Perfect!";
        }
        else if (distance <= goodRange){
            mesgTxt.text = "Good!";
        }
        else{
            mesgTxt.text = "Missed! Try again.";
            return;
        }

        scoop.position = pickedTub.position + new Vector3(0, scoopHeight, 0);
        scrollAmount = 0f;
        step = "scoop";
        instructTxt.text = "Scroll to scoop!";
    }
}