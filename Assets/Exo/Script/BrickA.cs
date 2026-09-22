using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickA : Brick
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void OnExplode()
    {
        Debug.Log("A");
    }
}
