using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.UI;

public class Background : MonoBehaviour
{
    [SerializeField] private Image _img;
    private float _r;
    private float _g;
    private float _b;
    private float _targetR;
    private float _targetG;
    private float _targetB;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ColorLogic();
    }

    private void ColorLogic()
    {
        if (_r == _targetR && _g == _targetG && _b == _targetB)
        {
            SetNewColor();
        }
        else
        {
            UpdateColor();
        }
    }

    private void SetNewColor()
    {
        int i = Random.Range(0, 3);
        
        _targetR = 0;
        _targetG = 0;
        _targetB = 0;

        if (i == 3)
        {
            _targetR = 1;
            i = Random.Range(0, 2);
            if (i == 1)
            {
                i = Random.Range(0, 2);
                if (i == 1)
                {
                    _targetG = 1;
                }
                else
                {
                    _targetB = 1;
                }
            }
        }
        else if (i == 2)
        {
            _targetG = 1;
            i = Random.Range(0, 2);
            if (i == 1)
            {
                i = Random.Range(0, 2);
                if (i == 1)
                {
                    _targetR = 1;
                }
                else
                {
                    _targetB = 1;
                }
            }
        }
        else
        {
            _targetB = 1;
            i = Random.Range(0, 2);
            if (i == 1)
            {
                i = Random.Range(0, 2);
                if (i == 1)
                {
                    _targetR = 1;
                }
                else
                {
                    _targetG = 1;
                }
            }
        }


    }

    private void UpdateColor()
    {
        if (_r < _targetR)
        {
            _r += Time.deltaTime;
            _r = Mathf.Clamp(_r, 0, _targetR);
        }
        else
        {
            _r -= Time.deltaTime;
            _r = Mathf.Clamp(_r, _targetR, 1);
        }


        if (_g < _targetG)
        {
            _g += Time.deltaTime;
            _g = Mathf.Clamp(_g, 0, _targetG);
        }
        else
        {
            _g -= Time.deltaTime;
            _g = Mathf.Clamp(_g, _targetG, 1);
        }


        if (_b < _targetB)
        {
            _b += Time.deltaTime;
            _b = Mathf.Clamp(_b, 0, _targetB);
        }
        else
        {
            _b -= Time.deltaTime;
            _b = Mathf.Clamp(_b, _targetB, 1);
        }

        _img.color = new Color(_r, _g, _b, .5f);
    }
}
