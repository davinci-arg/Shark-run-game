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
    [SerializeField] private Transform _anchorOfPool;
    [SerializeField] private float _maxAngleRotationY = 10f;
    [SerializeField] private float _minAngleRotationY = -10f;
    
    private Vector3 _nextPosition;
    private float _distanceTravelled;
    private float _time;
    private float _yVelocity = 0.0f;
    //private SharkPlayer _sharkPlayer;

    private void Start()
    {
        _time = 0f;
        //_sharkPlayer = GetComponent<SharkPlayer>();
        _distanceTravelled = _roadSpline.path.GetClosestTimeOnPath(transform.position);
        transform.position = _roadSpline.path.GetPointAtTime(_distanceTravelled);
        _nextPosition = transform.position;
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
        //Vector3 direction = _nextPosition - transform.position;
        Vector3 direction = _nextPosition - transform.position;

        //_t += _sensitivitySwerve * Time.deltaTime;
        //_anchorOfPool.forward = Vector3.Lerp(_anchorOfPool.forward, directionSwerve, _sensitivitySwerve * Time.deltaTime);
        


        if (Input.GetMouseButton(0))
        {          
            float offsetSwerve = Input.GetAxis("Mouse X");
            float delta = nextPosition.z - transform.position.z;
            float currentPositionX = 0f;

            //Debug.Log($"TimeBefor:{_time}");
            //Debug.Log($"BeforPossition:{_nextPosition.x}");
            

            if (offsetSwerve > 0)
            {
                currentPositionX = _nextPosition.x + delta;
                _time += _sensitivitySwerve * Time.deltaTime;
                //_nextPosition.x += Mathf.MoveTowards(_nextPosition.x, _nextPosition.x + delta, Time.delta);
                _nextPosition.x = Mathf.MoveTowards(_nextPosition.x, currentPositionX, _time);
                //_nextPosition.x += delta;
                Vector3 newDir = _nextPosition - transform.position;
                //direction = Vector3.RotateTowards(PlayerShark.MainShark.transform.forward, newDir, _time, 0f);
                direction = newDir;
            }

            else if (offsetSwerve < 0)
            {
                currentPositionX = _nextPosition.x - delta;
                _time += _sensitivitySwerve * Time.deltaTime;
                _nextPosition.x = Mathf.MoveTowards(_nextPosition.x, currentPositionX, _time);
                //_nextPosition.x -= delta;
                Vector3 newDir = _nextPosition - transform.position;
                //direction = Vector3.RotateTowards(PlayerShark.MainShark.transform.forward, newDir, _time, 0f);
                direction = newDir;
            }

            else
            {
                _time = 0f;
            }
            //Debug.Log($"TimeAfter:{_time}");
            //Debug.Log($"NextPosition:{currentPositionX}");
            //Debug.Log($"CurrentPosition:{_nextPosition.x}");
        }

        Vector3 newDirection = Vector3.RotateTowards(PlayerShark.MainShark.transform.forward, direction, _sensitivitySwerve * Time.deltaTime, 0f);
        Quaternion quaternion = Quaternion.LookRotation(newDirection);
        Vector3 eluarRotation = quaternion.eulerAngles;
        eluarRotation.y = ClampAngle(eluarRotation.y);
        PlayerShark.MainShark.transform.rotation = Quaternion.Euler(eluarRotation);
        transform.position = _nextPosition;
    }
    
    private float ClampAngle(float angle)
    {
        if (angle > 180f)
        {
            if (angle - 360f < _minAngleRotationY)
            {
                return _minAngleRotationY;
            }

            return angle - 360f;
        }

        if (angle > _maxAngleRotationY)
        {
            return _maxAngleRotationY;
        }
   
        return angle;
    }
}
