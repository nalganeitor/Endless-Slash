//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.InputSystem;
//using Cinemachine;

//public class PlayerStateMachine : MonoBehaviour
//{
//    public Attribute[] _attributes;
//    Animator _animator;
//    [SerializeField] CinemachineImpulseSource _source;
//    [SerializeField] InventoryObject _equipment;
//    [SerializeField] InventoryObject _inventory;
//    [SerializeField] LayerMask _enemyLayer;
//    PlayerInput _playerInput;
//    Rigidbody2D _rb2D;
//    [SerializeField] Transform _attackPoint;

//    [SerializeField] bool _canAttack = true;
//    public bool _isAttackPressed = false;
//    [SerializeField] bool _isEnemyNear = false;
//    [SerializeField] bool _keepRunning = true;

//    Coroutine _startAttackRoutine = null;
//    Coroutine _startWhirldwindRoutine = null;

//    public float _attackCooldown = 1f;
//    float _attackRange = 2f;
//    float _currentHealth;
//    float _maxHealth = 20000f;
//    float _whirlwindAmountDamaged;
//    float _whirlwindDamageAmount = 1200;
//    float _whirlwindDamagePerLoop = 200;
//    float _wirlwindDamageTime = 3;
//    float _whirlwindDuration = 3;
//    float _whirlwindAmountDamaged = 0;

//    int _attackHash;
//    int _attackDamage = 200;
//    int _isCombatingHash;
//    int _isDeadHash;
//    int _isHurtHash;
//    int _isRunningHash;
//    int _isWalkingHash;

//    PlayerBaseState _currentState;
//    PlayerStateFactory _states;

//    public Animator Animator { get { return _animator; } }
//    public CinemachineImpulseSource Source { get { return _source; } set { _source = value; } }
//    public LayerMask EnemyLayer { get { return _enemyLayer; } }
//    public Transform AttackPoint { get { return _attackPoint; } }
//    public bool CanAttack { get { return _canAttack; } set { _canAttack = value; } }
//    public bool IsAttackPressed { get { return _isAttackPressed; } }
//    public bool IsEnemyNear { get { return _isEnemyNear; } }
//    public bool KeepRunning { get { return _keepRunning; } }
//    public Coroutine StartAttackRoutine { get { return _startAttackRoutine; } set { _startAttackRoutine = value; } }
//    public Coroutine StartWhirldwindRoutine { get { return _startWhirldwindRoutine; } set { _startWhirldwindRoutine = value; } }
//    public float AttackCooldown { get { return _attackCooldown; } }
//    public float AttackRange { get { return _attackRange; } }
//    public int AttackHash { get { return _attackHash; } }
//    public int AttackDamage { get { return _attackDamage; } }
//    public int IsCombatingHash { get { return _isCombatingHash; } set { _isCombatingHash = value; } }
//    public int IsRunningHash { get { return _isRunningHash; } set { _isRunningHash = value; } }
//    public PlayerBaseState CurrentState { get { return _currentState; } set { _currentState = value; } }

//    void Awake()
//    {
//        _states = new PlayerStateFactory(this);
//        _currentState = _states.Run();
//        _currentState.EnterState();

//        _animator = GetComponent<Animator>();

//        _isCombatingHash = Animator.StringToHash("isCombating");

//        _isRunningHash = Animator.StringToHash("isRunning");

//        _isHurtHash = Animator.StringToHash("_isHurt");

//        _currentHealth = _maxHealth;

//        _playerInput = new PlayerInput();

//        _playerInput.PlayerController.Attack.started += OnAttack;
//        _playerInput.PlayerController.Attack.performed += OnAttack;
//        _playerInput.PlayerController.Attack.canceled += OnAttack;
//    }

//    void Start()
//    {
//        /*  for (int i = 0; i < _attributes.Length; i++)
//          {
//              _attributes[i].SetParent(this);
//          }
//          for (int i = 0; i < _equipment.GetSlots.Length; i++)
//          {
//              _equipment.GetSlots[i].OnBeforeUpdate += OnBeforeSlotUpdate;
//              _equipment.GetSlots[i].OnAfterUpdate += OnAfterSlotUpdate;

//          }*/
//    }

//    private void Update()
//    {
//        _currentState.UpdateState();

//        if (Input.GetKeyDown(KeyCode.S))
//        {
//            _inventory.Save();
//            _equipment.Save();
//        }

//        if (Input.GetKeyDown(KeyCode.L))
//        {
//            _inventory.Load();
//            _equipment.Load();
//        }

//        Whirlwind(_whirlwindDamageAmount, _wirlwindDamageTime);
//    }

//    void OnAttack(InputAction.CallbackContext context)
//    {
//        _isAttackPressed = context.ReadValueAsButton();
//    }

//    void OnEnable()
//    {
//        _playerInput.PlayerController.Enable();
//    }

//    void OnDisable()
//    {
//        _playerInput.PlayerController.Disable();
//    }

//    /* public void Attack()
//     {
//         if (_isAttackPressed && _canAttack)
//         {
//             Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(Ctx.AttackPoint.position, Ctx.AttackRange, Ctx.EnemyLayer);

//             foreach (Collider2D Demons in hitEnemies)
//             {
//                 Demons.GetComponent<EnemyStateMachine>().TakeDamage(Ctx.AttackDamage);
//             }

