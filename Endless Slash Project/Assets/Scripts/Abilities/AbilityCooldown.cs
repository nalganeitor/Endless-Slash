using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class AbilityCooldown : MonoBehaviour
{
    [Header("Attack 1")]
    [SerializeField] BasicAttack basicAttack;
    [SerializeField] Berserk berserk;
    [SerializeField] FlameThrow flameThrow;
    [SerializeField] Whirlwind whirlwind;
    [SerializeField] GameObject _player;
    [SerializeField] Image _basicAttackIcon;
    [SerializeField] Image _berserkIcon;
    [SerializeField] Image _flameThrowIcon;
    [SerializeField] Image _whirlwindIcon;
    PlayerInput _playerInput;

  //  bool _basicAttackPerformed;
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

    void Awake()
    {
        _basicAttackIcon.fillAmount = 0;
        _whirlwindIcon.fillAmount = 0;
        _berserkIcon.fillAmount = 0;
        _flameThrowIcon.fillAmount = 0;

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
    }

    void AttackOneCooldown()
    {

        if (_isAttackOnePressed && !_onCooldownOne)
        {
            _onCooldownOne = true;
            _basicAttackIcon.fillAmount = 1;
        }

        if (_onCooldownOne)
        {
            _basicAttackIcon.fillAmount -= 1 / basicAttack._basicAttackCooldown * Time.deltaTime;

            if (_basicAttackIcon.fillAmount <= 0)
            {
                _basicAttackIcon.fillAmount = 0;
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

        if (_isAttackThreePressed && !_onCooldownFour)
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
