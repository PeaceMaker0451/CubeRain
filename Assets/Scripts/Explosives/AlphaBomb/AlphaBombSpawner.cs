using System;
using UnityEngine;

public class AlphaBombSpawner : Spawner<AlphaBomb>
{
    private void Start()
    {
        Initialize(InitializeParticle, OnDespawn);
    }

    public void Spawn(Vector3 position)
    {
        var bomb = Spawn();
        bomb.gameObject.SetActive(true);
        bomb.transform.position = position;
        bomb.transform.rotation = Quaternion.identity;
        bomb.ResetBomb();
        bomb.Explode();
    }

    private void InitializeParticle(AlphaBomb particle, Action despawnAction)
    {
        particle.Initialize(despawnAction);
    }

    private void OnDespawn(AlphaBomb particle)
    {
        particle.gameObject.SetActive(false);
    }
}
