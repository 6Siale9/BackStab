using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Brick : MonoBehaviour
{
    private Vector2 _pos;
    private BrickSpawner _spawner;
    private int _up;
    private int _right;

    public Vector2 Pos { get => _pos; set => _pos = value; }
    public BrickSpawner Spawner { get => _spawner; set => _spawner = value; }

    public abstract void OnExplode();

    public void OnSpawn()
    {
        if (Random.Range(0, 8) <= 6)
        {
            _up = 1;
        }
        else
        {
            _up = -1;
        }
        if (Random.Range(0, 2) <= 0)
        {
            _right = 1;
        }
        else
        {
            _right = -1;
        }
        Placing();
    }

    private void Placing()
    {
        if (Random.Range(0, 20) <= 17)
        {
            _pos = new Vector2(_pos.x + _right, _pos.y);
        }
        else
        {
            _pos = new Vector2(_pos.x, _pos.y + _up);
        }

        if (Spawner.SpawnedBricks.Count > 0)
        {
            for (int i = 0; i < Spawner.SpawnedBricks.Count; i++)
            {
                if (_pos == Spawner.SpawnedBricks[i].Pos)
                {
                    Placing();
                    break;
                }
            }
        }
        transform.position = _pos;
    }
}
