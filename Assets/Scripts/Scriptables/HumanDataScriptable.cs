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

    public float SafeDistance => _safeDistance;
    public float SpeedOfSwim => _speedOfSwim;
    public ParticleSystem BloodEffect => _bloodEffect;
}
