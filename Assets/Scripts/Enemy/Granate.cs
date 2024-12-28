using UnityEngine;

public class Granate : MonoBehaviour
{
    private int _touchCount;
    [SerializeField] private float _damage;
    //[SerializeField] GameObject _hitEffect;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        _touchCount = _touchCount + 1;
        if(_touchCount >= 4)
        {
            //Instantiate(_hitEffect,transform.position, Quaternion.identity, null);
            Destroy(this.gameObject);
        }
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("<<< DAMAGE >>>");
            collision.gameObject.GetComponent<Player>().TakeDamage(_damage / 2);
            Destroy(this.gameObject);
        }
    }

}
