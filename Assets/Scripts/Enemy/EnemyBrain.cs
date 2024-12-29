using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBrain : MonoBehaviour
{
    public enum State 
    {
        Idle,
        Run,
        Attack
    }

    public State CurrentState;

    public Transform target;

    private EnemyMove _enemyMove;
    private EnemyAttack _enemyAttack;
    private EnemyHealth _enemyHealth;

    [HideInInspector] public Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();

        _enemyMove = GetComponent<EnemyMove>();
        _enemyAttack = GetComponent<EnemyAttack>();
        _enemyHealth = GetComponent<EnemyHealth>();

        _enemyMove.enemyBrain = this;
        _enemyAttack.enemyBrain = this;
        _enemyHealth.enemyBrain = this;
    }

    public void FixedUpdate()
    {
        //State Selector
        if (target == null && _enemyAttack._attacking == false)
        {
            CurrentState = State.Idle;
        }
        else if(target)
        {
            float distance = Vector2.Distance(transform.position, target.position);

            if (_enemyAttack._attacking == false && distance > _enemyAttack._distanceToAttack)
            {
                CurrentState = State.Run;
            }
            else
            {
                CurrentState = State.Attack;
            }
        }

        //Behavior
        if (CurrentState == State.Idle) 
        {
            _enemyMove.WalkOnPoints();
        }
        else if(CurrentState == State.Run)
        {
            _enemyMove.TargetFolow();
        }
        else if (CurrentState == State.Attack)
        {
            _enemyAttack.Attack();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !collision.isTrigger)
        {
            target = collision.transform;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !collision.isTrigger)
        {
            target = null;
        }
    }
}
