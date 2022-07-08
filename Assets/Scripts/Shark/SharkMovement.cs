using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class SharkMovement : MonoBehaviour
{
    [SerializeField] private PathCreator _path;
    [SerializeField] private float _speedMovement;
    [SerializeField] private float _sensitivitySwerve;
    [SerializeField] private float _minAngleSwerve;
    [SerializeField] private float _maxAngleSwerve;

    private float _distanceTravelled;
    private Vector3 _currentPosition;
    private float _timeSwerve;

    private void Start()
    {
        _timeSwerve = 0f;
        _distanceTravelled = 0f;
        transform.position = _path.path.GetPointAtTime(_distanceTravelled);
        _currentPosition = transform.position;
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        _distanceTravelled += _speedMovement * Time.deltaTime;
        Vector3 linePosition = _path.path.GetPointAtTime(_distanceTravelled, EndOfPathInstruction.Loop);

        if (Input.GetMouseButton(0))
        {
            Swerve();
        }
        else
        {
            _timeSwerve = default;
            transform.eulerAngles = Vector3.zero;
        }

        _currentPosition.z = linePosition.z;
        transform.position = _currentPosition;
    }

    private void Swerve()
    {
        float deltaSwerve = Input.GetAxis("Mouse X") * _sensitivitySwerve;
        _currentPosition.x += deltaSwerve * Time.deltaTime;
        Vector3 rotation = transform.eulerAngles;
        rotation.y -= deltaSwerve;

        if (deltaSwerve > 0)
        {
            Quaternion toRotation = Quaternion.Euler(new Vector3(0f, _maxAngleSwerve, 0f));
            transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, _timeSwerve);
            _timeSwerve += Time.deltaTime;
        }

        else if (deltaSwerve < 0)
        {
            Quaternion toRotation = Quaternion.Euler(new Vector3(0f, _minAngleSwerve, 0f));
            transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, _timeSwerve);
            _timeSwerve += Time.deltaTime;
        }
    }
}
