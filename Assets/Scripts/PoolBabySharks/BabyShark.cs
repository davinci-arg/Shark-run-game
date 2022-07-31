using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;

[RequireComponent(typeof(BoxCollider))]
public class BabyShark : MonoBehaviour
{
    [SerializeField] private BabySharkScriptableSkillData _babySharkSkillData;

    private Transform _placeTransform;
    private BoxCollider _boxCollider;

    public event UnityAction HasSailed;

    private void Start()
    {
        _boxCollider = GetComponent<BoxCollider>();
        HasSailed += StopSwimAnimation;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<PlayerShark>(out PlayerShark playerShark))
        {
            _boxCollider.enabled = false;
            playerShark.PoolBabySharks.AddInPool(this);           
        }
    }

    private void StopSwimAnimation()
    {
        print("Eazse");
       // transform.forward = _placeTransform.forward;
    }

   ///private void Swim(PlayerShark playerShark)
   ///{
   ///    transform.DOLocalMove(playerShark.transform.localPosition, _babySharkSkillData.SpeedOfSwiming)
   ///        .OnComplete(() => playerShark.PoolBabySharks.AddInPool(this));
   ///}
   ///
   ///public void Swim(Transform placeTransform)
   ///{
   ///    print("BabyShark");
   ///    _placeTransform = placeTransform;
   ///    transform.DOLocalMove(placeTransform.localPosition, _babySharkSkillData.SpeedOfSwiming).OnComplete(()=> HasSailed?.Invoke());
   ///}
}
