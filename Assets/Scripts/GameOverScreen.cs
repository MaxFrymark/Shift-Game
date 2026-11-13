using System.Collections;
using UnityEngine;

public class GameOverScreen : MonoBehaviour
{
    private void OnEnable()
    {
        FirstTimeCheck.SetFirstTime(false);
        StartCoroutine(PauseBeforeReset());
    }

    private IEnumerator PauseBeforeReset()
    {
       yield return new WaitForSeconds(0.5f);
       FindAnyObjectByType<InputHandler>().OnShiftLane += ResetGame;
    }

    private void ResetGame(int shiftDirection)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene
            (UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
    }
}
