using TMPro;
using UnityEngine;

public class OpeningMenu : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private Spawner spawner;
    
    private bool leftShiftPressed = false;
    private bool rightShiftPressed = false;
    
    void Start()
    {
        
    }
    
    public void LeftShiftPressed()
    {
        if(leftShiftPressed) {return;}
        text.text = "Press Right Shift";
        leftShiftPressed = true;
    }
    
    public void RightShiftPressed()
    {
        if(rightShiftPressed) {return;}
        text.text = "Press Left Shift";
        rightShiftPressed = true;
    }

    private void CheckIfReady()
    {
        if(leftShiftPressed && rightShiftPressed)
        {
            text.text = "Ready?";
        }
    }

    private void StartGame()
    {
        text.text = "Go!";
    }
    
}
