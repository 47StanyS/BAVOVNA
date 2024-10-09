using System.Collections;
using UnityEngine;

public class BredgeTriger : MonoBehaviour
{
    [SerializeField] private float _timeHold;
    private Rigidbody2D _rb2D;
   // [SerializeField] private Animator _animator;
    private void Start()
    {
       // _animator = GetComponent<Animator>();
        _rb2D = GetComponent<Rigidbody2D>();
    }
  //  private void Update()
  //  {
  //      _animator.Play("Idle");
  //  }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
           // _animator.Play("Driaba");
            StartCoroutine(canHold());

        }
    }

    private IEnumerator canHold()
    {
        yield return new WaitForSeconds(_timeHold);
       // _animator.enabled = true;
        _rb2D.bodyType = RigidbodyType2D.Dynamic;
        Destroy(gameObject, 10f);
    }
}
