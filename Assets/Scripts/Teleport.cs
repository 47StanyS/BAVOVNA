using UnityEngine;

public class Teleport : MonoBehaviour
{
    public Vector3 _respawnPoint = new Vector3(0, 0, 0);


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.transform.position = _respawnPoint; 
        }
    }
}
