using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Player : MonoBehaviour
{
    public PlayerShark PlayerShark { get; private set; }
    public PlayerSharkTrigger PlayerSharkTrigger { get; private set; }
    public PlayerSharkAnimation PlayerSharkAnimation { get; private set; }
    public PlayerSharkMovement PlayerSharkMovement { get; private set; }


    protected virtual void Awake()
    {
        PlayerShark = GetComponent<PlayerShark>();
        PlayerSharkTrigger = GetComponent<PlayerSharkTrigger>();
        PlayerSharkAnimation = GetComponent<PlayerSharkAnimation>();
        PlayerSharkMovement = GetComponent<PlayerSharkMovement>();
    }
}
