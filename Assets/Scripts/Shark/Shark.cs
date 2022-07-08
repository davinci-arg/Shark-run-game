using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shark : MonoBehaviour
{
    [SerializeField] private PoolBabySharks _poolBabySharks;

    public PoolBabySharks PoolBabySharks => _poolBabySharks;

}
