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
    [SerializeField] private ParticleSystem _effectOfHappiness;

    private bool _canRotating;
    private ParticleSystem _happiness;

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
        _canRotating = true;
        _happiness = Instantiate(_effectOfHappiness, transform.position, Quaternion.identity, transform);
    }

    private void Rotate()
    {
        if (_canRotating && UIManager.Instance.UIMainScore.CurrentScores % _numberOfScoresMultipleForRotationOfShark == 0)
        {
            StartCoroutine(StartRotating());
        }
        if (UIManager.Instance.UIMainScore.CurrentScores % _numberOfScoresMultipleForChangeOfShark == 0)
        {
            _happiness.Play();
            PlayerShark.ChangeShark();
        }
    }

    private IEnumerator StartRotating()
    {
        _canRotating = false;
        Vector3 startRotation = Vector3.zero;
        Vector3 endRotation = new Vector3(0f, 0f, 360f);
        float startTime = Time.time;
        float timeComplete = 0f;

        while (timeComplete < _rotationTime)
        {
            timeComplete = (Time.time - startTime) / _rotationTime;
            Vector3 rotation = Vector3.Slerp(startRotation, endRotation, timeComplete);
            PlayerShark.MainShark.transform.eulerAngles = rotation;
            yield return null;
        }

        _canRotating = true;
    }
    
}
