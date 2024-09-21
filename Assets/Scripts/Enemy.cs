using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Mover _mover; 
    private Action<Enemy> _onDead;

    public void Initialize(Action<Enemy> onDead, Vector3 targetPosition)
    {
        _mover.MoveTo(targetPosition);
        _onDead = onDead;
    }

    private void OnCollisionExit(Collision collision)
    {
        _onDead?.Invoke(this);   
    }    
}