//             Ctx.Source.GenerateImpulse();
//             Ctx.Animator.SetTrigger("Attack");
//             Ctx.StartAttackRoutine = Ctx.StartCoroutine(ResetAttackCooldown());
//             Ctx.CanAttack = false;
//         }
//     }*/

//    public void Whirlwind(int _whirlwindDamageAmount, int _wirlwindDamageTime)
//    {
//        if (_isAttackPressed && _canAttack)
//        {
//            Debug.Log("attacking");
//            StartCoroutine(ResetWhirlwindCoroutine(_whirlwindDamageAmount, _wirlwindDamageTime));
//            StartCoroutine(ResetAttackCooldown());
//            _canAttack = false;
//        }
//    }

//    IEnumerator ResetAttackCooldown()
//    {
//        yield return new WaitForSeconds(_attackCooldown);
//        _canAttack = true;
//    }

//    IEnumerator ResetWhirlwindCoroutine(int _whirlwindDamageAmount, int _whirlwindDuration)
//    {
//        _whirlwindDamagePerLoop = _whirlwindDamageAmount / _whirlwindDuration;
//        while (_whirlwindAmountDamaged < _whirlwindDamageAmount)
//        {
//            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(_attackPoint.position, _attackRange, _enemyLayer);

//            foreach (Collider2D Demons in hitEnemies)
//            {
//                Demons.GetComponent<EnemyStateMachine>().TakeDamage(_whirlwindDamagePerLoop);
//            }

//            _whirlwindAmountDamaged += _whirlwindDamagePerLoop;
//            yield return new WaitForSeconds(0.5f);
//        }
//    }

//    public void TakeDamage(int damage)
//    {
//        _currentHealth -= damage;
//        _animator.SetTrigger("isHurt");

//        if (_currentHealth <= 0f)
//        {
//            Die();
//        }
//    }

//    void Die()
//    {
//        _animator.SetBool(_isDeadHash, true);
//    }

//    public void OnBeforeSlotUpdate(InventorySlot _slot)
//    {
//        if (_slot.ItemObject == null)
//            return;
//        switch (_slot.parent.inventory.type)
//        {
//            case InterfaceType.Inventory:
//                break;
//            case InterfaceType.Equipment:
//                for (int i = 0; i < _slot.item.buffs.Length; i++)
//                {
//                    for (int j = 0; j < _attributes.Length; j++)
//                    {
//                        if (_attributes[j].type == _slot.item.buffs[i].attribute)
//                            _attributes[j].value.RemoveModifier(_slot.item.buffs[i]);
//                    }
//                }
//                break;
//            case InterfaceType.Chest:
//                break;
//            default:
//                break;
//        }
//    }

//    public void OnAfterSlotUpdate(InventorySlot _slot)
//    {
//        if (_slot.ItemObject == null)
//            return;
//        switch (_slot.parent.inventory.type)
//        {
//            case InterfaceType.Inventory:
//                break;
//            case InterfaceType.Equipment:
//                for (int i = 0; i < _slot.item.buffs.Length; i++)
//                {
//                    for (int j = 0; j < _attributes.Length; j++)
//                    {
//                        if (_attributes[j].type == _slot.item.buffs[i].attribute)
//                            _attributes[j].value.AddModifier(_slot.item.buffs[i]);
//                    }
//                }
//                break;
//            case InterfaceType.Chest:
//                break;
//            default:
//                break;
//        }
//    }

//    void OnTriggerEnter2D(Collider2D other)
//    {
//        _animator.SetBool(_isCombatingHash, true);

//        _animator.SetBool(_isRunningHash, false);

//        _isEnemyNear = true;

//        _canAttack = true;

//        _keepRunning = false;

//        var item = other.GetComponent<GroundItem>();
//        if (item)
//        {
//            Item _item = new Item(item.item);
//            if (_inventory.AddItem(_item, 1))
//            {
//                Destroy(other.gameObject);
//            }
//        }
//    }

//    void OnTriggerExit2D(Collider2D other)
//    {
//        _animator.SetBool(_isCombatingHash, false);

//        _animator.SetBool(_isRunningHash, true);

//        _isEnemyNear = false;

//        _canAttack = false;

//        _keepRunning = true;
//    }

//    void OnDrawGizmosSelected()
//    {
//        if (_attackPoint == null)
//            return;

//        Gizmos.color = Color.red;
//        Gizmos.DrawWireSphere(_attackPoint.position, _attackRange);
//    }

//    public void AttributeModified(Attribute attribute)
//    {
//        Debug.Log(string.Concat(attribute.type, " was updated! Value is now ", attribute.value.ModifiedValue));
//    }

//    private void OnApplicationQuit()
//    {
//        _inventory.Clear();
//        _equipment.Clear();
//    }
//}

///*[System.Serializable]
//public class Attribute
//{
//    [System.NonSerialized]
//    public PlayerStateMachine parent;
//    public Attributes type;
//    public ModifiableInt value;

//    public void SetParent(PlayerStateMachine _parent)
//    {
//        parent = _parent;
//        value = new ModifiableInt(AttributeModified);
//    }

//    public void AttributeModified()
//    {
//        parent.AttributeModified(this);
//    }
//}*/
