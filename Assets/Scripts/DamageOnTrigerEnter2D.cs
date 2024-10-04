using UnityEngine;

public class DamageOnTrigerEnter2D : MonoBehaviour
{
    private Player player;
    private void FixedUpdate()
    {
        if (player)
        {
            player._health --;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.GetComponent<SpriteRenderer>().color = Color.red; 
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
            collision.GetComponent<SpriteRenderer>().color = Color.white;
        }
    }
}
