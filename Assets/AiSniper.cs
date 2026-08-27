using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiSniper : MonoBehaviour
{
    [SerializeField] private List<EnemySniper> _snipers = new List<EnemySniper>();
    private EnemySniper _lastAttacker;

    private float _attackCd;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        AttackLogic();
    }

    private void AttackLogic()
    {
        if (_attackCd >= 0)
        {
            _attackCd -= Time.deltaTime;
        }
        else
        {
            Attack();
        }
    }

    private void Attack()
    {

        if (_snipers.Count > 1)
        {
            List<EnemySniper> toGo = new List<EnemySniper>();
            for (int i = 0; i < _snipers.Count; i++)
            {
                if (_snipers[i] != _lastAttacker)
                {
                    toGo.Add(_snipers[i]);
                }
            }
            int a = Random.Range(0, toGo.Count);
            toGo[a].OrderAttack();
            _lastAttacker = toGo[a];
        }
        else if (_snipers.Count == 1)
        {
            _snipers[0].OrderAttack();
            _lastAttacker = _snipers[0];
        }
        else if (_snipers.Count == 0)
        {
            Destroy(gameObject);
        }
        _attackCd = Random.Range(0, 1.75f);
    }
}