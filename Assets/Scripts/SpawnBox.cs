using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBox : MonoBehaviour
{
    [SerializeField] private Transform _spawnBoxPoints;
    [SerializeField] private GameObject _spawnBox;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            Debug.Log("collisionOn");
            Instantiate(_spawnBox,_spawnBoxPoints.transform.position, Quaternion.identity);
        }
    }
}
