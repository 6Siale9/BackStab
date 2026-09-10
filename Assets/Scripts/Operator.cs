using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Operator : MonoBehaviour
{
    private int _waveNumber = 0;
    private int _points = 0;

    [SerializeField] private GameObject _tester;

    private List<int> _nonEliteEnemies;
    private List<int> _eliteEnemies;
    private int _enemyTotal = 0;

    [SerializeField] private List<GameObject> _enemyTypes = new List<GameObject>();
    [SerializeField] private List<int> _enemyCosts = new List<int>();

    private void Start()
    {
        NewWave();
    }

    private void NewWave()
    {
        _waveNumber += 1;
        _points = _waveNumber;
        CreateSquad();
    }

    private void CreateSquad()
    {
        if (_nonEliteEnemies.Count > 0)
        {
            if (Random.Range(0f, 10f) > 30f)
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
                _points = _enemyCosts[i];
            }
        }
        else
        {
            int i = Random.Range(0, _enemyTypes.Count);
            _nonEliteEnemies.Add(i);
            _points = _enemyCosts[i];
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
        for (int i = 0; i < _eliteEnemies.Count; i++)
        {

        }
        for (int i = 0; i < _nonEliteEnemies.Count; i++)
        {

        }
    }

    public void OneMore(GameObject toSpawn, bool spawnElite)
    {

    }
}
