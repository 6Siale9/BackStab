using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class Pointer : MonoBehaviour
{
    private GameObject _toPoint;

    public GameObject ToPoint { get => _toPoint; set => _toPoint = value; }

    // Update is called once per frame
    void Update()
    {
        PointLogic();
    }

    private void PointLogic()
    {
        if (_toPoint != null)
        {
            Vector2 origin = transform.position;
            Vector2 dest = _toPoint.transform.position;
            Vector2 dir = dest - origin;
            transform.up = dir;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
