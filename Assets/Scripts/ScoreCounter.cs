using TMPro;
using UnityEngine;

public class ScoreCounter : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _scoreText;
    
    private int _score = 0;
    
    public void IncreaseScore()
    {
        _score++;
        _scoreText.text = _score.ToString();
    }
}
