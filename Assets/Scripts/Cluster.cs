using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClusterTank : BaseTank
{
    // Add properties or methods specific to ClusterTank later.

    protected override void Start()
    {
        base.Start();
        // Additional initialization specific to ClusterTank.
    }

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        base.OnTriggerEnter2D(other);
        // Additional logic specific to ClusterTank when entering a collider.
    }

    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);
        // Additional logic specific to ClusterTank when taking damage.
    }

    protected override void DestroyTank()
    {
        base.DestroyTank();
    }

    public override void Heal(int healAmount)
    {
        base.Heal(healAmount);
        // Additional logic specific to ClusterTank when healed.
    }

    protected override void HandleMovingState()
    {
        base.HandleMovingState();
    }

    public override void Refuel()
    {
        base.Refuel();
        // Additional logic specific to ClusterTank when refueling.
    }

    protected override void Update()
    {
        base.Update();
    }
}