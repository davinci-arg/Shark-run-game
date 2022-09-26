using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : PersistentSingleton<GameManager>
{
    [SerializeField] private PlayerShark _playerShark;
    
    private const int _frameRate = 30;

    public PlayerShark PlayerShark => _playerShark;

    protected override void Awake()
    {
        base.Awake();
        Application.targetFrameRate = _frameRate;
    }

    private void OnDestroy()
    {
        print("Destroy");
    }
}
