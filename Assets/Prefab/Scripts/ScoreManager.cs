using TMPro;
using UnityEngine;


public class ScoreManager : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _scoreText;
    public int score;
    public static ScoreManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void AddScore()
    {
        score += 10;
        _scoreText.text = score.ToString();
    }
}
