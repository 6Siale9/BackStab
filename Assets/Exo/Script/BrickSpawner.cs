using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickSpawner : MonoBehaviour
{
    [SerializeField] private Brick[] _bricks = null;
    [SerializeField] private int _howMany;
    private List<Brick> _spawnedBricks = new List<Brick>();
    //[SerializeField] private Dictionary<float, Brick> _bricks;

    public List<Brick> SpawnedBricks { get => _spawnedBricks; set => _spawnedBricks = value; }

    void Start()
    {
        SpawnEverything();
        KillEverything();
    }

    private void SpawnEverything()
    {
        for (int i = 0; i < _howMany; i++)
        {
            /*
            float f = 0;
            foreach (var item in _bricks)
            {
                f += item.Key;
            }
            float a = Random.Range(0, f);
            Brick toSpawn = null;
            foreach (var item in _bricks)
            {
                if (a > f)
                {
                    a -= f;
                }
                else
                {
                    toSpawn = item.Value;
                }
            }
            */
            Brick brick = Instantiate(_bricks[0], transform);
            brick.Spawner = this;
            brick.OnSpawn();
            _spawnedBricks.Add(brick);
        }
    }

    private void KillEverything()
    {
        for (int i = 0; i < SpawnedBricks.Count; i++)
        {
            _spawnedBricks[i].OnExplode();
        }
    }

    private void NextCoord()
    {
        
    }
}
