using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WaveCounter : MonoBehaviour
{
    [SerializeField] private TMP_Text _txt;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _txt.text = "Wave number : " + Operator.Instance.WaveNumber;
    }
}
