using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioSource _playerDead;
    [SerializeField] private AudioSource _playerHurt;
    [SerializeField] private AudioSource _enemyHurt;

    [SerializeField] private Texture2D _cursorTexture;
    
    private static SoundManager _instance;

    public static SoundManager Instance { get => _instance; set => _instance = value; }

    private void Awake()
    {
        if (SoundManager.Instance == null)
        {
            SoundManager.Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        _playerDead.minDistance = 5000;
        _playerHurt.minDistance = 5000;
        _enemyHurt.minDistance = 5000;
        Cursor.SetCursor(_cursorTexture, new Vector2(_cursorTexture.width / 2, _cursorTexture.height / 2), CursorMode.Auto);
    }

    public void PlayerDead()
    {
        _playerDead.Play();
    }
    public void PlayerHurt()
    {
        _playerHurt.Play();
    }
    public void EnemyHurt()
    {
        _enemyHurt.Play();
    }
}
