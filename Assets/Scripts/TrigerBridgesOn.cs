using UnityEngine;

public class TrigerBridgesOn : MonoBehaviour
{
    public BridgesDestroy bridgesDestroy;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            bridgesDestroy.OnDestroy = true;
        }
    }
}
