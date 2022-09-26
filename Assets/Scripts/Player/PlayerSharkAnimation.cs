using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerSharkAnimation : Player
{
    [Header("Rotation settings:")]
    [SerializeField] private int _numberOfScoresMultipleForChangeOfShark;
    [SerializeField] private int _numberOfScoresMultipleForRotationOfShark;
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private float _rotationTime;
    [SerializeField] private float _timeWaitRespawn;
    [SerializeField] private ParticleSystem _effectOfHappiness;

    private bool _canRotation;
    private int _counterEatenPeople;
    private ParticleSystem _respawnEffect;
    private WaitForSeconds _respawnTime;

    private void OnEnable()
    {
        PlayerSharkTrigger.HasEatenPeople += Rotate;
    }

    private void OnDisable()
    {
        PlayerSharkTrigger.HasEatenPeople -= Rotate;
    }

    private void Start()
    {
        _counterEatenPeople = 0;
        _canRotation = true;
        _respawnTime = new WaitForSeconds(_timeWaitRespawn);
        _respawnEffect = Instantiate(_effectOfHappiness, transform.position, Quaternion.identity, transform);
    }

    private void Rotate()
    {
        _counterEatenPeople++;

        if (_canRotation && _counterEatenPeople % _numberOfScoresMultipleForRotationOfShark == 0)
        {
            StartCoroutine(StartRotating());
        }
        if (_counterEatenPeople % _numberOfScoresMultipleForChangeOfShark == 0)
        {
            StartCoroutine(Respawning());
        }
    }

    private IEnumerator StartRotating()
    {
        _canRotation = false;
        Vector3 startRotation = Vector3.zero;
        Vector3 endRotation = new Vector3(0f, 0f, 360f);
        float startTime = Time.time;
        float timeComplete = 0f;

        while (timeComplete < _rotationTime)
        {
            timeComplete = (Time.time - startTime) / _rotationTime;           
            Vector3 rotation = Vector3.Slerp(startRotation, endRotation, timeComplete);
            PlayerShark.MainShark.transform.eulerAngles += rotation;
            yield return null;
        }

        _canRotation = true;
    }
    
    private IEnumerator Respawning()
    {
        _respawnEffect.Play();
        yield return _respawnTime;      
        PlayerShark.ChangeShark();
    }
}
