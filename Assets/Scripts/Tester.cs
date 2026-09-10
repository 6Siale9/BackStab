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



    private void Start()
    {
        //int i = _collider.OverlapCollider(_filter, _colliders); // !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
        if (false/*i > 0*/)
        {
            _operator.OneMore(_toSpawn, _spawnElite);
            Destroy(gameObject);
        }
        else
        {
            GameObject go = Instantiate(_toSpawn);
            
            EnemySniper sniper = go.GetComponent<EnemySniper>();
            if (sniper != null)
            {
                sniper.Elite = _spawnElite;
            }

            //Add the next enemies here too !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!



            Destroy(gameObject);
        }
    }
}
