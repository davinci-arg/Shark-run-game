using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(BoxCollider))]
public class Human : MonoBehaviour
{
    [SerializeField] private HumanDataScriptable _humanData;
    [SerializeField] private Animator _animator;
    [SerializeField] private ParticleSystem _swimEffect;

    private Vector3 _randomDirection;
    private bool _isSwimming;
    ParticleSystem _bloodEffect;

    private void Start()
    {
        _isSwimming = false;
        float randomAngle = Random.Range(-_humanData.AngleSwim, _humanData.AngleSwim);
        _randomDirection = Quaternion.AngleAxis(randomAngle, Vector3.up) * transform.forward;
        _swimEffect.gameObject.SetActive(false);
        _bloodEffect = Instantiate(_humanData.BloodEffect, transform.position, Quaternion.identity);
        _bloodEffect.gameObject.SetActive(false);
    }

    private void Update()
    {
        Watch();
    }

    private void Watch()
    {
        float sharkDistance = Vector3.Distance(GameManager.Instance.PlayerShark.transform.position, transform.position);

        if (sharkDistance < _humanData.SafeDistance)
        {
            Swim();
        }
        else
        {
            Stay();
        }
    }

    private void Swim()
    {
        PlayAnimation(HumanAnimation.Swimming);
        _swimEffect.gameObject.SetActive(true);
        transform.Translate(_randomDirection * _humanData.SpeedOfSwim * Time.deltaTime);
        _isSwimming = true;
    }

    private void Stay()
    {
        if (_isSwimming)
        {
            StopAnimation(HumanAnimation.Swimming);
            _swimEffect.gameObject.SetActive(false);
            gameObject.SetActive(false);
        }
    }

    private void PlayAnimation(string animation) => _animator.SetBool(animation, true);
    private void StopAnimation(string animation) => _animator.SetBool(animation, false);

    public void TakeDamage()
    {
        _bloodEffect.gameObject.transform.position = transform.position;
        _bloodEffect.gameObject.SetActive(true);
        _bloodEffect.Play();
        Destroy(gameObject);
    }
}
