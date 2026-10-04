using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Saved : MonoBehaviour
{
    private static Saved _instance;
    public static Saved Instance { get => _instance; set => _instance = value; }

    private EInputMode _inputMode = EInputMode.Keyboard;


    public EInputMode InputMode { get => _inputMode; set => _inputMode = value; }

    private void Awake()
    {
        if (Saved.Instance == null)
        {
            Saved.Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
