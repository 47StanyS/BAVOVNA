
using UnityEngine;

public class TriggerBarrier : MonoBehaviour
{
    public OpenBarrier OpenBarrier;

    public OffCollider OffCollider;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("DoorsOpen");
            OpenBarrier.Open = true;

            OffCollider.Off= true;

        }
        
    }
}
