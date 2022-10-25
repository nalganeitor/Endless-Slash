using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateMachine : MonoBehaviour
{
    Animator _animator;

    Rigidbody2D _rb2D;

    EnemyBaseState _currentState;
    EnemyStateFactory _states;

    bool _isAttacking = false;

    public EnemyBaseState CurrentState { get { return _currentState; } set { _currentState = value; } }
    public bool IsAttacking { get { return _isAttacking; } }
    public Animator Animator { get { return _animator; } }
    public Rigidbody2D Rb2D { get { return _rb2D; } }
    public float MoveSpeed { get { return _moveSpeed; } }


    float _moveSpeed = 0.05f; 

    void Awake()
    {
        _states = new EnemyStateFactory(this);
        _currentState = _states.Run();
        _currentState.EnterState();

        _animator = GetComponent<Animator>();
        _rb2D = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Rb2D.AddForce(new Vector2(-MoveSpeed, 0), ForceMode2D.Impulse);
    }
}
