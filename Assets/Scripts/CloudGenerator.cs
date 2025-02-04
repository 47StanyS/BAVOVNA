using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudGenerator : MonoBehaviour
{
    [SerializeField] GameObject[] clouds;
    [SerializeField] float spawnInterval;

    [SerializeField] GameObject endPoint;
   

    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
        Prewarm();
        Invoke("AttemptSpawn", spawnInterval);

    }


    void SpawnCloud(Vector3 startPos)
    {
        int randomIndex = UnityEngine.Random.Range(0, clouds.Length);
        GameObject cloud = Instantiate(clouds[randomIndex]);

        float startY = UnityEngine.Random.Range(startPos.y - 3f, startPos.y + 3f);

        cloud.transform.position = new Vector3(startPos.x, startY, startPos.z);

        float scale = UnityEngine.Random.Range(0.5f, 1f);
        cloud.transform.localScale = new Vector2(scale, scale);

        float speed = UnityEngine.Random.Range(0.1f, 1f);
        cloud.GetComponent<Cloud>().StartFloating(speed, endPoint.transform.position.x);
    }
      void AttemptSpawn()
      {
        //check some things.
        SpawnCloud(startPos);

        Invoke("AttemptSpawn", spawnInterval);
    }

    void Prewarm()
    {
        for (int i = 0; i < 10; i++)
        {
            Vector3 spawnPos = startPos + Vector3.right * (i * 2);
            SpawnCloud(spawnPos);
        }
    }
}
