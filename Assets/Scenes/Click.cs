
using UnityEngine;
using UnityEngine.SceneManagement;

public class Click : MonoBehaviour
{
    void OnMouseDown()
    {
        SceneManager.LoadScene("RefillMinigame");
    }
}
