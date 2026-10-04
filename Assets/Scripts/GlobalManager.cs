using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalManager : MonoBehaviour
{
    #region Instance
    static private GlobalManager instance;
    public static GlobalManager Instance { get => instance; set => instance = value; }

    public void TrySetInstance()
    {
        if (GlobalManager.Instance == null)
        {
            GlobalManager.Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    #endregion Instance

    
    [SerializeField] private bool _justSet = true; //If in scene, change to false in inspector
    private List<PlayerInput> _allPlayers = new List<PlayerInput>();
    private List<PlayerInput> _activePlayers = new List<PlayerInput>();
    private Dictionary<PlayerInput, EInputMode> _playerModes = new Dictionary<PlayerInput, EInputMode>();

    public List<PlayerInput> AllPlayers { get => _allPlayers; set => _allPlayers = value; }
    public List<PlayerInput> ActivePlayers { get => _activePlayers; set => _activePlayers = value; }
    public Dictionary<PlayerInput, EInputMode> PlayerModes { get => _playerModes; set => _playerModes = value; }

    void Awake()
    {
        if (!_justSet) //Only allow GlobalMangers in scene to fight for instance
        {
            TrySetInstance();
        }
    }
}
