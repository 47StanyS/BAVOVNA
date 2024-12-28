using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class TrigerBridgeFoll : MonoBehaviour
{
    [SerializeField] private PlayableDirector _Director;
    //[SerializeField] private Player _player;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _Director.Play();
            StartCoroutine(boxColliderEnabled());
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(boxColliderEnabled());
        }
    }
    private IEnumerator boxColliderEnabled()
    {
        yield return new WaitForSeconds(10);
        Destroy(gameObject);
    }
}
