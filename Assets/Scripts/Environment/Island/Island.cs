using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Island : MonoBehaviour
{
    [SerializeField] private Transform [] _restPlaces;
    [SerializeField] private float _radiusDistance;

    private PlayerShark _playerShark;
    private bool _isEmpty;
    private Vector3[] _restPositions;


    private void Start()
    {
        _playerShark = GameManager.Instance.PlayerShark;
        _isEmpty = true;
        _restPositions = new Vector3[_restPlaces.Length];

        for (int i = 0; i < _restPlaces.Length; i++)
        {
            _restPositions[i] = _restPlaces[i].position;
        }
    }

    private void Update()
    {
        if (_isEmpty && Vector3.Distance(transform.position, _playerShark.transform.position) < _radiusDistance)
        {
            print("Closest");
            _isEmpty = false;
            _playerShark.PoolBabySharks.GetFromPool(_restPositions);
        }
    }
}
