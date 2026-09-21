using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class If : MonoBehaviour
{
    #region Instance
    private static If _instance;
    public static If Instance { get => _instance; set => _instance = value; }

    private void SetInstance()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    #endregion Instance

    private int _frames;

    private bool _ready;

    [SerializeField] private List<Image> _images = new List<Image>();

    [SerializeField] private Image _background;


    private void Awake()
    {
        SetInstance();
    }

    // Update is called once per frame
    void Update()
    {
        RemainLogic();
    }

    private void RemainLogic()
    {
        if (_frames > 0)
        {
            _frames -= 1;
        }
        else if (_ready)
        {
            Disappear();
        }
    }
    
    public void Appear(Transform other)
    {
        transform.position = other.position;
        transform.rotation = new Quaternion(transform.rotation.w, transform.rotation.x, transform.rotation.y, Random.Range(0f, 360f));
        for (int i = 0; i < _images.Count; i++)
        {
            _images[i].color = SetAlpha(Color.black, true);
        }
        _frames = 2;
        _background.color = SetAlpha(Color.white, true);//new Color(1, 1, 1, 1);
        _ready = true;
    }

    private void Disappear()
    {
        for (int i = 0; i < _images.Count; i++)
        {
            _images[i].color = SetAlpha(Color.black, false);
        }
        _background.color = SetAlpha(Color.white, false);
        _ready = false;
    }

    private Color SetAlpha(Color color, bool a)
    {
        if (a)
        {
            Color c = color;
            c.a = 1;
            return c;
        }
        else
        {
            Color c = color;
            c.a = 0;
            return c;
        }
    }
}
