using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalManager : MonoBehaviour
{
    #region Instance
    static private GlobalManager instance;
    public static GlobalManager Instance { get => instance; set => instance = value; }
    #endregion Instance

    private List<PlayerInput> _players = new List<PlayerInput>();




    public List<PlayerInput> Players { get => _players; set => _players = value; }


    void Awake()
    {
        TrySetInstance();
    }

    private void TrySetInstance()
    {
        if (GlobalManager.Instance == null)
        {
            GlobalManager.Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
