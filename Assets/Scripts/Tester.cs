using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Tester : MonoBehaviour
{
    [SerializeField] private Collider2D _collider;

    private ContactFilter2D _filter;
    private List<Collider2D> _colliders = new List<Collider2D>();

    private Operator _operator;
    private GameObject _toSpawn;
    private bool _spawnElite = false;

    
    public Operator Operator { get => _operator; set => _operator = value; }
    public GameObject ToSpawn { get => _toSpawn; set => _toSpawn = value; }
    public bool SpawnElite { get => _spawnElite; set => _spawnElite = value; }

    private float _timer = 2f;

    private void Start()
    {
        _timer = Random.Range(.25f, 10f);
    }

    private void Update()
    {
        if (_timer > 0)
        {
            _timer -= Time.deltaTime;
        }
        else
        {
            GameObject go = Instantiate(_toSpawn, gameObject.transform.position, gameObject.transform.rotation);

            EnemySniper sniper = go.GetComponent<EnemySniper>();
            if (sniper != null)
            {
                sniper.Elite = _spawnElite;
            }
            //Add the next enemies here too !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!


            Destroy(gameObject);
        }
    }

    /*
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(gameObject);
        _operator.OneMore(_toSpawn, _spawnElite);
    }
    */
}
