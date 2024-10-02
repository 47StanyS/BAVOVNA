using UnityEngine;

public class BlockPostDor : MonoBehaviour
{
    [SerializeField] SpriteRenderer _spriteRender;

    private void Start()
    {
        _spriteRender = GetComponent<SpriteRenderer>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.gameObject.CompareTag("Player"))
        {
            _spriteRender.enabled = false;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _spriteRender.enabled = true;
        }
    }
}
