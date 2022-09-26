using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(PoolBabySharks))]
public class PoolBabySharksMovement : MonoBehaviour
{
    [SerializeField] private Transform _targetOfView;
    [SerializeField] private PlayerShark _playerShark;
    [SerializeField] private float _amplitudeDive;
    [SerializeField] private float _durationAnchorToPointY;
    [SerializeField] private float _speedRotation = 1f;
    [SerializeField] private float _maxAngleRotation = 10f;
    [SerializeField] private float _minAngleRotation = -10f;

    private bool _canDive;
    private Vector3 _startDirection;
    private float _currentAngle;

    private void OnDisable()
    {
        _playerShark.PlayerSharkTrigger.HasEatenPeople -= Dive;
    }

    private void Start()
    {
        _playerShark.PlayerSharkTrigger.HasEatenPeople += Dive;
        _canDive = true;
        _startDirection = _targetOfView.position - transform.position;
        _currentAngle = transform.eulerAngles.y;
    }

    private void Update()
    {
        Vector3 direction = _targetOfView.position - transform.position;
        float angleRotation = Vector3.SignedAngle(_startDirection, direction, Vector3.up);
        float angleClamp = Mathf.Clamp(angleRotation, _minAngleRotation, _maxAngleRotation);
        _currentAngle = Mathf.Lerp(_currentAngle, angleClamp, _speedRotation * Time.deltaTime);
        transform.rotation = Quaternion.Euler(transform.eulerAngles.x, _currentAngle, transform.eulerAngles.z);
    }

    private void Dive()
    {
        if (_canDive)
        {
            int loops = 2;
            _canDive = false;
            transform.DOLocalMoveY(_amplitudeDive, _durationAnchorToPointY)
                .SetLoops(loops, LoopType.Yoyo).OnComplete(() => _canDive = true);
        }
    }
}
