using System.Collections;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [HideInInspector]
    public EnemyBrain enemyBrain;

    [SerializeField] private Transform _targetPlayer;
    [SerializeField] private Transform _attackPoint;
    [SerializeField] private LayerMask _playerMask;
    [SerializeField] private float _attackRange;
    public float _distanceToAttack;

    [SerializeField] private float _attackSpeed;
    public float _attackEnemyDamage;

    public bool _canAttack = false;
    public bool _attacking;

    public void Attack()
    {
        if (_canAttack) 
        {
            _attacking = true;
            enemyBrain._animator.SetBool("Attacking", _attacking);

            StartCoroutine(AttackDelay());

            _canAttack = false;
        }
    }

    public void HitZone()
    {
        Collider2D[] _hitPlayer = Physics2D.OverlapCircleAll(_attackPoint.position, _attackRange, _playerMask);
        foreach (Collider2D _playerGround in _hitPlayer)
        {
            
            Debug.Log("attack");
            CameraShake.Instance.ShakeCamera(10f, 0.2f);
            _playerGround.GetComponent<Player>().TakeDamage(_attackEnemyDamage / 2);
        }
    }

    private IEnumerator AttackDelay()
    {
        yield return new WaitForSeconds(1.5f);
        HitZone();

        Debug.Log("HitZone");

        yield return new WaitForSeconds(1.5f);
        _canAttack = true;

        Debug.Log("canAttack = true");

        _attacking = false;
        enemyBrain._animator.SetBool("Attacking", _attacking);
    }

    private void OnDrawGizmosSelected()
    {
        if (_attackPoint == null)
            return;
        Gizmos.DrawWireSphere(_attackPoint.position, _attackRange);
    }
}
