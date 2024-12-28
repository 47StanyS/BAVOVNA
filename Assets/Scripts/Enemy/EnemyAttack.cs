using System.Collections;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] private Transform _targetPlayer;
    [SerializeField] private Transform _attackPoint;
    [SerializeField] private LayerMask _playerMask;
    [SerializeField] private float _attackRange;

    [SerializeField] private float _attackSpeed;
    public float _attackEnemyDamage;

    [SerializeField]private Player _player;
    //[SerializeField] private EnemyMove _enemyMove;

    [SerializeField] private bool _canAttack = false;
    private Animator _playerAnimator;

    private Animator _animator;
    private void Awake()
    {
        _playerAnimator = _player.transform.GetChild(0).GetComponent<Animator>();
        _animator = GetComponent<Animator>();
    }
    private void Update()
    {
        if(_targetPlayer != null)
        {
            if (_canAttack == true)
            {

                _animator.Play("Damage");
                StartCoroutine(AttackAnimation());

                _canAttack = false;
                StartCoroutine(AttackCuldown());
              // if (_player._isBlocking == false)
              // {
              //
              //
              // }
               // else
               // {
               //
               //     _animator.Play("Block");
               //     _canAttack = false;
               //     StartCoroutine(AttackCuldown());
               // }

                //_player._isBlocking = false;
            }
        }

        


        //  if (_canAttack == true && _player._isBlocking == true)
        //  {
        //      _animator.Play("Blokc");
        //      _animator.SetBool("_isBlock", true);
        //      _animator.SetBool("_isAttack", false);
        //      _canAttack = false;
        //      StartCoroutine(AttackCuldown());
        //      Debug.Log("DamageIsBlocking");
        //  }
        //  else if(_canAttack == true)
        //  {
        //      _animator.Play("Damage");
        //      _animator.SetBool("_isAttack", true);
        //      _animator.SetBool("_isBlock", false);
        //      StartCoroutine(AttackAnimation());
        //      _canAttack = false;
        //      StartCoroutine(AttackCuldown());
        //  }

    }

    private void Attack()
    {
        
        Collider2D[] _hitPlayer = Physics2D.OverlapCircleAll(_attackPoint.position, _attackRange, _playerMask);
        foreach (Collider2D _playerGround in _hitPlayer)
        {
            
            Debug.Log("attack");
            CameraShake.Instance.ShakeCamera(10f, 0.2f);
            _playerGround.GetComponent<Player>().TakeDamage(_attackEnemyDamage / 2);
            
        }
    }
    private void OnDrawGizmosSelected()
    {
        if(_attackPoint ==  null)
            return;
        Gizmos.DrawWireSphere(_attackPoint.position, _attackRange);
    }
    private IEnumerator AttackCuldown()
    {
        yield return new WaitForSeconds(3f);
        _canAttack = true;
    }
    private IEnumerator AttackAnimation()
    {
        yield return new WaitForSeconds(1.50f);
        Attack();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !collision.isTrigger)
        {
            _targetPlayer = collision.transform;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !collision.isTrigger)
        {
            _targetPlayer = null;
        }
    }
}
