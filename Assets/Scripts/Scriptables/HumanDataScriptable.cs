using PathCreation;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Human Data")]
public class HumanDataScriptable : ScriptableObject
{
    [SerializeField] private float _safeDistance;
    [SerializeField] private float _speedOfSwim;
    [SerializeField] private ParticleSystem _bloodEffect;
    [SerializeField] private ParticleSystem _swimEffect;
    [SerializeField] [Range(0f, 90f)] private float _angleSwimRange;

    public float SafeDistance => _safeDistance;
    public float SpeedOfSwim => _speedOfSwim;
    public ParticleSystem BloodEffect => _bloodEffect;
    public ParticleSystem SwimEffect => _swimEffect;

    public float AngleSwim => _angleSwimRange;
}
