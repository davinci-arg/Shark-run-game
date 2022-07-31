using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using DG.Tweening;
using System;

public class PoolBabySharks : MonoBehaviour
{
    [SerializeField] private int _width;
    [SerializeField] private int _height;
    [SerializeField] private float _widthStep;
    [SerializeField] private float _heightStep;
    [SerializeField] private float _durationMovement;
    [SerializeField] private Transform _anchorOfPool;
    [SerializeField] private PlayerShark _playerShark;

    [Header("DrawGizmos")]
    [SerializeField] private float _radius;

    private List<Vector3> _positionsInPool;
    private List<BabySharkPlace> _poolPlaces;

    private Action _moved;

    private void Start()
    {
        CreatePoolPlaces();
    }

    private void Update()
    {
        _anchorOfPool.transform.forward = _playerShark.MainShark.transform.forward;

        
        //transform.LookAt(_player.MainShark.transform);
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
        _poolPlaces = new List<BabySharkPlace>();

        foreach (Vector3 position in _positionsInPool)
        {
            GameObject gameObject = Instantiate(new GameObject(), position, Quaternion.identity, transform);
            gameObject.AddComponent<BabySharkPlace>();
            _poolPlaces.Add(gameObject.GetComponent<BabySharkPlace>());
        }        
    }

    private void AllocatePositionsInPool()
    {
        _positionsInPool = new List<Vector3>();
        Vector3 firstPossition = transform.position;

        for (int i = 0; i < _height; i++)
        {
            _positionsInPool.Add(firstPossition);
            Vector3 secondPosition = firstPossition + new Vector3(_widthStep, 0f, 0f);
            Vector3 thirdPosition = firstPossition - new Vector3(_widthStep, 0f, 0f);
            _positionsInPool.Add(secondPosition);
            _positionsInPool.Add(thirdPosition);
            firstPossition -= new Vector3(0f, 0f, _heightStep);
        }
    }

    public void AddInPool(BabyShark babyShark)
    {
        var poolPlace = _poolPlaces.FirstOrDefault(place => place.IsEmpty == true);

        if (poolPlace != null)
        {
            babyShark.transform.SetParent(transform);
            babyShark.transform.DOLocalMove(poolPlace.transform.localPosition, _durationMovement)
            .OnComplete(() => SetDirectionInPool());
            poolPlace.AddBabyShark(babyShark);
        }

        void SetDirectionInPool()
        {
            babyShark.transform.forward = _playerShark.MainShark.transform.position - poolPlace.transform.position;
        }
    }

    public void GetFromPool(Vector3[] newPositions)
    {
        var babysharks = new List<BabyShark>();

        for (int i = 0; i < newPositions.Length; i++)
        {
            var poolPlace = _poolPlaces.LastOrDefault(place => place.IsEmpty == false);
            
            if (poolPlace != null)
            {
                BabyShark babyShark = poolPlace.GetBabyShark();
                babyShark.transform.parent = null;
                babyShark.transform.DOMove(newPositions[i], _durationMovement);
            }
        }
    }

    //private void MoveBabyShark(BabyShark babyShark, Vector3 toPosition)
    //{
    //    babyShark.transform.DOLocalMove(poolPlace.transform.localPosition, _speedMovementToPool)
    //        .OnComplete(() => SetDirectionInPool());
    //}
}
