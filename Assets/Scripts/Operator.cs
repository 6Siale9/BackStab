using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Operator : MonoBehaviour
{
    #region Instance
    static private Operator instance;
    public static Operator Instance { get => instance; set => instance = value; }

    private void TrySetInstance()
    {
        if (Operator.Instance == null)
        {
            Operator.Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    #endregion Instance

    #region Attribut
    [Header("Spawn")]
    [SerializeField] private int _waveNumber = 0; //SerializeField for debug and test purpose
    [SerializeField] private List<GameObject> _enemyTypes = new List<GameObject>();
    [SerializeField] private List<int> _enemyCosts = new List<int>();
    [SerializeField] private GameObject _tester;
    private int _points = 0;
    private List<int> _nonEliteEnemies = new List<int>();
    private List<int> _eliteEnemies = new List<int>();
    private float _range = 0;

    [Header("Attack")]
    private List<GameObject> _managers = new List<GameObject>();
    [SerializeField] private float _attackTimerMax;
    [SerializeField] private float _attackTimerMin;
    private float _attackTimerThreshold;
    private float _attackTimer;
    private bool _launchAttack = false;

    [Header("Wave")]
    [SerializeField] private float _waveTimerThreshold;
    private float _waveTimer = 0;
    private bool _launchWave = false;

    [Header("Base")]
    [SerializeField] private bool _justSet = true; // Change to false if in scene in inspector
    #endregion Attribut

    public List<GameObject> Managers { get => _managers; set => _managers = value; }

    #region Method
    void Awake()
    {
        if (!_justSet) //Only allow GlobalMangers in scene to fight for instance
        {
            TrySetInstance();
        }
    }

    private void Start()
    {
        NewWave();
    }

    private void Update()
    {
        NextWaveTimerLogic();
        AttackTimerLogic();
    }

    #region Logic
    private void NextWaveTimerLogic()
    {
        if (_waveTimer < _waveTimerThreshold)
        {
            _waveTimer += Time.deltaTime;
        }
        else
        {
            if (_launchWave)
            {
                NewWave();
                _launchWave = false;
            }
        }
    }

    private void AttackTimerLogic()
    {
        if (_attackTimer < _attackTimerThreshold)
        {
            _attackTimer += Time.deltaTime;
        }
        else
        {
            if (_launchAttack)
            {
                Attack();
                _launchAttack = false;
            }
        }
    }
    #endregion Logic

    #region Wave management

    public void CheckForWave() //Called when an empty manager self destruct
    {
        if (_managers.Count == 0)
        {
            _waveTimer = 5f;
            _launchWave= true;
        }
    }

    private void NewWave()
    {
        _waveNumber += 1;
        _points = _waveNumber;
        ClearForNewSquad();
        NewSquad();
        for (int i = 0; i < GlobalManager.Instance.AllPlayers.Count; i++) //Heal all players
        {
            GlobalManager.Instance.AllPlayers[i].Heal();
            /*
            if (GlobalManager.Instance.ActivePlayers.Find(GlobalManager.Instance.AllPlayers[i])) 
            {
                Sylvain !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
            }
            */
        }
    }

    #region Spawning
    private void ClearForNewSquad()
    {
        if (_eliteEnemies.Count != 0)
        {
            _eliteEnemies.Clear();
        }
        if (_nonEliteEnemies.Count != 0)
        {
            _nonEliteEnemies.Clear();
        }
    }

    private void NewSquad()
    {
        if (_nonEliteEnemies.Count > 0) //Roll for enemy level up only if there are basic enemies
        {
            if (Random.Range(0f, 1f) < 0.45f)
            {
                int i = Random.Range(0, _nonEliteEnemies.Count);
                _eliteEnemies.Add(_nonEliteEnemies[i]);
                _points -= _enemyCosts[_nonEliteEnemies[i]];
                _nonEliteEnemies.Remove(_nonEliteEnemies[i]);
            }
            else
            {
                int i = Random.Range(0, _enemyTypes.Count);
                _nonEliteEnemies.Add(i);
                _points -= _enemyCosts[i];
            }
        }
        else
        {
            int i = Random.Range(0, _enemyTypes.Count);
            _nonEliteEnemies.Add(i);
            _points -= _enemyCosts[i];
        }
        if (_points > 0)
        {
            NewSquad();
        }
        else
        {
            SpawnEverything();
        }
    }

    private void SpawnEverything()
    {
        _range = (_eliteEnemies.Count + _nonEliteEnemies.Count) + 5;
        
        for (int i = 0; i < _eliteEnemies.Count; i++)
        {
            Vector3 v = RandomVector();
            switch (_eliteEnemies[i])
            {
                case 0:
                    GameObject go = Instantiate(_tester, v, gameObject.transform.rotation);
                    Tester tester = go.GetComponent<Tester>();
                    tester.SpawnElite = true;
                    tester.ToSpawn = _enemyTypes[0];
                    tester.Operator = this;
                    break;

                case 1:
                    GameObject go1 = Instantiate(_tester, v, gameObject.transform.rotation);
                    Tester tester1 = go1.GetComponent<Tester>();
                    tester1.SpawnElite = true;
                    tester1.ToSpawn = _enemyTypes[1];
                    tester1.Operator = this;
                    break;

                case 2:
                    GameObject go2 = Instantiate(_tester, v, gameObject.transform.rotation);
                    Tester tester2 = go2.GetComponent<Tester>();
                    tester2.SpawnElite = true;
                    tester2.ToSpawn = _enemyTypes[2];
                    tester2.Operator = this;
                    break;

                case 3:
                    GameObject go3 = Instantiate(_tester, v, gameObject.transform.rotation);
                    Tester tester3 = go3.GetComponent<Tester>();
                    tester3.SpawnElite = true;
                    tester3.ToSpawn = _enemyTypes[3];
                    tester3.Operator = this;
                    break;

                case 4:
                    GameObject go4 = Instantiate(_tester, v, gameObject.transform.rotation);
                    Tester tester4 = go4.GetComponent<Tester>();
                    tester4.SpawnElite = true;
                    tester4.ToSpawn = _enemyTypes[4];
                    tester4.Operator = this;
                    break;
            }
        } //Maybe one day I will clean this
        for (int u = 0; u < _nonEliteEnemies.Count; u++)
        {
            Vector3 v = RandomVector();
            switch (_nonEliteEnemies[u])
            {
                case 0:
                    GameObject go = Instantiate(_tester, v, gameObject.transform.rotation);
                    Tester tester = go.GetComponent<Tester>();
                    tester.SpawnElite = false;
                    tester.ToSpawn = _enemyTypes[0];
                    tester.Operator = this;
                    break;

                case 1:
                    GameObject go1 = Instantiate(_tester, v, gameObject.transform.rotation);
                    Tester tester1 = go1.GetComponent<Tester>();
                    tester1.SpawnElite = false;
                    tester1.ToSpawn = _enemyTypes[1];
                    tester1.Operator = this;
                    break;

                case 2:
                    GameObject go2 = Instantiate(_tester, v, gameObject.transform.rotation);
                    Tester tester2 = go2.GetComponent<Tester>();
                    tester2.SpawnElite = false;
                    tester2.ToSpawn = _enemyTypes[2];
                    tester2.Operator = this;
                    break;

                case 3:
                    GameObject go3 = Instantiate(_tester, v, gameObject.transform.rotation);
                    Tester tester3 = go3.GetComponent<Tester>();
                    tester3.SpawnElite = false;
                    tester3.ToSpawn = _enemyTypes[3];
                    tester3.Operator = this;
                    break;

                case 4:
                    GameObject go4 = Instantiate(_tester, v, gameObject.transform.rotation);
                    Tester tester4 = go4.GetComponent<Tester>();
                    tester4.SpawnElite = false;
                    tester4.ToSpawn = _enemyTypes[4];
                    tester4.Operator = this;
                    break;
            }
        } //Maybe one day I will clean this
    }
    #endregion Spawning
    #endregion Wave Management

    #region Attack
    private void Attack()
    {
        SetNewThreshold();
        for (int i = 0; i < GlobalManager.Instance.ActivePlayers.Count; i++)
        {
            AttackPlayer(GlobalManager.Instance.ActivePlayers[i]);
        }
    }

    private void SetNewThreshold()
    {
        _attackTimerThreshold = Random.Range(_attackTimerMin, _attackTimerMax);
    }

    private void AttackPlayer(PlayerInput target)
    {
        List<GameObject> am = _managers; //am for available managers

        for (int i = 0; i < _managers.Count; i++)
        {

            AiSniper sniper = _managers[i].GetComponent<AiSniper>();
            if (sniper != null)
            {
                if (sniper.Snipers.Count > 1)
                {

                }
            }
            else
            {
                //Other enemy types here !!
            }





        }


        Random.Range(0, _managers.Count);
    }
    #endregion Attack

    public void OneMore(GameObject toSpawn, bool spawnElite)
    {
        GameObject go = Instantiate(_tester, new Vector3(Random.Range(-_range, _range), Random.Range(-_range, _range), 0), gameObject.transform.rotation);
        Tester tester = go.GetComponent<Tester>();
        tester.SpawnElite = spawnElite;
        tester.ToSpawn = toSpawn;
        tester.Operator = this;
    }

    private Vector3 RandomVector()
    {
        return new Vector3(Random.Range(-_range, _range), Random.Range(-_range, _range), 0);
    }
    #endregion Method
}
