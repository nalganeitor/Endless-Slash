using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerStateMachine : MonoBehaviour
{
    public Attribute[] _attributes;
    Animator _animator;
    [SerializeField] InventoryObject _equipment;
    [SerializeField] InventoryObject _inventory;
    [SerializeField] LayerMask _enemyLayer;
    PlayerInput _playerInput;
    Rigidbody2D _rb2D;
    [SerializeField] Transform _attackPoint;

    [SerializeField] bool _canAttack = true;
    bool _isAttackPressed = false;
    [SerializeField]bool _isEnemyNear = false;
    [SerializeField]bool _keepRunning = true;

    Coroutine _attackRoutine = null;

    float _attackCooldown = 4f;
    float _attackRange = 0.3f;
    float _currentHealth;
    float _maxHealth = 20000f;

    int _attackHash;
    int _attackDamage = 200;
    int _isCombatingHash;
    int _isDeadHash;
    int _isHurtHash;
    int _isRunningHash;
    int _isWalkingHash;

    PlayerBaseState _currentState;
    PlayerStateFactory _states;

    public Animator Animator { get { return _animator; } }
    public LayerMask EnemyLayer { get { return _enemyLayer; } }
    public Transform AttackPoint { get { return _attackPoint; } }
    public bool CanAttack { get { return _canAttack; } set { _canAttack = value; } }
    public bool IsAttackPressed { get { return _isAttackPressed; } }
    public bool IsEnemyNear { get { return _isEnemyNear; } }
    public bool KeepRunning { get { return _keepRunning; } }
    public Coroutine AttackRoutine { get { return _attackRoutine; } set { _attackRoutine = value; } }
    public float AttackCooldown { get { return _attackCooldown; } }
    public float AttackRange { get { return _attackRange; } }
    public int AttackHash { get { return _attackHash; } }
    public int AttackDamage { get { return _attackDamage; } }
    public int IsCombatingHash { get { return _isCombatingHash; } set { _isCombatingHash = value; } }
    public int IsRunningHash { get { return _isRunningHash; } set { _isRunningHash = value; } }
    public PlayerBaseState CurrentState { get { return _currentState; } set { _currentState = value; } }

    void Awake()
    {
        _states = new PlayerStateFactory(this);
        _currentState = _states.Run();
        _currentState.EnterState();

        _animator = GetComponent<Animator>();

        _isCombatingHash = Animator.StringToHash("isCombating");

        _isRunningHash = Animator.StringToHash("isRunning");

        _isHurtHash = Animator.StringToHash("_isHurt");

        _currentHealth = _maxHealth;

        _playerInput = new PlayerInput();

        _playerInput.PlayerController.Attack.started += OnAttack;
        _playerInput.PlayerController.Attack.performed += OnAttack;
        _playerInput.PlayerController.Attack.canceled += OnAttack;
    }

    void Start()
    {
        for (int i = 0; i < _attributes.Length; i++)
        {
            _attributes[i].SetParent(this);
        }
        for (int i = 0; i < _equipment.GetSlots.Length; i++)
        {
            _equipment.GetSlots[i].OnBeforeUpdate += OnBeforeSlotUpdate;
            _equipment.GetSlots[i].OnAfterUpdate += OnAfterSlotUpdate;

        }
    }

    private void Update()
    {
        _currentState.UpdateState();

        if (Input.GetKeyDown(KeyCode.S))
        {
            _inventory.Save();
            _equipment.Save();
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            _inventory.Load();
            _equipment.Load();
        }
    }

    void OnAttack(InputAction.CallbackContext context)
    {
        _isAttackPressed = context.ReadValueAsButton();
    }

    void OnEnable()
    {
        _playerInput.PlayerController.Enable();
    }

    void OnDisable()
    {
        _playerInput.PlayerController.Disable();
    }

    public void TakeDamage(int damage)
    {
        _currentHealth -= damage;
        Debug.Log(_currentHealth);
        _animator.SetTrigger("isHurt");

        if (_currentHealth <= 0f)
        {
            Die();
        }
    }

    void Die()
    {
        _animator.SetBool(_isDeadHash, true);
    }

    public void OnBeforeSlotUpdate(InventorySlot _slot)
    {
        if (_slot.ItemObject == null)
            return;
        switch (_slot.parent.inventory.type)
        {
            case InterfaceType.Inventory:
                break;
            case InterfaceType.Equipment:
                for (int i = 0; i < _slot.item.buffs.Length; i++)
                {
                    for (int j = 0; j < _attributes.Length; j++)
                    {
                        if (_attributes[j].type == _slot.item.buffs[i].attribute)
                            _attributes[j].value.RemoveModifier(_slot.item.buffs[i]);
                    }
                }
                break;
            case InterfaceType.Chest:
                break;
            default:
                break;
        }
    }

    public void OnAfterSlotUpdate(InventorySlot _slot)
    {
        if (_slot.ItemObject == null)
            return;
        switch (_slot.parent.inventory.type)
        {
            case InterfaceType.Inventory:
                break;
            case InterfaceType.Equipment:
                for (int i = 0; i < _slot.item.buffs.Length; i++)
                {
                    for (int j = 0; j < _attributes.Length; j++)
                    {
                        if (_attributes[j].type == _slot.item.buffs[i].attribute)
                            _attributes[j].value.AddModifier(_slot.item.buffs[i]);
                    }
                }
                break;
            case InterfaceType.Chest:
                break;
            default:
                break;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        _animator.SetBool(_isCombatingHash, true);

        _animator.SetBool(_isRunningHash, false);

        _isEnemyNear = true;

        _canAttack = true;

        _keepRunning = false;

        var item = other.GetComponent<GroundItem>();
        if (item)
        {
            Item _item = new Item(item.item);
            if (_inventory.AddItem(_item, 1))
            {
                Destroy(other.gameObject);
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        _animator.SetBool(_isCombatingHash, false);

        _animator.SetBool(_isRunningHash, true);

        _isEnemyNear = false;

        _canAttack = false;

        _keepRunning = true;
    }

    void OnDrawGizmosSelected()
    {
        if (_attackPoint == null)
            return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_attackPoint.position, _attackRange);
    }

    public void AttributeModified(Attribute attribute)
    {
        Debug.Log(string.Concat(attribute.type, " was updated! Value is now ", attribute.value.ModifiedValue));
    }

    private void OnApplicationQuit()
    {
        _inventory.Clear();
        _equipment.Clear();
    }
}

[System.Serializable]
public class Attribute
{
    [System.NonSerialized]
    public PlayerStateMachine parent;
    public Attributes type;
    public ModifiableInt value;

    public void SetParent(PlayerStateMachine _parent)
    {
        parent = _parent;
        value = new ModifiableInt(AttributeModified);
    }

    public void AttributeModified()
    {
        parent.AttributeModified(this);
    }
}
