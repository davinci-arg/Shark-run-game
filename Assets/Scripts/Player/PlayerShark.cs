using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerShark : Player
{
    [SerializeField] private PoolBabySharks _poolBabySharks;
    [SerializeField] private Shark _mainShark;
    [SerializeField] private Shark[] _templatesSharks;
    
    //[SerializeField] private PlayerSharkTrigger _sharkPlayerTrigger;

    private Queue<Shark> _sharks;

    //public int EatenPeopleCounter { get; private set; }

    public IReadOnlyCollection<Shark> TemplatesSharks => _templatesSharks;
    public PoolBabySharks PoolBabySharks => _poolBabySharks;
    public Shark MainShark => _mainShark;

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
            _mainShark = currentShark;
            currentShark.gameObject.SetActive(true);
            
        }
    }
}
