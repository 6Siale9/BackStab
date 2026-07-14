using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowController : MonoBehaviour
{
    private float _stopCd = 1;

    private bool _stopped = false;

    private float _speed = 0;
    
    private bool _go = false;

    private Rigidbody2D _rb;

    private PlayerInput _player;







    public float Speed { get => _speed; set => _speed = value; }
    public PlayerInput Player { get => _player; set => _player = value; }
    public float StopCd { get => _stopCd; set => _stopCd = value; }

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        StopLogic();
        GoLogic();
    }

    private void StopLogic()
    {
        if (StopCd > 0)
        {
            StopCd -= Time.deltaTime;
        }
        else if (!_stopped)
        {
            _stopped = true;
            _rb.velocity = Vector2.zero;
        }
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
        if (collision.tag == "player" && _stopped)
        {
            _player.ArrowsFired.Remove(this);
            Destroy(gameObject);
        }
    }

    private void GoLogic()
    {
        if (_go)
        {
            _rb.velocity = (_player.transform.position - gameObject.transform.position).normalized * _speed;
        }
    }
}
