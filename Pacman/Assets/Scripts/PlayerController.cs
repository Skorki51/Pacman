using UnityEngine;
using UnityEngine.SceneManagement;


public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private float moveMultipler = 50f;
    private Vector3 playerRotation = new Vector3();
    private Vector2 playerFoceDirection;
    private Vector2 playerStartPosition;

    [SerializeField] private int pointsLeft = 0;
    private int bosterLeft;
    private int playerLives = 3;

    [SerializeField] private AudioSource pointGetSound;
    [SerializeField] private AudioSource bosterGetSound;

    public int PlayerLives
    {
        get { return playerLives; }
    }

    void Start()
    {
        if (rb == null) { rb = transform.GetComponent<Rigidbody2D>(); }
        if (spriteRenderer == null) { spriteRenderer = gameObject.GetComponent<SpriteRenderer>(); }

 
        playerStartPosition = rb.position;
    }
    void FixedUpdate()
    {
        Move();
    }

    void Update()
    {
        BackToMenu();
    }

    void Move()
    {
        LayerMask mask = LayerMask.GetMask("Wall");
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            TryMoveAfterButtonDown(Vector2.up, mask, 90f);
        }

        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            TryMoveAfterButtonDown(Vector2.down, mask, 270f);
        }

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            TryMoveAfterButtonDown(Vector2.left, mask, 180f);
        }

        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            TryMoveAfterButtonDown(Vector2.right, mask, 0f);
        }

        transform.rotation = Quaternion.Euler(playerRotation);
        rb.AddForce(playerFoceDirection);
    }

    private void TryMoveAfterButtonDown(Vector2 directionVector2 , LayerMask mask, float rotationZ) {

        RaycastHit2D raycastHit = Physics2D.Raycast(transform.position, directionVector2, 1f, mask);
        if (raycastHit.collider == null)
        {
            playerRotation.z = rotationZ;
            playerFoceDirection = directionVector2 * Time.fixedDeltaTime * moveMultipler;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "point")
        {
            Destroy(collision.gameObject);
            ScoreManager.Instance.AddPoint();
            pointGetSound.Play();
            pointsLeft--;
        }
        if (collision.gameObject.tag == "boster")
        {
            Destroy(collision.gameObject);
            ScoreManager.Instance.AddPointBoster();
            bosterGetSound.Play();
            bosterLeft--;
        }
        if (collision.gameObject.tag == "enemy")
        {
            if(LivesManager.instance.Lives > 1)
            {
                LivesManager.instance.ReduceLives();
                rb.position = playerStartPosition;
            }
            else
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
        if (pointsLeft+bosterLeft < 1)
        {
            if(SceneManager.GetActiveScene().buildIndex + 1 > 5)
            {
                SceneManager.LoadScene(0);
            }
            else
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
        }
    }

    private void BackToMenu()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(0);
        }
    }

}
