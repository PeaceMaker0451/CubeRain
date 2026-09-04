using System;
using UnityEngine;

public class AlphaBombSpawner : Spawner<AlphaBomb>
{
    public void Spawn(Vector3 position)
    {
        var bomb = Spawn();
        bomb.gameObject.SetActive(true);
        bomb.transform.position = position;
        bomb.transform.rotation = Quaternion.identity;
        bomb.ResetBomb();
        bomb.Explode();
    }

    protected override void InitializeParticle(AlphaBomb particle, Action despawnAction)
    {
        particle.Initialize(despawnAction);
    }

    protected override void DespawnParticle(AlphaBomb particle)
    {
        particle.gameObject.SetActive(false);
    }
}
