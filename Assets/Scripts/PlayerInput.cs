using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    private PlayerControls _controls;

    private Rigidbody2D _rb;

    [SerializeField] private EInputMode _inputMode;
    
    private Vector2 _directionController;

    private float _upAxisValueKeyboard = 0f;
    private float _downAxisValueKeyboard = 0f;
    private float _rightAxisValueKeyboard = 0f;
    private float _leftAxisValueKeyboard = 0f;

    [SerializeField] private float _moveSpeedValue = 1f;
    private float _moveSpeed = 1f;

    [Header("Dash")]
    [SerializeField] private float _dashMoveSpeed = 2f;
    [SerializeField] private float _dashCdValue = 2f;
    [SerializeField] private float _dashSpeedDecrease = 1f;
    private float _dashCd = 0f;

    [Header("Shoot")]
    [SerializeField] private GameObject _arrowPrefab;
    [SerializeField] private int _arrowCapacity = 1;
    [SerializeField] private float _arrowSpeed = 1;
    [SerializeField] private float _arrowStopCd = 1;
    private List<ArrowController> _arrowsFired = new List<ArrowController>();

    public List<ArrowController> ArrowsFired { get => _arrowsFired; set => _arrowsFired = value; }

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _controls = new PlayerControls();

        _moveSpeed = _moveSpeedValue;

        _controls.GameplayKeyboard.MoveUp.performed += ctx => _upAxisValueKeyboard = 1;
        _controls.GameplayKeyboard.MoveUp.canceled += ctx => _upAxisValueKeyboard = 0;

        _controls.GameplayKeyboard.MoveDown.performed += ctx => _downAxisValueKeyboard = 1;
        _controls.GameplayKeyboard.MoveDown.canceled += ctx => _downAxisValueKeyboard = 0;

        _controls.GameplayKeyboard.MoveLeft.performed += ctx => _leftAxisValueKeyboard = 1;
        _controls.GameplayKeyboard.MoveLeft.canceled += ctx => _leftAxisValueKeyboard = 0;

        _controls.GameplayKeyboard.MoveRight.performed += ctx => _rightAxisValueKeyboard = 1;
        _controls.GameplayKeyboard.MoveRight.canceled += ctx => _rightAxisValueKeyboard = 0;

        _controls.GameplayKeyboard.Dash.performed += ctx => Dash();

        _controls.GameplayKeyboard.Enable();

        _controls.GameplayController.Move.performed += ctx => ReadVectorValue(ctx.ReadValue<Vector2>());
        _controls.GameplayController.Move.canceled += ctx => ReadVectorValue(ctx.ReadValue<Vector2>());

        _controls.GameplayController.Dash.performed += ctx => Dash();

        _controls.GameplayController.Attack.performed += ctx => Shoot();

        _controls.GameplayController.Special.performed += ctx => Return();

        _controls.GameplayController.Enable();
    }

    private void Update()
    {
        switch (_inputMode)
        {
            case EInputMode.Keyboard :
                VelocityKeyboard();
            break;
            case EInputMode.Controller :
                VelocityController();
            break;
        }
        DashLogic();
    }

    private void VelocityKeyboard()
    {
        _rb.velocity = new Vector2(_rightAxisValueKeyboard + -_leftAxisValueKeyboard, _upAxisValueKeyboard + -_downAxisValueKeyboard).normalized * _moveSpeed;
    }

    private void VelocityController()
    {
        _rb.velocity = _directionController.normalized * _moveSpeed;
    }

    private void ReadVectorValue(Vector2 vector)
    {
        if (_inputMode == EInputMode.Controller)
        {
            _directionController = vector;
        }
    }

    private void Dash()
    {
        Debug.Log("Dash");
        if (_dashCd == 0)
        {
            _dashCd = _dashCdValue;
            _moveSpeed = _dashMoveSpeed;
        }
    }

    private void DashLogic()
    {
        if (_dashCd > 0)
        {
            _dashCd -= Time.deltaTime;
        }
        else if (_dashCd < 0)
        {
            _dashCd = 0;
        }
        if (_moveSpeed <= _moveSpeedValue)
        {
            _moveSpeed = _moveSpeedValue;
        }
        else if (_moveSpeed >= _moveSpeedValue)
        {
            _moveSpeed -= Time.deltaTime * _dashSpeedDecrease * _moveSpeedValue;
        }
    }

    private void Shoot()
    {
        if (ArrowsFired.Count < _arrowCapacity)
        {
            GameObject arrow = Instantiate(_arrowPrefab, gameObject.transform.position, gameObject.transform.rotation);
            Rigidbody2D arrowRb = arrow.GetComponent<Rigidbody2D>();
            arrowRb.velocity = _rb.velocity.normalized * _arrowSpeed;
            ArrowController controller = arrow.GetComponent<ArrowController>();
            ArrowsFired.Add(controller);
            controller.Player = this;
            controller.Speed = _arrowSpeed;
            controller.StopCd = _arrowStopCd;
        }
    }

    private void Return()
    {
        for (int i = 0; i < _arrowsFired.Count; i++)
        {
            _arrowsFired[i].Return();
        }
    }
}
