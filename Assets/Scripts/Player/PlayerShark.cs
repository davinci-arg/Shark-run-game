using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerShark : Player
{
    [SerializeField] private PoolBabySharks _poolBabySharks;
    [SerializeField] private Shark _mainShark;
    [SerializeField] private Shark[] _templatesSharks;
    [SerializeField] private Transform _targetForBabyShark;

    private Queue<Shark> _sharks;

    public IReadOnlyCollection<Shark> TemplatesSharks => _templatesSharks;
    public PoolBabySharks PoolBabySharks => _poolBabySharks;
    public Shark MainShark => _mainShark;
    public Transform TargetForBabyShark => _targetForBabyShark;

    private void Start()
    {     
        InitializeQueueOfSharks();
    }

    private void InitializeQueueOfSharks()
    {
        if (_templatesSharks.Length > 0 && _mainShark != null)
        {
            _sharks = new Queue<Shark>();

            foreach (Shark shark in _templatesSharks)
            {
                Shark currentShark = Instantiate(shark, transform.position, Quaternion.identity, transform);
                currentShark.gameObject.SetActive(false);
                _sharks.Enqueue(currentShark);
            }
        }
    }

    public void ChangeShark()
    {
        if (_sharks.TryDequeue(out Shark currentShark))
        {
            _mainShark.gameObject.SetActive(false);
            currentShark.transform.rotation = _mainShark.transform.rotation;
            _mainShark = currentShark;
            currentShark.gameObject.SetActive(true);    
        }
    }
}
