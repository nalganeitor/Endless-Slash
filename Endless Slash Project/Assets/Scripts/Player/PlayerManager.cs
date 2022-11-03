using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;

public class PlayerManager : MonoBehaviour
{
    Animator _animator;
    public Attribute[] attributes;
    [SerializeField] CinemachineImpulseSource _source;
    public InventoryObject _equipment;
    public InventoryObject _inventory;
    [SerializeField] LayerMask _enemyLayer;
    [SerializeField] BasicAttack basicAttack;
    [SerializeField] Berserk berserk;
    [SerializeField] FlameThrow flameThrow;
    [SerializeField] Whirlwind whirlwind;
    PlayerInput _playerInput;
    [SerializeField] Transform _basicAttackPoint;
    [SerializeField] Transform _whirlwindAttackPoint;

 //   bool _canBerserk = false;
    bool _isAttackOnePressed = false;
    bool _isAttackTwoPressed = false;
    bool _isAttackThreePressed = false;
    bool _isAttackFourPressed = false;
    bool _isAttackFivePressed = false;
    bool _isAttackSixPressed = false;
    bool _isAttackSevenPressed = false;
    bool _isAttackEightPressed = false;
  //  bool _keepRunning;
    
    float _basicAttackRange = 0.3f;
    float _playerCurrentHealth;
    float _playerMaxHealth = 200000f;
    float _whirlwindAttackRange = 1f;


    int _basicAttackHash;
    int _berserkAttackHash;
    int _flameThrowHash;
    int _isAttackingHash;
    int _isCombatingHash;
    int _isDeadHash;
    int _isHurtHash;
    int _isRunningHash;
  //  int _whirlwindAttackHash;

    void Awake()
    {
        _animator = GetComponent<Animator>();
        _playerInput = new PlayerInput();
        _playerCurrentHealth = _playerMaxHealth;

        _playerInput.PlayerController.Attack1.started += OnAttack1;
        _playerInput.PlayerController.Attack1.canceled += OnAttack1;
        _playerInput.PlayerController.Attack2.started += OnAttack2;
        _playerInput.PlayerController.Attack2.canceled += OnAttack2;
        _playerInput.PlayerController.Attack3.started += OnAttack3;
        _playerInput.PlayerController.Attack3.canceled += OnAttack3;
        _playerInput.PlayerController.Attack4.started += OnAttack4;
        _playerInput.PlayerController.Attack4.canceled += OnAttack4;
        _playerInput.PlayerController.Attack5.started += OnAttack5;
        _playerInput.PlayerController.Attack5.canceled += OnAttack5;
        _playerInput.PlayerController.Attack6.started += OnAttack6;
        _playerInput.PlayerController.Attack6.canceled += OnAttack6;
        _playerInput.PlayerController.Attack7.started += OnAttack7;
        _playerInput.PlayerController.Attack7.canceled += OnAttack7;
        _playerInput.PlayerController.Attack8.started += OnAttack8;
        _playerInput.PlayerController.Attack8.canceled += OnAttack8;

        _basicAttackHash = Animator.StringToHash("Attack");
        _berserkAttackHash = Animator.StringToHash("Berserk");
        _flameThrowHash = Animator.StringToHash("FlameThrow");
        _isCombatingHash = Animator.StringToHash("isCombating");
        _isDeadHash = Animator.StringToHash("isDead");
        _isHurtHash = Animator.StringToHash("isHurt");
        _isRunningHash = Animator.StringToHash("isRunning");
      //  _whirlwindAttackHash = Animator.StringToHash("whirlwindAttackHash");

        for (int i = 0; i < attributes.Length; i++)
        {
            attributes[i].SetParent(this);
        }
        for (int i = 0; i < _equipment.GetSlots.Length; i++)
        {
            _equipment.GetSlots[i].OnBeforeUpdate += OnBeforeSlotUpdate;
            _equipment.GetSlots[i].OnAfterUpdate += OnAfterSlotUpdate;
        }
    }

    void Start()
    {
        _animator.SetBool(_isRunningHash, true);
    }

