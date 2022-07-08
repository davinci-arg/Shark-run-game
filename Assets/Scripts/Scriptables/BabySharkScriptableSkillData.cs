using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Skill Settings")]
public class BabySharkScriptableSkillData : ScriptableObject
{
    [Header("Skills:")]
    [SerializeField] private float _speedOfSwimming;

    public float SpeedOfSwiming => _speedOfSwimming;
}
