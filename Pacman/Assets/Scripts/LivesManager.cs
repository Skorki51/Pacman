using UnityEngine;
using UnityEngine.UI;

public class LivesManager : MonoBehaviour
{
    public static LivesManager instance = null;
    [SerializeField] private PlayerController playerController = null;
    [SerializeField] private Text livesText = null;
    private int lives = 0;

    public int Lives
    {
        get { return lives; }
    }

    private void Awake()
    {
        lives = playerController.PlayerLives;
        instance = this;
    } 

    void Start()
    {
        livesText.text = "Lives: " + lives.ToString();
    }

    public void ReduceLives()
    {
        lives--;
        livesText.text = "Lives: " + lives.ToString();
    }
}
