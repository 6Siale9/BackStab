using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Experimental.GraphView.GraphView;

public class EnemySniper: MonoBehaviour
{
    private List<PlayerInput> _targets = new List<PlayerInput>();
    private GameObject _lockedOnTarget;

    private bool _elite = false;

    private float _windUpTime;
    private float _attackTime;
    private float _blastRemainTime;
    private bool _attacking = false;
    private bool _canRotate = true;
    private bool _damageTiming = false;

    [SerializeField] private Image _blast;

    // Start is called before the first frame update
    void Start()
    {
        GetAllPlayer();
        OrderAttack(); //TEST !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
    }

    // Update is called once per frame
    void Update()
    {
        AttackLogic();
        SetOrientation();
        Attack();





        // Debug !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
        if (Input.GetKeyDown(KeyCode.Keypad0))
        {
            OrderAttack();
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


    private void AttackLogic()
    {
        if (_windUpTime >= 0 && _attacking)
        {
            _windUpTime -= Time.deltaTime;
            _blast.color = new Color(1, 1, 1, (0.75f-_windUpTime));
        }
        else if (_windUpTime < 0)
        {
            _windUpTime = 0;
            _attacking = false;
            _canRotate = false;
            _attackTime = 0.25f;
            _blastRemainTime = .5f;
            _damageTiming = true;
            _blast.color = new Color(1, 1, 1, 1);
        }
    }

    private void Attack()
    {
        if (_damageTiming)
        {
        if (_attackTime >= 0)
        {
            _attackTime -= Time.deltaTime;
            Debug.Log("1");
        }
        else if (_blastRemainTime >= 0)
        {
            _blastRemainTime -= Time.deltaTime;
            _blast.color = new Color(1, 0, 0.3137255f, 1);
            Debug.Log("2");
        }
        else
        {
            _canRotate = true;
            _blast.color = new Color(1, 1, 1, 0);
            Debug.Log("3");
            _damageTiming = false;
        }
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
}
