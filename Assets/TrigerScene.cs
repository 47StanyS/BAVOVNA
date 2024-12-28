using UnityEngine;
using UnityEngine.SceneManagement;

public class TrigerScene : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Time.timeScale = 0f;
            SceneManager.LoadScene(3); 
        }
    }
}