    private void Update()
    {

        BasicAttack();

        Whirlwind(/*_whirlwindDamageAmount, _whirlwindDuration*/);

        Berserk();

        FlameThrow();

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

    public void BasicAttack()
    {
        if (_isAttackOnePressed && basicAttack._canBasicAttack)
        {
            _animator.SetTrigger("Attack");

            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(_basicAttackPoint.position, _basicAttackRange, _enemyLayer);

            foreach (Collider2D Demons in hitEnemies)
            {
                Demons.GetComponent<EnemyManager>().TakeDamage(basicAttack._basicAttackDamage);
            }

            StartCoroutine(ResetBasicAttackCooldown());
        }
    }

    IEnumerator ResetBasicAttackCooldown()
    {
        yield return new WaitForSeconds(basicAttack._basicAttackCooldown);
        basicAttack._canBasicAttack = true;
    }

    public void Whirlwind(/*float whirlwindDamageAmount, float whirlwindDuration*/)
    {
        if (_isAttackTwoPressed && whirlwind._canWhirlwind)
        {
            StartCoroutine(ResetWhirlwindCoroutine(/*whirlwind._whirlwindDamageAmount, whirlwind._whirlwindDuration*/));
            StartCoroutine(ResetWhirlwindCooldown());
            whirlwind._canWhirlwind = false;

            Debug.Log("whirlwind");
        }
    }

    IEnumerator ResetWhirlwindCoroutine(/*float whirlwindDamageAmount, float whirlwindDuration*/)
    {
        whirlwind._whirlwindDamagePerLoop = whirlwind._whirlwindDamageAmount / whirlwind._whirlwindDuration;
        while (whirlwind._whirlwindAmountDamaged < whirlwind._whirlwindDamageAmount)
        {
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(_whirlwindAttackPoint.position, whirlwind._whirlwindAttackRange, _enemyLayer);

            foreach (Collider2D Demons in hitEnemies)
            {
                Demons.GetComponent<EnemyManager>().TakeDamage(whirlwind._whirlwindDamagePerLoop);
            }

            Debug.Log(whirlwind._whirlwindAmountDamaged + "whirlwind dmg");

            whirlwind._whirlwindAmountDamaged += whirlwind._whirlwindDamagePerLoop;
            yield return new WaitForSeconds(0.5f);
        }
    }

    IEnumerator ResetWhirlwindCooldown()
    {
        yield return new WaitForSeconds(whirlwind._whirlwindCooldown);
        whirlwind._canWhirlwind = true;
        whirlwind._whirlwindAmountDamaged = 0;
    }

    public void Berserk()
    {
        if (_isAttackThreePressed && !berserk._canBerserk)
        {
            _animator.SetBool(_berserkAttackHash, true);

            berserk._canBerserk = true;
            berserk._berserkMode = true;

            StartCoroutine(ResetBerserkCoroutine());
            StartCoroutine(ResetBerserkCooldown());
        }
    }

    IEnumerator ResetBerserkCoroutine()
    {
        yield return new WaitForSeconds(berserk._berserkDuration);
        berserk._berserkMode = false;
        _animator.SetBool(_berserkAttackHash, false);
    }

    IEnumerator ResetBerserkCooldown()
    {
        yield return new WaitForSeconds(berserk._berserkCooldown);
        berserk._canBerserk = false;
    }

    public void FlameThrow()
    {
        if (_isAttackFourPressed && flameThrow._canFlameThrow)
        {
            _animator.SetTrigger("FlameThrow");

            Instantiate(flameThrow._flameThrow, new Vector3(flameThrow._flameThrowXPos, flameThrow._flameThrowYPos, 0), Quaternion.identity);

          //  flameThrow._flameThrow.transform.position = transform.right * (Time.deltaTime * 3);

            flameThrow._canFlameThrow = false;
          //  StartCoroutine(ResetFlameThrowCoroutine());
            StartCoroutine(ResetFlameThrowCooldown());
        }
    }

   /* IEnumerator ResetFlameThrowCoroutine()
    {
        yield return new WaitForSeconds(flameThrow._flameThrowDuration);
        DestroyImmediate(flameThrow._flameThrow);
    }*/

    IEnumerator ResetFlameThrowCooldown()
    {
        yield return new WaitForSeconds(flameThrow._flameThrowCooldown);
        flameThrow._canFlameThrow = true;
    }

    void OnDrawGizmos()
    {
        if (_basicAttackPoint == null)
            return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_basicAttackPoint.position, _basicAttackRange);

        if (_whirlwindAttackPoint == null)
            return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_whirlwindAttackPoint.position, _whirlwindAttackRange);
    }

    public void TakeDamage(float _damage)
    {
        _playerCurrentHealth -= _damage;
        _animator.SetTrigger("isHurt");

        if (_playerCurrentHealth <= 0f)
        {
            Die();
        }
    }

    void Die()
    {
        _animator.SetBool(_isDeadHash, true);
    }

    public void AttributeModified(Attribute attribute)
    {
        Debug.Log(string.Concat(attribute.type, " was updated! Value is now ", attribute.value.ModifiedValue));
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        _animator.SetBool(_isCombatingHash, true);

        _animator.SetBool(_isRunningHash, false);

        if (other.gameObject.CompareTag("Enemy") && berserk._canBerserk)
        {
            if (berserk._berserkMode)
            {
                Destroy(other.gameObject);

                Debug.Log("active");
            }
        }

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

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy") && berserk._canBerserk)
        {
            if (berserk._berserkMode)
            {
                Destroy(other.gameObject);

                Debug.Log("active");
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    { 
        _animator.SetBool(_isCombatingHash, false);

        _animator.SetBool(_isRunningHash, true);

       // _keepRunning = true;
    }

    void OnAttack1(InputAction.CallbackContext context)
    {
        _isAttackOnePressed = context.ReadValueAsButton();
    }

    void OnAttack2(InputAction.CallbackContext context)
    {
        _isAttackTwoPressed = context.ReadValueAsButton();
    }

    void OnAttack3(InputAction.CallbackContext context)
    {
        _isAttackThreePressed = context.ReadValueAsButton();
    }

    void OnAttack4(InputAction.CallbackContext context)
    {
        _isAttackFourPressed = context.ReadValueAsButton();
    }

    void OnAttack5(InputAction.CallbackContext context)
    {
        _isAttackFivePressed = context.ReadValueAsButton();
    }

    void OnAttack6(InputAction.CallbackContext context)
    {
        _isAttackSixPressed = context.ReadValueAsButton();
    }

    void OnAttack7(InputAction.CallbackContext context)
    {
        _isAttackSevenPressed = context.ReadValueAsButton();
    }

    void OnAttack8(InputAction.CallbackContext context)
    {
        _isAttackEightPressed = context.ReadValueAsButton();
    }

    void OnEnable()
    {
        _playerInput.PlayerController.Enable();
    }

    void OnDisable()
    {
        _playerInput.PlayerController.Disable();
    }

    private void OnApplicationQuit()
    {
        _inventory.Clear();
        _equipment.Clear();
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
                    for (int j = 0; j < attributes.Length; j++)
                    {
                        if (attributes[j].type == _slot.item.buffs[i].attribute)
                            attributes[j].value.RemoveModifier(_slot.item.buffs[i]);
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
                    for (int j = 0; j < attributes.Length; j++)
                    {
                        if (attributes[j].type == _slot.item.buffs[i].attribute)
                            attributes[j].value.AddModifier(_slot.item.buffs[i]);
                    }
                }
                break;
            case InterfaceType.Chest:
                break;
            default:
                break;
        }
    }
}

[System.Serializable]
public class Attribute
{
    [System.NonSerialized]
    public PlayerManager parent;
    public Attributes type;
    public ModifiableInt value;

    public void SetParent(PlayerManager _parent)
    {
        parent = _parent;
        value = new ModifiableInt(AttributeModified);
    }

    public void AttributeModified()
    {
        parent.AttributeModified(this);
    }
}