using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;

    public void StartMoving(float speed)
    {
        rb.linearVelocity = Vector2.down * speed;
    }
}
