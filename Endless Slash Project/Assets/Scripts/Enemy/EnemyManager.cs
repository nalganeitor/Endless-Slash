using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    Animator _animator;
    [SerializeField] GameObject _chestModel;
    [SerializeField] GameObject _enemy;
    [SerializeField] GameObject _player;
    [SerializeField] LayerMask _playerLayer;
    [SerializeField] Transform _enemyAttackPoint;

    bool _canEnemyAttack = false;
    bool _chestDrop = false;
    bool _isEnemyDead = false;
    bool _isPlayerNear = false;

    float _enemyAttackCooldown = 5f;
    float _enemyAttackDamage = 40;
    float _enemyAttackRange = 0.3f;
    float _enemyCurrentHealth;
    float _enemyMaxHealth = 200;

    int _enemyAttackHash;
    int _isEnemyDeadHash;
    int _isEnemyHurtHash;

    Vector3 _enemyPosition;

    void Awake()
    {
        _animator = GetComponent<Animator>();

        _enemyAttackHash = Animator.StringToHash("enemyAttack");
        _isEnemyDeadHash = Animator.StringToHash("isEnemyDead");
        _isEnemyHurtHash = Animator.StringToHash("isEnemyHurt");

        _enemyCurrentHealth = _enemyMaxHealth;
    }

    void Update()
    {
        EnemyRun();

        Attack();
    }

    void EnemyRun()
    {
        if (!_isPlayerNear)
        {
            transform.position -= transform.right * (Time.deltaTime * 3);
        }
    }

    public void Attack()
    {
        if (_enemy != null)
        {
            if (_isPlayerNear && _canEnemyAttack)
            {
                Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(_enemyAttackPoint.position, _enemyAttackRange, _playerLayer);

                foreach (Collider2D knight in hitEnemies)
                {
                    knight.GetComponent<PlayerManager>().TakeDamage(_enemyAttackDamage);
                }

                _animator.SetTrigger("enemyAttack");
                StartCoroutine(ResetAttackCooldown());
                _canEnemyAttack = false;
            }
        }
    }


    IEnumerator ResetAttackCooldown()
    {
        yield return new WaitForSeconds(_enemyAttackCooldown);
        _canEnemyAttack = true;
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
        _enemyCurrentHealth -= damage;

        //_animator.SetTrigger("isEnemyHurt");

        if (_enemyCurrentHealth <= 0)
        {
            Dead();
        }
    }

    void Dead()
    {
        if (_isEnemyDead && _chestDrop == false)
        {
            if (_enemy != null)
            {
                Vector3 _position = _enemy.transform.position;
            }
            _chestModel = Instantiate(_chestModel, _enemyPosition + new Vector3(0.0f, 1.0f, 0.0f), Quaternion.identity);
            _chestModel.SetActive(true);
            _chestDrop = true;
        }

        _animator.SetBool(_isEnemyDeadHash, true);

        _isEnemyDead = true;

        StartCoroutine(SelfDestruct());
    }

<<<<<<< Updated upstream
=======
    public void InstantiateLoot()
    {
        ItemObject droppedItem = GetComponent<LootBag>().GetDroppedItem();
        if (droppedItem != null)
        {
                int n = Random.Range(0, droppedItemPrefab.Length);
                GameObject lootGameObject = droppedItemPrefab[n];
                Instantiate(lootGameObject, new Vector3(-12.67f, 17.53f, 0f), Quaternion.identity);

                lootGameObject.GetComponent<SpriteRenderer>().sprite = droppedItem.uiDisplay;       
        }
    }

>>>>>>> Stashed changes
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

            _canEnemyAttack = true;
        }
    }
}

