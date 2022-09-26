using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

[RequireComponent(typeof(PlayerShark))]
public class PlayerSharkMovement : Player
{
    [SerializeField] private PathCreator _roadSpline;
    [SerializeField] private float _speedMovement;
    [SerializeField] private float _sensitivitySwerve;
    [SerializeField] private float _sensitivityRotationY = 1f;
    [SerializeField] private float _sensitivityOffsetTargetX = 1f;
    [SerializeField] private float _sensitivityTargetX = 1f;
    [SerializeField] private float _maxAngleRotationY = 10f;
    [SerializeField] private float _minAngleRotationY = -10f;
    [SerializeField] private float _maxAngleTarget = 10f;
    [SerializeField] private float _minAngleTarget = -10f;
    [SerializeField] private float _maxTargetOffsetX = 1f;
    [SerializeField] private float _minTargetOffsetX = -1f;

    private Vector3 _nextPosition;
    private float _distanceTravelled;
    private float _time;
    private bool _isStright;
    private Vector3 _targetLockalPosition;

    private void Start()
    {
        _time = 0f;
        _distanceTravelled = _roadSpline.path.GetClosestTimeOnPath(transform.position);
        transform.position = _roadSpline.path.GetPointAtTime(_distanceTravelled);
        _nextPosition = transform.position;
        _isStright = true;
        _targetLockalPosition = PlayerShark.TargetForBabyShark.localPosition;
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        _distanceTravelled += _speedMovement * Time.deltaTime;       
        Vector3 nextPosition = _roadSpline.path.GetPointAtTime(_distanceTravelled, EndOfPathInstruction.Loop);        
        _nextPosition.z = nextPosition.z;
        Vector3 direction = _nextPosition - PlayerShark.MainShark.transform.position;

        if (Input.GetMouseButton(0))
        {          
            float offsetSwerve = Input.GetAxis("Mouse X");
            float delta = nextPosition.z - transform.position.z;
            float currentPositionX = 0f;

            if (offsetSwerve > 0)
            {
                currentPositionX = _nextPosition.x + delta;
                _time += _sensitivitySwerve * Time.deltaTime;
                _nextPosition.x = Mathf.MoveTowards(_nextPosition.x, currentPositionX, _time);
                PlayerShark.TargetForBabyShark.localPosition = MoveTargetX(PlayerShark.TargetForBabyShark.localPosition.x + delta);
                Vector3 newDir = _nextPosition - transform.position;
                direction = newDir;
                _isStright = false;
            }

            else if (offsetSwerve < 0)
            {
                currentPositionX = _nextPosition.x - delta;
                _time += _sensitivitySwerve * Time.deltaTime;
                _nextPosition.x = Mathf.MoveTowards(_nextPosition.x, currentPositionX, _time);
                PlayerShark.TargetForBabyShark.localPosition = MoveTargetX(PlayerShark.TargetForBabyShark.localPosition.x - delta);
                Vector3 newDir = _nextPosition - transform.position;
                direction = newDir;
                _isStright = false;
            }

            else
            {
                _time = 0f;
                _isStright = true;
            }
        }
        else
        {
            _isStright = true;
        }

        if (_isStright)
        {
           Vector3 targetPosition = PlayerShark.TargetForBabyShark.localPosition;
           targetPosition.x = Mathf.MoveTowards(targetPosition.x, 0f, Time.deltaTime * _sensitivityTargetX);
           PlayerShark.TargetForBabyShark.localPosition = targetPosition;
        }

        Vector3 newDirection = Vector3.RotateTowards(PlayerShark.MainShark.transform.forward, direction, _sensitivityRotationY * Time.deltaTime, 0f);
        Quaternion quaternion = Quaternion.LookRotation(newDirection);
        Vector3 eluarRotation = quaternion.eulerAngles;
        eluarRotation.y = ClampAngle(eluarRotation.y);
        PlayerShark.MainShark.transform.rotation = Quaternion.Euler(eluarRotation);
        transform.position = _nextPosition;
    }
    
    private float ClampAngle(float currentAngle)
    {
        if (currentAngle > 180f)
        {
            if (currentAngle - 360f < _minAngleRotationY)
            {
                return _minAngleRotationY;
            }

            return currentAngle - 360f;
        }

        if (currentAngle > _maxAngleRotationY)
        {
            return _maxAngleRotationY;
        }
   
        return currentAngle;
    }

    private Vector3 MoveTargetX(float offsetX)
    {
        float currentOffsetX = Mathf.MoveTowards(PlayerShark.TargetForBabyShark.localPosition.x, Mathf.Clamp(offsetX, _minTargetOffsetX, _maxTargetOffsetX), Time.deltaTime * _sensitivityOffsetTargetX);
        Vector3 currentTargetPosition = PlayerShark.TargetForBabyShark.localPosition;
        currentTargetPosition = new Vector3(currentOffsetX, currentTargetPosition.y, currentTargetPosition.z);
        return currentTargetPosition;
    }
}
