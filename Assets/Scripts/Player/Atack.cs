using System.Collections;
using UnityEngine;

public class Atack : MonoBehaviour
{

    [SerializeField] private Transform _attackPoint;
    [SerializeField] private float _attackRange;


    [SerializeField] private LayerMask _enemyLayers;
    public float _attackDamage = 40;

    [SerializeField]private bool _canAttack = true;

    [SerializeField] private Player _player;
    private Animator _animator;
    private AudioSource _audioSource;
    [SerializeField] AudioClip _attackClip;
    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _animator = transform.GetChild(0).GetComponent<Animator>();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && _canAttack == true && _player._isBlocking == false)
        {
            _audioSource.PlayOneShot(_attackClip);
            _animator.Play("Attack");
            Attack();
            _canAttack = false;
            StartCoroutine(AttackCuldown());
        }

    }

    private void Attack()
    {
        // Play an attack animation

        //detect enemis in range of attack
        Collider2D[] _hitEnemies = Physics2D.OverlapCircleAll(_attackPoint.position, _attackRange, _enemyLayers);

        foreach(Collider2D _enemyGround in _hitEnemies)
        {
            //if(!collision.isTrigger)
            CameraShake.Instance.ShakeCamera(2f, 0.1f);
            _enemyGround.GetComponent<EnemyHealth>().UpdateHealth(_attackDamage);
        }

    }

    //culdawn attack
    private IEnumerator AttackCuldown()
    {
        yield return new WaitForSeconds(1);
        _canAttack = true;
    }
    private void OnDrawGizmosSelected()
    {
        if (_attackPoint == null)
            return;
        Gizmos.DrawWireSphere(_attackPoint.position, _attackRange);
    }
}
