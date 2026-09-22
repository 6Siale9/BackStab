using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiSniper : MonoBehaviour
{
    #region Instance
    private static AiSniper _instance;
    public static AiSniper Instance { get => _instance; set => _instance = value; }
    private void SetInstance()
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
    #endregion Instance

    #region Attribut
    private List<EnemySniper> _snipers = new List<EnemySniper>(); //Every enemy snipers currently alive
    private List<EnemySniper> _snipersAvailable = new List<EnemySniper>(); //Every enemy snipers not attacking
    private EnemySniper _lastAttacker; //Last sniper who attacked
    private float _attackCd; //Activates Attack when reaching _attackCd
    private float _attackCdThreshold; //Randomly set with _attackCdMax and _attackCdMin
    [SerializeField] private float _attackCdMax;
    [SerializeField] private float _attackCdMin;
    #endregion Attribut

    #region Accessor
    public List<EnemySniper> Snipers { get => _snipers; set => _snipers = value; }
    public List<EnemySniper> SnipersAvailable { get => _snipersAvailable; set => _snipersAvailable = value; }
    #endregion Accessor

    #region Method
    private void Awake()
    {
        SetInstance();
    }

    public void OrderAttack(PlayerInput target)
    {
        if (Snipers.Count > 1) //If several snipers are alive, choose one at random (not the last who attacked) and fire it
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
            toGo[a].OrderAttack(target);
            _lastAttacker = toGo[a];
        }

        else if (Snipers.Count == 1) //If only one sniper is alive, fire it
        {
            Snipers[0].OrderAttack(target);
            _lastAttacker = Snipers[0];
        }

        else if (Snipers.Count == 0) //If no snipers are alive, unsubscribe and self destruct
        {
            Operator.Instance.CheckForWave();
            Destroy(gameObject);
        }

        _attackCdThreshold = Random.Range(_attackCdMin, _attackCdMax);
    }
    #endregion Method
}