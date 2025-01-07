using Unity.VisualScripting;
using UnityEngine;

public class DamageOnTrigerEnter2D : MonoBehaviour
{
    private Player player;
    private void FixedUpdate()
    {
        if (player)
        {
            player.TakeDamage(0.1f);
            CameraShake.Instance.ShakeCamera(2f, 0.2f);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.transform.GetChild(0).GetComponent<SpriteRenderer>().color = Color.red; 

        }
        if (collision.gameObject.CompareTag("Ground"))
        {
            Destroy(collision.gameObject);
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Damage");
            player = collision.GetComponent<Player>();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            player = null;
            collision.transform.GetChild(0).GetComponent<SpriteRenderer>().color = Color.white;
        }
    }


}
