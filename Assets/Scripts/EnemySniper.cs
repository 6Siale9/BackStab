using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Experimental.GraphView.GraphView;

public class EnemySniper : MonoBehaviour
{
    #region Attribut
    [Header("Attack")]
    [SerializeField] private float _windupTimeThreshold;
    [SerializeField] private float _lockedWindupTimeThreshold;
    [SerializeField] private float _blastRemainTimeThreshold;
    private float _attackTime; //Triggers when reaching _windupTimeThreshold, _attackTimeThreshold and _blastRemainTimeThreshold

    [Header("GameObject")]
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private GameObject _blast;
    [SerializeField] private Image _blastImg;
    [SerializeField] private Image _bodyImg;
    [SerializeField] private bool _elite = false;

    [Header("Behavior")]
    [SerializeField] private AiSniper _manager;
    private bool _attacking = false;
    private bool _hurt = false;
    private bool _canRotate = true;
    private float _hurtTime;
    private float _hurtThreshold;
    private Vector2 _hurtDir = Vector2.zero;
    private bool _condemned = false;
    private PlayerInput _lockedOnTarget;

    [Header("Colors")]
    [SerializeField] private Color _eliteGold;
    [SerializeField] private Color _baseRed;
    [SerializeField] private Color _hurtWhite;
    #endregion Attribut

    public bool Elite { get => _elite; set => _elite = value; }

    #region Method
    #region Base
    void Start()
    {
        CheckForAi();
    }

    void Update()
    {
        if (!_condemned)
        {
            OrientationLogic();
        }
        if (!_hurt)
        {
            AttackLogic();
        }
        else
        {
            HurtLogic();
        }
        if (Elite)
        {
            ColorLogic();
        }
    }
    #endregion Base

    private void CheckForAi()
    {
        if (AiSniper.Instance == null)
        {
            AiSniper manager = Instantiate(_manager);
            manager.Snipers.Add(this);
        }
        else
        {
            AiSniper.Instance.Snipers.Add(this);
        }
    }

    #region Logic
    private void OrientationLogic()
    {
        if (_canRotate)
        {
            if (_lockedOnTarget != null)
            {
                Vector2 origin = transform.position;
                Vector2 dest = _lockedOnTarget.transform.position;
                Vector2 dir = dest - origin;
                transform.up = dir.normalized;
            }
        }
    }

    private void AttackLogic()
    {
        if (_attacking)
        {
            _attackTime += Time.deltaTime;

            if (_attackTime < _windupTimeThreshold)
            {
                _blastImg.color = new Color(1, 1, 1, _attackTime * (1 / _windupTimeThreshold));
            }
            else if (_attackTime < _lockedWindupTimeThreshold)
            {
                _canRotate = false;
                _blastImg.color = Color.white;
            }
            else if (_attackTime < _blastRemainTimeThreshold)
            {
                _blast.SetActive(true);
                _blastImg.color = _baseRed;
            }
            else
            {
                ResetAttack();
            }
        }
    }

    private void HurtLogic()
    {
        _rb.velocity = _hurtDir * _hurtTime * 7f;
        if (_condemned)
        {
            transform.Rotate(Vector3.forward, _hurtTime * Time.deltaTime * 1000); //Spin the enemy
            _bodyImg.color = Color.white;
        }

        if (_hurtTime < _hurtThreshold)
        {
            _hurtTime += Time.deltaTime;
        }
        else
        {
            if (_condemned)
            {
                AiSniper.Instance.Snipers.Remove(this);
                Destroy(gameObject);
            }
            _hurt = false; //HurtLogic is only called if set to true
            _hurtTime = 0; //Will restart HurtLogic from the begining
            _bodyImg.color = _baseRed;
        }
    }

    private void ColorLogic()
    {
        if (_hurt)
        {
            if (_condemned)
            {
                _bodyImg.color = _hurtWhite;
            }
            else
            {
                _bodyImg.color = _baseRed;
            }
        }
        else
        {
            _bodyImg.color = _eliteGold;
        }
    }
    #endregion Logic

    public void OrderAttack(PlayerInput target)
    {
        if (!_hurt && !_attacking)
        {
            _lockedOnTarget = target;
            _attackTime = 0;
            _attacking = true;
        }
    }

    private void ResetAttack()
    {
        _blastImg.color = new Color(1, 1, 1, 0);
        _attacking = false;
        _blast.SetActive(false);
    }

    private void GotHit()
    {
        ResetAttack();
        _hurt = true;
        _hurtTime = 0;
        _hurtDir = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Arrow"))
        {
            ArrowController arrow = collision.gameObject.GetComponent<ArrowController>();
            if (arrow.Go)
            {
                if (!_elite || _hurt)
                {
                    _condemned = true;
                    GotHit();
                    If.Instance.Appear(transform);
                }
            }
            else
            {
                GotHit();
            }
        }
    }
    #endregion Method
}
