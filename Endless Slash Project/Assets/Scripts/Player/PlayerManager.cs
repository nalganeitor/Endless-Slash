using System.Collections;
using System.Collections.Generic;
using UnityEngine;
<<<<<<< Updated upstream
using UnityEngine.InputSystem;
using Cinemachine;
=======
>>>>>>> Stashed changes

public class PlayerManager : MonoBehaviour
{
    Animator _animator;
    public Attribute[] attributes;
<<<<<<< Updated upstream
    [SerializeField] CinemachineImpulseSource _source;
=======
    [SerializeField] PlayerAudio playerAudio;
    [SerializeField] PlayerStats playerStats;
>>>>>>> Stashed changes
    public InventoryObject _equipment;
    public InventoryObject _inventory;
    [SerializeField] LayerMask _enemyLayer;
    [SerializeField] BasicAttack basicAttack;
    [SerializeField] Berserk berserk;
    [SerializeField] FlameThrow flameThrow;
    [SerializeField] Whirlwind whirlwind;
<<<<<<< Updated upstream
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
=======
    [SerializeField] Transform _playerBasicAttackPoint;
    [SerializeField] Transform _whirlwindAttackPoint;

    float _playerBasicAttackRange = 0.3f;
>>>>>>> Stashed changes
    float _whirlwindAttackRange = 1f;


    int _basicAttackHash;
    int _berserkAttackHash;
    int _flameThrowHash;
    int _isAttackingHash;
    int _isCombatingHash;
    int _isDeadHash;
    int _isHurtHash;
    int _isRunningHash;
<<<<<<< Updated upstream
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
=======
    //  int _whirlwindAttackHash;

    public Dictionary<Stats, int> statsValue = new Dictionary<Stats, int>();

    public int totalDamage;
    public int totalDefense;
    public int totalHealth;

    void Awake()
    {
        playerBasicAttack._canPlayerBasicAttack = true;
        whirlwind._canWhirlwind = true;
        berserk._canBerserk = false;
        flameThrow._canFlameThrow = true;
        explosion._canExplosion = true;

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
        playerHealth._playerCurrentHealth = playerHealth._playerMaxHealth;

        _playerBasicAttackHash = Animator.StringToHash("BasicAttack");
>>>>>>> Stashed changes
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
<<<<<<< Updated upstream
=======

        int baseDamage = attributes[0].value.modifiedValue;
        int modDamage = attributes[0].value.baseValue = 200;

        int baseDefense = attributes[1].value.modifiedValue;
        int modDefense = attributes[1].value.baseValue = 100;

        int baseHealth = attributes[1].value.modifiedValue;
        int modHealth = attributes[1].value.baseValue = 50;

        totalDamage = baseDamage + modDamage;
        totalDefense = baseDefense + modDefense;
        totalHealth = baseHealth + modHealth;
>>>>>>> Stashed changes
    }

    public void BasicAttack()
    {
<<<<<<< Updated upstream
        if (_isAttackOnePressed && basicAttack._canBasicAttack)
        {
            _animator.SetTrigger("Attack");
=======
        if (Input.GetKeyDown(KeyCode.Alpha1) && playerBasicAttack._canPlayerBasicAttack)
        {
            AudioManager._instance.PlaySound(playerAudio._basicAttackSound);

            _animator.SetTrigger("BasicAttack");
>>>>>>> Stashed changes

            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(_basicAttackPoint.position, _basicAttackRange, _enemyLayer);

            foreach (Collider2D Demons in hitEnemies)
            {
                Demons.GetComponent<EnemyManager>().TakeDamage(basicAttack._basicAttackDamage);
            }
<<<<<<< Updated upstream

            StartCoroutine(ResetBasicAttackCooldown());
=======
            playerBasicAttack._canPlayerBasicAttack = false;
            StartCoroutine(ResetPlayerBasicAttackCooldown());
>>>>>>> Stashed changes
        }
    }

    IEnumerator ResetBasicAttackCooldown()
    {
        yield return new WaitForSeconds(basicAttack._basicAttackCooldown);
        basicAttack._canBasicAttack = true;
    }

    public void Whirlwind(/*float whirlwindDamageAmount, float whirlwindDuration*/)
    {
        if (Input.GetKeyDown(KeyCode.Alpha2) && whirlwind._canWhirlwind)
        {
            StartCoroutine(ResetWhirlwindCoroutine(/*whirlwind._whirlwindDamageAmount, whirlwind._whirlwindDuration*/));
            StartCoroutine(ResetWhirlwindCooldown());
            whirlwind._canWhirlwind = false;
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
        if (Input.GetKeyDown(KeyCode.Alpha3) && !berserk._canBerserk)
        {
            AudioManager._instance.PlaySound(playerAudio._berserkSound);

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
        if (Input.GetKeyDown(KeyCode.Alpha4) && flameThrow._canFlameThrow)
        {
            _animator.SetTrigger("FlameThrow");

<<<<<<< Updated upstream
            Instantiate(flameThrow._flameThrow, new Vector3(flameThrow._flameThrowXPos, flameThrow._flameThrowYPos, 0), Quaternion.identity);

          //  flameThrow._flameThrow.transform.position = transform.right * (Time.deltaTime * 3);
=======
            AudioManager._instance.PlaySound(playerAudio._flameThrowSound);

            var copy = Instantiate(flameThrow._flameThrow, new Vector3(flameThrow._flameThrowXPos, flameThrow._flameThrowYPos, 0), Quaternion.identity);
>>>>>>> Stashed changes

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

<<<<<<< Updated upstream
=======
    public void Explosion()
    {
        if (Input.GetKeyDown(KeyCode.Alpha5) && explosion._canExplosion)
        {
            AudioManager._instance.PlaySound(playerAudio._explosionSound);

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
        if (Input.GetKeyDown(KeyCode.Alpha6))
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

>>>>>>> Stashed changes
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
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    { 
        _animator.SetBool(_isCombatingHash, false);

        _animator.SetBool(_isRunningHash, true);

       // _keepRunning = true;
    }

    private void OnApplicationQuit()
    {
        _inventory.Clear();
        _equipment.Clear();
    }

<<<<<<< Updated upstream
    public void OnBeforeSlotUpdate(InventorySlot _slot)
=======
    public void AttributeModified(Attribute attribute)
    {
      //  Debug.Log(string.Concat(attribute.type, " was updated! Value is now ", attribute.value.ModifiedValue));
    }

       public void OnBeforeSlotUpdate(InventorySlot _slot)
>>>>>>> Stashed changes
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