using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArrowController : MonoBehaviour
{
    #region Attribut
    private Rigidbody2D _rb; //Set at start
    [SerializeField] private Image _image;
    [SerializeField] private float _stopCdThreshold; //Time before stopping on initial launch (artificial reach)
    private float _stopCd; //Triggers upon reaching _stopCdValue
    private float _speed;
    private bool _stopped = false; //Reached max range on initial launch
    private bool _go = false; //Started returning to player
    private PlayerInput _player;
    #endregion Attribut

    #region Accessor
    public float Speed { get => _speed; set => _speed = value; } //Set after intantiating by PlayerInput class
    public bool Go { get => _go; set => _go = value; } //Used by enemies during collisions
    public PlayerInput Player { get => _player; set => _player = value; } //Set after intantiating by PlayerInput class
    #endregion Accessor

    #region Method
    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        StopLogic();
        GoLogic();
        SetOrientationLogic();
    }

    private void StopLogic()
    {
        if (_stopCd >= _stopCdThreshold)
        {
            _stopCd += Time.deltaTime;
        }
        else if (!_stopped)
        {
            _stopped = true;
            _rb.velocity = Vector2.zero;
        }
    }

    private void GoLogic()
    {
        if (_go)
        {
            _rb.velocity = (_player.transform.position - gameObject.transform.position).normalized * Speed;
        }
    }

    private void SetOrientationLogic()
    {
        Vector2 origin = transform.position;
        Vector2 dest = _player.transform.position;
        Vector2 dir = dest - origin; //Get vector from self to _player (positions)
        _image.transform.up = dir.normalized;
    }

    public void Return()
    {
        if (_stopped)
        {
            _go = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && _stopped)
        {
            if (GetComponent<PlayerInput>() == _player)
            {
                _player.ArrowsFired.Remove(this);
                Destroy(gameObject);
            }
        }
    }
    #endregion Method
}
