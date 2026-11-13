using System.Collections;
using TMPro;
using UnityEngine;

public class OpeningMenu : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private Spawner spawner;

    private InputHandler inputHandler;
    
    private bool leftShiftPressed = false;
    private bool rightShiftPressed = false;
        
    private void Start()
    {
        if(!FirstTimeCheck.IsFirstTime)
        {
            leftShiftPressed = true;
            rightShiftPressed = true;
            CheckIfReady();
        }
    }


    public void SubscribeToInputHandler(InputHandler inputHandler)
    {
        this.inputHandler = inputHandler;
        inputHandler.OnShiftLane += ShiftLane;
    }

    private void ShiftLane(int shiftDirection)
    {
        if(shiftDirection == -1)
        {
            LeftShiftPressed();
        }
        else if(shiftDirection == 1)
        {
            RightShiftPressed();
        }
    }

    private void LeftShiftPressed()
    {
        if(leftShiftPressed) {return;}
        leftShiftPressed = true;
        if(CheckIfReady()) {return;}
        text.text = "Press Right Shift";
    }
    
    private void RightShiftPressed()
    {
        if(rightShiftPressed) {return;}
        rightShiftPressed = true;
        if(CheckIfReady()) {return;}
        text.text = "Press Left Shift";
    }

    private bool CheckIfReady()
    {
        if(leftShiftPressed && rightShiftPressed)
        {
            text.text = "Ready?";
            StartCoroutine(GetReadyCoroutine());
        }
        return leftShiftPressed && rightShiftPressed;
    }

    private void StartGame()
    {
        text.text = "Go!";
        StartCoroutine(StartGameCoroutine());
    }

    private IEnumerator GetReadyCoroutine()
    {
        yield return new WaitForSeconds(1f);
        StartGame();

    }

    private IEnumerator StartGameCoroutine()
    {
        spawner.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        gameObject.SetActive(false);
    }

    void OnDisable()
    {
        inputHandler.OnShiftLane -= ShiftLane;
    }

}
