using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerManager : MonoBehaviour
{
    Animator _animator;
    public Attribute[] attributes;
    [SerializeField] PlayerStats playerStats;
    public InventoryObject _equipment;
    public InventoryObject _inventory;
    [SerializeField] PlayerHealth playerHealth;
    [SerializeField] LayerMask _enemyLayer;
    [SerializeField] PlayerBasicAttack playerBasicAttack;
    [SerializeField] Berserk berserk;
    [SerializeField] Explosion explosion;
    [SerializeField] FlameThrow flameThrow;
    [SerializeField] ShieldBlock shieldBlock;
    [SerializeField] Whirlwind whirlwind;
    PlayerInput _playerInput;
    [SerializeField] Transform _playerBasicAttackPoint;
    [SerializeField] Transform _whirlwindAttackPoint;

    bool _isAttackOnePressed = false;
    bool _isAttackTwoPressed = false;
    bool _isAttackThreePressed = false;
    bool _isAttackFourPressed = false;
    bool _isAttackFivePressed = false;
    bool _isAttackSixPressed = false;
    bool _isAttackSevenPressed = false;
    bool _isAttackEightPressed = false;

    float _playerBasicAttackRange = 0.3f;
    float _whirlwindAttackRange = 1f;

    int _playerBasicAttackHash;
    int _berserkAttackHash;
    int _flameThrowHash;
    int _isAttackingHash;
    int _isCombatingHash;
    int _isDeadHash;
    int _isHurtHash;
    int _isRunningHash;
    //  int _whirlwindAttackHash;

    public Dictionary<Stats, int> statsValue = new Dictionary<Stats, int>();

    int totalDamage;

    int damage;

    void Awake()
    {
        flameThrow._canFlameThrow = true;

        for (int i = 0; i < attributes.Length; i++)
        {
           // attributes[0].value.BaseValue = 1;
            attributes[i].SetParent(this);
        }
        for (int i = 0; i < _equipment.GetSlots.Length; i++)
        {
            _equipment.GetSlots[i].OnBeforeUpdate += OnBeforeSlotUpdate;
            _equipment.GetSlots[i].OnAfterUpdate += OnAfterSlotUpdate;
        }

        statsValue.Add(Stats.Damage, playerStats.damageStat);
        statsValue.Add(Stats.Defense, playerStats.defenseStat);
        statsValue.Add(Stats.Health, playerStats.healthStat);

        _animator = GetComponent<Animator>();
        _playerInput = new PlayerInput();
        playerHealth._playerCurrentHealth = playerHealth._playerMaxHealth;

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

        _playerBasicAttackHash = Animator.StringToHash("BasicAttack");
        _berserkAttackHash = Animator.StringToHash("Berserk");
        _flameThrowHash = Animator.StringToHash("FlameThrow");
        _isCombatingHash = Animator.StringToHash("isCombating");
        _isDeadHash = Animator.StringToHash("isDead");
        _isHurtHash = Animator.StringToHash("isHurt");
        _isRunningHash = Animator.StringToHash("isRunning");
        //  _whirlwindAttackHash = Animator.StringToHash("whirlwindAttackHash");
    }

    void Start()
    {
        _animator.SetBool(_isRunningHash, true);
        _animator.SetBool(_playerBasicAttackHash, false);
    }

    private void Update()
    {
        PlayerBasicAttack();

        Whirlwind();

        Berserk();

        FlameThrow();

        Explosion();

        ShieldBlock();

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

        int damage = attributes[0].value.modifiedValue;
        attributes[0].value.baseValue = 200;
        int defense = attributes[1].value.modifiedValue;
        attributes[1].value.baseValue = 100;
    }

    public void PlayerBasicAttack()
    {
        if (Input.GetKey(KeyCode.B) && playerBasicAttack._canPlayerBasicAttack)
        {
            _animator.SetTrigger("BasicAttack");

            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(_playerBasicAttackPoint.position, _playerBasicAttackRange, _enemyLayer);

            foreach (Collider2D Demons in hitEnemies)
            {
                Demons.GetComponent<EnemyManager>().TakeDamage(playerBasicAttack._playerBasicAttackDamage);
            }

            StartCoroutine(ResetPlayerBasicAttackCooldown());
        }
    }

    IEnumerator ResetPlayerBasicAttackCooldown()
    {
        yield return new WaitForSeconds(playerBasicAttack._playerBasicAttackCooldown);
        playerBasicAttack._canPlayerBasicAttack = true;
    }

    public void Whirlwind()
    {
        if (_isAttackTwoPressed && whirlwind._canWhirlwind)
        {
            StartCoroutine(ResetWhirlwindCoroutine());
            StartCoroutine(ResetWhirlwindCooldown());
            whirlwind._canWhirlwind = false;

            Debug.Log("whirlwind");
        }
    }

    IEnumerator ResetWhirlwindCoroutine()
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

            var copy = Instantiate(flameThrow._flameThrow, new Vector3(flameThrow._flameThrowXPos, flameThrow._flameThrowYPos, 0), Quaternion.identity);

            flameThrow._canFlameThrow = false;
            StartCoroutine(ResetFlameThrowCooldown());
            Destroy(copy, flameThrow._flameThrowDuration);
        }
    }

    IEnumerator ResetFlameThrowCooldown()
    {
        yield return new WaitForSeconds(flameThrow._flameThrowCooldown);
        flameThrow._canFlameThrow = true;
    }

    public void Explosion()
    {
        if (_isAttackFivePressed && explosion._canExplosion)
        {
            _animator.SetTrigger("Explosion");

            var copy = Instantiate(explosion._explosion, new Vector3(explosion._explosionXPos, explosion._explosionYPos, 0), Quaternion.identity);

            explosion._canExplosion = false;
            StartCoroutine(ResetExplosionCooldown());
            Destroy(copy, explosion._explosionDuration);
        }
    }

    IEnumerator ResetExplosionCooldown()
    {
        yield return new WaitForSeconds(explosion._explosionCooldown);
        explosion._canExplosion = true;
    }

    public void ShieldBlock()
    {
        if (_isAttackSixPressed)
        {
            shieldBlock._isPlayerBlocking = true;

            shieldBlock._playerStuns = true;

            StartCoroutine(ResetShieldBlockStunDuration());
            StartCoroutine(ResetExplosionCooldown());
        }
    }

    IEnumerator ResetShieldBlockStunDuration()
    {
        yield return new WaitForSeconds(shieldBlock._shieldBlockStunDuration);
        shieldBlock._playerStuns = false;
    }

    IEnumerator ResetShieldBlockCooldown()
    {
        yield return new WaitForSeconds(shieldBlock._shieldBlockCooldown);
        shieldBlock._playerStuns = false;
    }

    void OnDrawGizmos()
    {
        if (_playerBasicAttackPoint == null)
            return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_playerBasicAttackPoint.position, _playerBasicAttackRange);

        if (_whirlwindAttackPoint == null)
            return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_whirlwindAttackPoint.position, _whirlwindAttackRange);
    }

    public void TakeDamage(float _damage)
    {
        playerHealth._playerCurrentHealth -= _damage;
        _animator.SetTrigger("isHurt");

        if (playerHealth._playerCurrentHealth <= 0f)
        {
            Die();
        }
    }

    void Die()
    {
        _animator.SetBool(_isDeadHash, true);
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
            Debug.Log("ha");
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

    public void AttributeModified(Attribute attribute)
    {
        Debug.Log(string.Concat(attribute.type, " was updated! Value is now ", attribute.value.ModifiedValue));
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
                        if (attributes[j].type == _slot.item.buffs[i].stats)
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
                        if (attributes[j].type == _slot.item.buffs[i].stats)
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
    public Stats type;
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