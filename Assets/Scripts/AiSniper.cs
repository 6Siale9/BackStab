using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiSniper : MonoBehaviour
{
    private static AiSniper _instance;
    public static AiSniper Instance { get => _instance; set => _instance = value; }
    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }




    [SerializeField] private List<EnemySniper> _snipers = new List<EnemySniper>();

    private EnemySniper _lastAttacker;

    private float _attackCd;

    public List<EnemySniper> Snipers { get => _snipers; set => _snipers = value; }


    // Start is called before the first frame update
    void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
        for (int i = 0; i < _snipers.Count; i++)
        {
            _snipers[i].AiSuperior = this;
        }
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

        if (Snipers.Count > 1)
        {
            List<EnemySniper> toGo = new List<EnemySniper>();
            for (int i = 0; i < Snipers.Count; i++)
            {
                if (Snipers[i] != _lastAttacker)
                {
                    toGo.Add(Snipers[i]);
                }
            }
            int a = Random.Range(0, toGo.Count);
            toGo[a].OrderAttack();
            _lastAttacker = toGo[a];
        }
        else if (Snipers.Count == 1)
        {
            Snipers[0].OrderAttack();
            _lastAttacker = Snipers[0];
        }
        else if (Snipers.Count == 0)
        {

            Destroy(gameObject);
        }
        _attackCd = Random.Range(0, 1.75f);
    }
}