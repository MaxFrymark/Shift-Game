using System.Collections;
using UnityEngine;

public class GameOverScreen : MonoBehaviour
{
    private InputHandler _inputHandler;
    
    private void OnEnable()
    {
        FirstTimeCheck.SetFirstTime(false);
        _inputHandler = FindAnyObjectByType<InputHandler>();
        StartCoroutine(PauseBeforeReset());
    }

    private IEnumerator PauseBeforeReset()
    {
       yield return new WaitForSeconds(0.5f);
       _inputHandler.OnShiftLane += ResetGame;
    }

    private void ResetGame(int shiftDirection)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene
            (UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
        _inputHandler.OnShiftLane -= ResetGame;
    }

    
}
