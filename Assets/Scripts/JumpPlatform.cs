using UnityEngine;

public class JumpPlatform : MonoBehaviour
{
    [SerializeField]private float _jump = 10;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * _jump, ForceMode2D.Impulse);
        }
    }
}
