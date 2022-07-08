using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PoolBabySharks : MonoBehaviour
{
    [SerializeField] private int _width;
    [SerializeField] private int _height;
    [SerializeField] private float _step;

    [Header("DrawGizmos")]
    [SerializeField] private float _radius;

    private List<Vector3> _positionsInPool;
    private List<PoolPlace> _poolPlaces;

    private void Start()
    {
        CreatePoolPlaces();
    }

    private void OnDrawGizmos()
    {
        AllocatePositionsInPool();
        Gizmos.color = Color.magenta;

        foreach (Vector3 point in _positionsInPool)
        {
            Gizmos.DrawSphere(point, _radius);
        }
    }

    private void CreatePoolPlaces() 
    {
        AllocatePositionsInPool();
        _poolPlaces = new List<PoolPlace>();

        foreach (Vector3 position in _positionsInPool)
        {
            GameObject gameObject = Instantiate(new GameObject(), position, Quaternion.identity, transform);
            gameObject.AddComponent<PoolPlace>();
            _poolPlaces.Add(gameObject.GetComponent<PoolPlace>());
        }        
    }

    private void AllocatePositionsInPool()
    {
        _positionsInPool = new List<Vector3>();
        Vector3 currentPossition = transform.position;

        for (int i = 0; i < _height; i++)
        {
            _positionsInPool.Add(currentPossition);

            for (int j = 0; j < _width - 1; j++)
            {
                currentPossition = new Vector3(currentPossition.x + _step, currentPossition.y, currentPossition.z);
                _positionsInPool.Add(currentPossition);
            }

            currentPossition = new Vector3(transform.position.x, currentPossition.y, currentPossition.z - _step);
        }
    }

    public void AddInPool(BabyShark babyShark)
    {
        var placePool = _poolPlaces.FirstOrDefault(place => place.IsEmpty == true);

        if (placePool != null)
        {
            babyShark.transform.SetParent(transform);
            placePool.AddBabyShark(babyShark);
            babyShark.Swim(placePool.transform);
        }
    }

}
