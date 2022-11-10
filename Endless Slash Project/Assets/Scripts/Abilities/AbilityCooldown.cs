using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class AbilityCooldown : MonoBehaviour
{
    [Header("Attack 1")]
    [SerializeField] PlayerBasicAttack playerBasicAttack;
    [SerializeField] Berserk berserk;
    [SerializeField] Explosion explosion;
    [SerializeField] FlameThrow flameThrow;
    [SerializeField] ShieldBlock shieldBlock;
    [SerializeField] Whirlwind whirlwind;
    [SerializeField] GameObject _player;
    [SerializeField] Image _playerBasicAttackIcon;
    [SerializeField] Image _berserkIcon;
    [SerializeField] Image _explosionIcon;
    [SerializeField] Image _flameThrowIcon;
    [SerializeField] Image _shieldBlockIcon;
    [SerializeField] Image _whirlwindIcon;
    PlayerInput _playerInput;

    bool _isAttackOnePressed = false;
    bool _isAttackTwoPressed = false;
    bool _isAttackThreePressed = false;
    bool _isAttackFourPressed = false;
    bool _isAttackFivePressed = false;
    bool _isAttackSixPressed = false;
    bool _isAttackSevenPressed = false;
    bool _isAttackEightPressed = false;
    bool _onCooldownOne = false;
    bool _onCooldownTwo = false;
    bool _onCooldownThree = false;
    bool _onCooldownFour = false;
    bool _onCooldownFive = false;
    bool _onCooldownSix = false;

    void Awake()
    {
        _playerBasicAttackIcon.fillAmount = 0;
        _berserkIcon.fillAmount = 0;
        _explosionIcon.fillAmount = 0;
        _flameThrowIcon.fillAmount = 0;
        _shieldBlockIcon.fillAmount = 0;
        _whirlwindIcon.fillAmount = 0;

        _playerInput = new PlayerInput();

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
    }

    void Start()
    {

    }

    void Update()
    {
        AttackOneCooldown();
        AttackTwoCooldown();
        AttackThreeCooldown();
        AttackFourCooldown();
        AttackFiveCooldown();
        AttackSixCooldown();
    }

    void AttackOneCooldown()
    {

        if (_isAttackOnePressed && !_onCooldownOne)
        {
            _onCooldownOne = true;
            _playerBasicAttackIcon.fillAmount = 1;
        }

        if (_onCooldownOne)
        {
            _playerBasicAttackIcon.fillAmount -= 1 / playerBasicAttack._playerBasicAttackCooldown * Time.deltaTime;

            if (_playerBasicAttackIcon.fillAmount <= 0)
            {
                _playerBasicAttackIcon.fillAmount = 0;
                _onCooldownOne = false;
            }
        }
    }

    void AttackTwoCooldown()
    {

        if (_isAttackTwoPressed && !_onCooldownTwo)
        {
            _onCooldownTwo = true;
            _whirlwindIcon.fillAmount = 1;
        }

        if (_onCooldownTwo)
        {
            _whirlwindIcon.fillAmount -= 1 / whirlwind._whirlwindCooldown * Time.deltaTime;

            if (_whirlwindIcon.fillAmount <= 0)
            {
                _whirlwindIcon.fillAmount = 0;
                _onCooldownTwo = false;
            }
        }
    }

    void AttackThreeCooldown()
    {

        if (_isAttackThreePressed && !_onCooldownThree)
        {
            _onCooldownThree = true;
            _berserkIcon.fillAmount = 1;
        }

        if (_onCooldownThree)
        {
            _berserkIcon.fillAmount -= 1 / berserk._berserkCooldown * Time.deltaTime;

            if (_berserkIcon.fillAmount <= 0)
            {
                _berserkIcon.fillAmount = 0;
                _onCooldownThree = false;
            }
        }
    }


    void AttackFourCooldown()
    {

        if (_isAttackFourPressed && !_onCooldownFour)
        {
            _onCooldownFour = true;
            _flameThrowIcon.fillAmount = 1;
        }

        if (_onCooldownFour)
        {
            _flameThrowIcon.fillAmount -= 1 / flameThrow._flameThrowCooldown * Time.deltaTime;

            if (_flameThrowIcon.fillAmount <= 0)
            {
                _flameThrowIcon.fillAmount = 0;
                _onCooldownFour = false;
            }
        }
    }

    void AttackFiveCooldown()
    {

        if (_isAttackFivePressed && !_onCooldownFive)
        {
            _onCooldownFive = true;
            _explosionIcon.fillAmount = 1;
        }

        if (_onCooldownFive)
        {
            _explosionIcon.fillAmount -= 1 / explosion._explosionCooldown * Time.deltaTime;

            if (_explosionIcon.fillAmount <= 0)
            {
                _explosionIcon.fillAmount = 0;
                _onCooldownFive = false;
            }
        }
    }

    void AttackSixCooldown()
    {

        if (_isAttackSixPressed && !_onCooldownSix)
        {
            _onCooldownSix = true;
            _shieldBlockIcon.fillAmount = 1;
        }

        if (_onCooldownSix)
        {
            _shieldBlockIcon.fillAmount -= 1 / shieldBlock._shieldBlockCooldown * Time.deltaTime;

            if (_shieldBlockIcon.fillAmount <= 0)
            {
                _shieldBlockIcon.fillAmount = 0;
                _onCooldownSix = false;
            }
        }
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
}
