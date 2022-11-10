using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class EnemyManager : MonoBehaviour
{
    Animator _animator;
    public GameObject[] droppedItemPrefab;
    [SerializeField] CinemachineImpulseSource _source;
    [SerializeField] EnemyBasicAttack enemyBasicAttack;
    [SerializeField] EnemyHealth enemyHealth;
    [SerializeField] ShieldBlock shieldBlock;
    [SerializeField] GameObject _enemy;
    [SerializeField] GameObject _player;
    [SerializeField] LayerMask _playerLayer;
    [SerializeField] Transform _enemyAttackPoint;
    [SerializeField] GameObject popupText;

  //  bool _isEnemyDead = false;
    bool _isPlayerNear = false;

    float _enemyAttackRange = 0.3f;
    float _enemyCurrentHealth;
    float _enemyMaxHealth = 200;

    int _enemyAttackHash;
    int _isEnemyDeadHash;
    int _isEnemyHurtHash;
    int _isEnemyStunHash;

    Vector3 _enemyPosition;

    void Awake()
    {
        _animator = GetComponent<Animator>();

        _source = GetComponent<CinemachineImpulseSource>();

        _enemyAttackHash = Animator.StringToHash("enemyAttack");
        _isEnemyDeadHash = Animator.StringToHash("isEnemyDead");
        _isEnemyHurtHash = Animator.StringToHash("isEnemyHurt");
        _isEnemyStunHash = Animator.StringToHash("isEnemyStun");

        _enemyCurrentHealth = _enemyMaxHealth;
    }

    void Update()
    {
        EnemyRun();

        Attack();
    }

    void EnemyRun()
    {
        if (!_isPlayerNear && _enemy != null)
        {
            transform.position -= transform.right * (Time.deltaTime * 3);
        }
    }

    public void Attack()
    {
        if (_enemy != null)
        {
            if (_isPlayerNear && enemyBasicAttack._canEnemyBasicAttack && shieldBlock._isPlayerBlocking && shieldBlock._playerStuns)
            {
                _animator.SetTrigger("isEnemyStun");

                shieldBlock._isPlayerBlocking = false;
                enemyBasicAttack._canEnemyBasicAttack = false;
                StartCoroutine(ResetAttackCooldown());
            }
            else if (_isPlayerNear && enemyBasicAttack._canEnemyBasicAttack && shieldBlock._isPlayerBlocking && !shieldBlock._playerStuns)
            {
                _animator.SetTrigger("enemyAttack");

                shieldBlock._isPlayerBlocking = false;
                enemyBasicAttack._canEnemyBasicAttack = false;
                StartCoroutine(ResetAttackCooldown());
            }
            else if (_isPlayerNear && enemyBasicAttack._canEnemyBasicAttack && !shieldBlock._isPlayerBlocking && !shieldBlock._playerStuns)
            {
                Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(_enemyAttackPoint.position, _enemyAttackRange, _playerLayer);

                foreach (Collider2D knight in hitEnemies)
                {
                    knight.GetComponent<PlayerManager>().TakeDamage(enemyBasicAttack._enemyBasicAttackDamage);
                }
                _animator.SetTrigger("enemyAttack");
                enemyBasicAttack._canEnemyBasicAttack = false;
                StartCoroutine(ResetAttackCooldown());
            }
        }
    }


    IEnumerator ResetAttackCooldown()
    {
        yield return new WaitForSeconds(enemyBasicAttack._enemyBasicAttackCooldown);
        enemyBasicAttack._canEnemyBasicAttack = true;
    }

    void OnDrawGizmosSelected()
    {
        if (_enemyAttackPoint == null)
            return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_enemyAttackPoint.position, _enemyAttackRange);
    }

    public void TakeDamage(float damage)
    {
        if (_enemy != null)
        {
            ShowDamage(damage.ToString());
            _enemyCurrentHealth -= damage;

            _source.GenerateImpulse();
            //_animator.SetTrigger("isEnemyHurt");

            if (_enemyCurrentHealth <= 0)
            {
                Dead();
           //     _isEnemyDead = true;
            }
        }
    }

    void ShowDamage(string text)
    {
        if (popupText)
        {
            GameObject copy = Instantiate(popupText, transform.position, Quaternion.identity);
            copy.GetComponentInChildren<TextMesh>().text = text;

            Destroy(copy, 2f);
        }
    }

    void Dead()
    {
        InstantiateLoot();
        Vector3 _position = _enemy.transform.position;

        //  _chestModel = Instantiate(_chestModel, _enemyPosition + new Vector3(0.0f, 1.0f, 0.0f), Quaternion.identity);
        //  _chestModel.SetActive(true);
        _animator.SetBool(_isEnemyDeadHash, true);

    //    GetComponent<LootBag>().InstantiateLoot(new Vector3(-12.67f, 17.53f, 0f));

        StartCoroutine(SelfDestruct());
    }

    public void InstantiateLoot()
    {
        ItemObject droppedItem = GetComponent<LootBag>().GetDroppedItem();
        if (droppedItem != null)
        {
            
                Debug.Log("nadasa");
                int n = Random.Range(0, droppedItemPrefab.Length);
                GameObject lootGameObject = droppedItemPrefab[n];
                Instantiate(lootGameObject, new Vector3(-12.67f, 17.53f, 0f), Quaternion.identity);

                lootGameObject.GetComponent<SpriteRenderer>().sprite = droppedItem.uiDisplay;
            
        }
    }

    IEnumerator SelfDestruct()
    {
        yield return new WaitForSeconds(0.75f);
        _enemy.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            _isPlayerNear = true;

            enemyBasicAttack._canEnemyBasicAttack = true;
        }
    }

}

