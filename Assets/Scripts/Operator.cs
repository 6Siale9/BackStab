using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Operator : MonoBehaviour
{
    #region Instance
    static private Operator instance;
    public static Operator Instance { get => instance; set => instance = value; }

    void Awake()
    {
        TrySetInstance();
    }

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

    private List<GameObject> _managers = new List<GameObject>();

    private int _waveNumber = 0;
    private int _points = 0;

    [SerializeField] private GameObject _tester;

    private List<int> _nonEliteEnemies = new List<int>();
    private List<int> _eliteEnemies = new List<int>();

    private float _range = 0;
    private float _timer = 0;
    private bool _launchWave = false;

    [SerializeField] private List<GameObject> _enemyTypes = new List<GameObject>();
    [SerializeField] private List<int> _enemyCosts = new List<int>();

    public List<GameObject> Managers { get => _managers; set => _managers = value; }

    private void Start()
    {
        NewWave();
    }

    private void Update()
    {
        NextWaveTimer();
    }

    private void NextWaveTimer()
    {
        if (_timer > 0)
        {
            _timer -= Time.deltaTime;
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

    public void CheckForWave()
    {
        if (_managers.Count == 0)
        {
            _timer = 5f;
            _launchWave= true;
        }
    }

    private void NewWave()
    {
        _waveNumber += 1;
        _points = _waveNumber;
        CreateSquad();
    }

    private void CreateSquad()
    {
        if (_eliteEnemies.Count != 0)
        {
            _eliteEnemies.Clear();
        }
        if (_nonEliteEnemies.Count != 0)
        {
            _nonEliteEnemies.Clear();
        }


        if (_nonEliteEnemies.Count > 0)
        {
            if (Random.Range(0f, 1f) < 0.3f)
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
            CreateSquad();
        }
        else
        {
            SpawnEverything();
        }
    }

    private void SpawnEverything()
    {
        _range = _eliteEnemies.Count + _nonEliteEnemies.Count;
        
        for (int i = 0; i < _eliteEnemies.Count; i++)
        {
            // DECLARE LA //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            switch(_eliteEnemies[i])
            {
                case 0:
                    GameObject go = Instantiate(_tester, new Vector3(Random.Range(-_range, _range), Random.Range(-_range, _range), 0), gameObject.transform.rotation);
                    Tester tester = go.GetComponent<Tester>();
                    tester.SpawnElite = true;
                    tester.ToSpawn = _enemyTypes[0];
                    break;

                case 1:
                    GameObject go1 = Instantiate(_tester, new Vector3(Random.Range(-_range, _range), Random.Range(-_range, _range), 0), gameObject.transform.rotation);
                    Tester tester1 = go1.GetComponent<Tester>();
                    tester1.SpawnElite = true;
                    tester1.ToSpawn = _enemyTypes[1];
                    break;

                case 2:
                    GameObject go2 = Instantiate(_tester, new Vector3(Random.Range(-_range, _range), Random.Range(-_range, _range), 0), gameObject.transform.rotation);
                    Tester tester2 = go2.GetComponent<Tester>();
                    tester2.SpawnElite = true;
                    tester2.ToSpawn = _enemyTypes[2];
                    break;

                case 3:
                    GameObject go3 = Instantiate(_tester, new Vector3(Random.Range(-_range, _range), Random.Range(-_range, _range), 0), gameObject.transform.rotation);
                    Tester tester3 = go3.GetComponent<Tester>();
                    tester3.SpawnElite = true;
                    tester3.ToSpawn = _enemyTypes[3];
                    break;

                case 4:
                    GameObject go4 = Instantiate(_tester, new Vector3(Random.Range(-_range, _range), Random.Range(-_range, _range), 0), gameObject.transform.rotation);
                    Tester tester4 = go4.GetComponent<Tester>();
                    tester4.SpawnElite = true;
                    tester4.ToSpawn = _enemyTypes[4];
                    break;
            }
        }
        for (int i = 0; i < _nonEliteEnemies.Count; i++)
        {
            switch (_nonEliteEnemies[i])
            {
                case 0:
                    Instantiate(_enemyTypes[0], new Vector3(Random.Range(-_range, _range), Random.Range(-_range, _range), 0), gameObject.transform.rotation);
                    break;

                case 1:
                    Instantiate(_enemyTypes[1], new Vector3(Random.Range(-_range, _range), Random.Range(-_range, _range), 0), gameObject.transform.rotation);
                    break;

                case 2:
                    Instantiate(_enemyTypes[2], new Vector3(Random.Range(-_range, _range), Random.Range(-_range, _range), 0), gameObject.transform.rotation);
                    break;

                case 3:
                    Instantiate(_enemyTypes[3], new Vector3(Random.Range(-_range, _range), Random.Range(-_range, _range), 0), gameObject.transform.rotation);
                    break;

                case 4:
                    Instantiate(_enemyTypes[4], new Vector3(Random.Range(-_range, _range), Random.Range(-_range, _range), 0), gameObject.transform.rotation);
                    break;
            }
        }
    }

    public void OneMore(GameObject toSpawn, bool spawnElite)
    {
        GameObject go = Instantiate(_tester, new Vector3(Random.Range(-_range, _range), Random.Range(-_range, _range), 0), gameObject.transform.rotation);
        Tester tester = go.GetComponent<Tester>();
        tester.SpawnElite = spawnElite;
        tester.ToSpawn = toSpawn;
    }
}
