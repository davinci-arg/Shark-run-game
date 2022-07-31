using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider), typeof(Rigidbody))]
public class PlayerSharkTrigger : Player
{
    public event UnityAction HasEatenPeople;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Human>(out Human human))
        {
            human.TakeDamage();
            HasEatenPeople?.Invoke();
        }
    }
}
