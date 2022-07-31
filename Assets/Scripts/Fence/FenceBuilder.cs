using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

[RequireComponent(typeof(PathCreator))]

public class FenceBuilder : MonoBehaviour
{
    [SerializeField] private PathCreator _roadSpline;
    [SerializeField] private Fence _templateFence;
    [SerializeField] private float _widthBetweenFence;

    private List<Fence> _fences;
    private List<Vector3> _fencePositions;

    [Header("Draw fence")]
    [SerializeField] [Range(0.01f, 1f)] private float _stepFence;
    [SerializeField] [Range(0.1f, 10f)] private float _radius;


    private void Start()
    {
        PlaceFence();
    }

    private void OnDrawGizmos()
    {
        FindPositionForFence();

        if (_fencePositions.Capacity > 0 && _roadSpline != null)
        {          
            Gizmos.color = Color.magenta;

            foreach (Vector3 fencePositions in _fencePositions)
            {
                Gizmos.DrawSphere(fencePositions, _radius);
            }
        }
    }

    private void FindPositionForFence()
    {
        if (_stepFence > 0f && _stepFence < 1f)
        {
            _fencePositions = new List<Vector3>();

            for (float i = 0; i < 1f; i += _stepFence)
            {
                Vector3 centerOfRoad = _roadSpline.path.GetPointAtTime(i);
                Vector3 rightFaence = centerOfRoad + new Vector3(_widthBetweenFence, 0f, 0f);
                Vector3 leftFaence = centerOfRoad - new Vector3(_widthBetweenFence, 0f, 0f);
                _fencePositions.Add(rightFaence);
                _fencePositions.Add(leftFaence);
            }
        }
    }

    private void PlaceFence() 
    {
        FindPositionForFence();

        if (_fencePositions.Capacity > 0)
        {
            _fences = new List<Fence>();

            foreach (Vector3 fencePosition in _fencePositions)
            {
                Fence fence = Instantiate(_templateFence, fencePosition, Quaternion.identity, transform);
                _fences.Add(fence);
            }
        }     
    }
}
