using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Skill Settings")]
public class BabySharkScriptableSkillData : ScriptableObject
{
    [Header("Skills:")]
    [SerializeField] private float _speedOfSwimming;
    [SerializeField] private float _sensitivityFollow;

    public float SpeedOfSwiming => _speedOfSwimming;
    public float SensitivityFollow => _sensitivityFollow;
}
