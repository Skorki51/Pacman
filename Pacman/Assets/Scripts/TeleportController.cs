using UnityEngine;

public class TeleportController : MonoBehaviour
{
    [SerializeField] private GameObject target = null;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        collision.rigidbody.position = target.transform.position;
    }
}
