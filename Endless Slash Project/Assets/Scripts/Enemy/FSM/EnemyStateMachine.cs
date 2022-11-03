//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class EnemyStateMachine : MonoBehaviour
//{
//    Animator _animator;
//    [SerializeField] GameObject _chestModel;
//    [SerializeField] GameObject _enemy;
//    [SerializeField] GameObject _player;
//    [SerializeField] LayerMask _enemyLayer;
//    [SerializeField] Transform _attackPoint;

//    [SerializeField]bool _canAttack = false;
//    bool _chestDrop = false;
//    [SerializeField] bool _isDead = false;
//    [SerializeField] bool _isEnemyNear = false;

//    Coroutine _attackRoutine = null;

//    float _attackCooldown = 5f;
//    float _attackRange = 0.3f;
//    float _distance;

//    float _attackDamage = 40;
//    int _attackRangeHash;
//    [SerializeField]float _currentHealth;
//    int _isAttackingHash;
//    int _isDeadHash;
//    int _isHurtHash;
//    int _isRunningHash;
//    int _maxHealth = 200;

//    Vector3 _position;

//    EnemyBaseState _currentState;
//    EnemyStateFactory _states;

//    public Animator Animator { get { return _animator; } }
//    public GameObject Enemy { get { return _enemy; } }
//    public GameObject Player { get { return _player; } }
//    public LayerMask EnemyLayer { get { return _enemyLayer; } }
//    public Transform AttackPoint { get { return _attackPoint; } }
//    public bool CanAttack { get { return _canAttack; } set { _canAttack = value; } }
//    public bool IsDead { get { return _isDead; } set { _isDead = value; } }
//    public bool IsEnemyNear { get { return _isEnemyNear; } }
//    public Coroutine AttackRoutine { get { return _attackRoutine; } set { _attackRoutine = value; } }
//    public float AttackCooldown { get { return _attackCooldown; } }
//    public float AttackRange { get { return _attackRange; } }
//    public float Distance { get { return _distance; } }
//    public float AttackDamage { get { return _attackDamage; } }
//    public float CurrentHealth { get { return _currentHealth; } set { _currentHealth = value; } }
//    public int IsAttackingHash { get { return _isAttackingHash; } }
//    public int MaxHealth { get { return _maxHealth; } }
//    public Vector3 Position { get { return _position; } }
//    public EnemyBaseState CurrentState { get { return _currentState; } set { _currentState = value; } }

//    void Awake()
//    {
//        _states = new EnemyStateFactory(this);
//        _currentState = _states.Run();
//        _currentState.EnterState();

//        _animator = GetComponent<Animator>();

//        _attackRangeHash = Animator.StringToHash("attackRange");

//        _isDeadHash = Animator.StringToHash("isDead");

//        _isRunningHash = Animator.StringToHash("isRunning");

//        _currentHealth = _maxHealth;
//    }

//    void Update()
//    {
//        _currentState.UpdateState();

//        EnemyRun();
//    }

//    void EnemyRun()
//    {
//        if (!_isEnemyNear)
//        {
//            transform.position -= transform.right * (Time.deltaTime * 3);
//        }
//    }

//    void OnTriggerEnter2D(Collider2D other)
//    {
//        if (other.tag == "Player")
//        {
//            _isEnemyNear = true;

//            _canAttack = true;
//        }
//    }


//    void OnTriggerExit2D(Collider2D other)
//    {
//            _isEnemyNear = false;

//            _canAttack = false;
//    }

//    void OnDrawGizmosSelected()
//    {
//        if (_attackPoint == null)
//            return;

//        Gizmos.color = Color.red;
//        Gizmos.DrawWireSphere(_attackPoint.position, _attackRange);
//    }

//    public void TakeDamage(float damage)
//    {
//        _currentHealth -= damage;

//        if (_currentHealth <= 0)
//        {
//            Dead();
//        }
//    }

//    void Dead()
//    {
//        if (_isDead && _chestDrop == false)
//        {
//            if (_enemy != null) 
//            {
//              Vector3 _position = _enemy.transform.position;
//            }
//            _chestModel = Instantiate(_chestModel, _position + new Vector3(0.0f, 1.0f, 0.0f), Quaternion.identity);
//            _chestModel.SetActive(true);
//            _chestDrop = true;
//        }

//        _animator.SetBool(_isDeadHash, true);

//        _isDead = true;

//        StartCoroutine(SelfDestruct());
//    }

//    IEnumerator SelfDestruct()
//    {
//        yield return new WaitForSeconds(0.75f);
//                _enemy.SetActive(false);
//    }
//}
