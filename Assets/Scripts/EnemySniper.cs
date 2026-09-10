using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Experimental.GraphView.GraphView;

public class EnemySniper : MonoBehaviour
{
    private List<PlayerInput> _targets = new List<PlayerInput>();
    private GameObject _lockedOnTarget;

    private AiSniper _aiSuperior;

    [SerializeField] private Rigidbody2D _rb;

    [SerializeField] private bool _elite = false;

    private float _windUpTime;
    private float _attackTime;
    private float _blastRemainTime;
    private bool _attacking = false;
    private bool _canRotate = true;
    private bool _damageTiming = false;

    [SerializeField] private GameObject _blast;

    [SerializeField] private Image _blastImg;
    [SerializeField] private Image _body;

    private bool _hurt = false;
    private float _hurtTime;
    private Vector2 _hurtDir = Vector2.zero;
    private bool _condemnt = false;

    public AiSniper AiSuperior { get => _aiSuperior; set => _aiSuperior = value; }
    public bool Elite { get => _elite; set => _elite = value; }

    // Start is called before the first frame update
    void Start()
    {
        GetAllPlayer();
        OrderAttack(); //TEST !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
    }

    // Update is called once per frame
    void Update()
    {
        if (!_hurt)
        {
            SetOrientation();
            AttackLogic();
            Attack();
        }
        else
        {
            Hurt();
        }
        if (Elite)
        {
            ColorLogic();
        }




        // Debug !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
        if (Input.GetKeyDown(KeyCode.Keypad0))
        {
            Debug.Log("Debug");
            GotHit();
        }
    }

    private void ColorLogic()
    {
        if (_hurt)
        {
            _body.color = new Color(1, 1, 1, 1);
        }
        else
        {
            _body.color = new Color(0.8207547f, 0.7538613f, 0.2051887f, 1);
        }
    }

    private void GetAllPlayer()
    {
        for (int i = 0; i < GlobalManager.Instance.Players.Count; i++)
        {
            if (GlobalManager.Instance.Players[i].Hp > 0)
            {
                _targets.Add(GlobalManager.Instance.Players[i]);
            }
        }
    }

    public void OrderAttack()
    {
        if (!_hurt && !_attacking)
        {
        float dist = 0;
        for (int i = 0; i < _targets.Count; i++)
        {
            if (i == 0)
            {
                Vector2 origin = transform.position;
                Vector2 dest = _targets[i].transform.position;
                Vector2 dir = dest - origin;
                dist = dir.magnitude;
                _lockedOnTarget = _targets[i].gameObject;
            }
            else
            {
                Vector2 origin = transform.position;
                Vector2 dest = _targets[i].transform.position;
                Vector2 dir = dest - origin;
                if (dir.magnitude < dist)
                {
                    dist = dir.magnitude;
                    _lockedOnTarget = _targets[i].gameObject;
                }
            }
        }
        _windUpTime = .75f;
        _attacking = true;
        }
    }


    private void AttackLogic()
    {
        if (_windUpTime >= 0 && _attacking)
        {
            _windUpTime -= Time.deltaTime;
            _blastImg.color = new Color(1, 1, 1, (0.75f - _windUpTime));
        }
        else if (_windUpTime < 0)
        {
            _windUpTime = 0;
            _attacking = false;
            _canRotate = false;
            _attackTime = 0.25f;
            _blastRemainTime = .5f;
            _damageTiming = true;
            _blastImg.color = new Color(1, 1, 1, 1);
        }
    }

    private void Attack()
    {
        if (_damageTiming)
        {
            if (_attackTime >= 0)
            {
                _attackTime -= Time.deltaTime;
            }
            else if (_blastRemainTime >= 0)
            {
                _blast.SetActive(true);
                _blastRemainTime -= Time.deltaTime;
                _blastImg.color = new Color(1, 0, 0.3137255f, 1);
            }
            else
            {
                _blast.SetActive(false);
                _canRotate = true;
                _blastImg.color = new Color(1, 1, 1, 0);
                _damageTiming = false;
            }
        }
    }

    private void GotHit()
    {
        _blast.SetActive(false);
        _hurt = true;
        _hurtTime = 1.5f;
        _blastImg.color = new Color(1, 1, 1, 0);
        _hurtDir = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
    }

    private void Hurt()
    {
        transform.Rotate(Vector3.forward, _hurtTime * Time.deltaTime * 360);
        _rb.velocity = _hurtDir * _hurtTime * 2.5f;
        if (_hurtTime > 0)
        {
            _hurtTime -= Time.deltaTime;
        }
        else
        {
            if (_condemnt)
            {
                Destroy(gameObject);
                _aiSuperior.Snipers.Remove(this);
            }
            _hurt = false;
            _hurtTime = 0;
        }
    }

    private void SetOrientation()
    {
        if (_canRotate)
        {
            Vector2 origin = transform.position;
            Vector2 dest = _lockedOnTarget.transform.position;
            Vector2 dir = dest - origin;
            transform.up = dir.normalized;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Arrow"))
        {
            ArrowController arrow = collision.gameObject.GetComponent<ArrowController>();
            if (arrow.Go)
            {
                if (!Elite || _hurt)
                {
                    _condemnt = true;
                }
            }
            GotHit();
        }
    }
}
