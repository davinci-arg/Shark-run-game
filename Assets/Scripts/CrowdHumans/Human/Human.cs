using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class Human : MonoBehaviour
{
    [SerializeField] private HumanDataScriptable _humanData;
    [SerializeField] private Animator _animator;

    private CrowdHumans _crowdHumans;

    private void Start()
    {
        _crowdHumans = GetComponentInParent<CrowdHumans>();
    }

    private void Update()
    {
        Watch();
    }

    private void Watch()
    {
        float sharkDistance = Vector3.Distance(_crowdHumans.Player.transform.position, transform.position);

        if (sharkDistance < _humanData.SafeDistance)
        {
            Swim();
        }
    }

    private void Swim()
    {
        //print("Alarm Swim everybody");
        PlayAnimation(HumanAnimation.Swimming);
        transform.Translate(transform.forward * _humanData.SpeedOfSwim * Time.deltaTime);
    }

    private void PlayAnimation(string animation) => _animator.SetBool(animation, true);

    public void TakeDamage()
    {
        //print("Do not eat me AAAA! Effect");
        ParticleSystem bloodEffect = Instantiate(_humanData.BloodEffect, transform.position, Quaternion.identity);
        bloodEffect.Play();
        Destroy(gameObject);
    }
}
