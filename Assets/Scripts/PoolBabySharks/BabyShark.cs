using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(BoxCollider))]
public class BabyShark : MonoBehaviour
{
    [SerializeField] private BabySharkScriptableSkillData _babySharkSkillData;

    private BoxCollider _boxCollider;
    private bool _canLookAtShark;
    private PlayerShark _playerShark;

    public event UnityAction HasSailed;

    private void OnEnable()
    {
        HasSailed += StopLookAtTarget;
    }

    private void OnDisable()
    {
        HasSailed -= StopLookAtTarget;
    }

    private void Start()
    {
        _boxCollider = GetComponent<BoxCollider>();
        _canLookAtShark = false;
    }

    private void Update()
    {
        if (_canLookAtShark)
        {
            Vector3 directionFollow = _playerShark.TargetForBabyShark.position - transform.position;
            transform.forward = Vector3.MoveTowards(transform.forward, directionFollow, _babySharkSkillData.SensitivityFollow * Time.deltaTime);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<PlayerShark>(out PlayerShark playerShark))
        {
            _boxCollider.enabled = false;
            playerShark.PoolBabySharks.AddInPool(this);
            _playerShark = playerShark;
            _canLookAtShark = true;
        }
    }

    public void StopLookAtTarget() => _canLookAtShark = false;

}
