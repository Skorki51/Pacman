using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;
    public Text scoreText;
    private int score = 0;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        scoreText.text = "Score: " + score.ToString();
    }

    public void AddPoint()
    {
        score++;
        scoreText.text = "Score: " + score.ToString();
    }
    public void AddPointBoster()
    {
        score = score + 5;
        scoreText.text = "Score: " + score.ToString();
    }
}
