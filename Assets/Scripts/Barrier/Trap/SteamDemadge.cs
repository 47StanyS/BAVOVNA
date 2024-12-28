using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteamDemadge : MonoBehaviour
{
    private Player player;
    [SerializeField] private float _forseLeft;
    [SerializeField] private float _forseUp;
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
            player = collision.GetComponent<Player>();

            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.left *_forseLeft, ForceMode2D.Impulse);
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * _forseUp, ForceMode2D.Impulse);
            collision.transform.GetChild(0).GetComponent<SpriteRenderer>().color = Color.red;
            Debug.Log("Damage");
            
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
