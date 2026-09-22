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

    public void TrySetInstance()
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

    [SerializeField] private int _activeFrames;
    private int _frameCount;
    private bool _ready;
    [SerializeField] private List<Image> _images = new List<Image>();
    [SerializeField] private Image _background;

    #region Method
    private void Awake()
    {
        TrySetInstance();
    }

    // Update is called once per frame
    void Update()
    {
        RemainLogic();
    }

    private void RemainLogic()
    {
        if (_frameCount <= _activeFrames)
        {
            _frameCount++;
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
        _frameCount = 0;
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

    private Color SetAlpha(Color color, bool visible)
    {
        if (visible)
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
    #endregion Method
}
