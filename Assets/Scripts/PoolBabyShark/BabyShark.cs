using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;

public class BabyShark : MonoBehaviour
{
    [SerializeField] private BabySharkScriptableSkillData _babySharkSkillData;

    private Transform _placeTransform;

    public event UnityAction HasSailed;

    private void Start()
    {
        HasSailed += StopSwimAnimation;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Shark>(out Shark shark))
        {
            shark.PoolBabySharks.AddInPool(this);           
        }
    }

    private void StopSwimAnimation()
    {
        transform.forward = _placeTransform.forward;
    }

    public void Swim(Transform placeTransform)
    {
        print("Swim");
        _placeTransform = placeTransform;
        transform.DOLocalMove(placeTransform.localPosition, _babySharkSkillData.SpeedOfSwiming).OnComplete(()=> HasSailed?.Invoke());
    }
}
