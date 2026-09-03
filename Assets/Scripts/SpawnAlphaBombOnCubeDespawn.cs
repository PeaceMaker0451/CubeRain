using UnityEngine;

[RequireComponent(typeof(AlphaBombSpawner), typeof(CubeSpawner))]
public class SpawnAlphaBombOnCubeDespawn : MonoBehaviour
{
    private CubeSpawner _cubeSpawner;
    private AlphaBombSpawner _bombSpawner;

    private void Awake()
    {
        _cubeSpawner = GetComponent<CubeSpawner>();
        _bombSpawner = GetComponent<AlphaBombSpawner>();
    }

    private void OnEnable()
    {
        _cubeSpawner.Despawned += OnDespawned;
    }

    private void OnDisable()
    {
        _cubeSpawner.Despawned -= OnDespawned;
    }

    private void OnDespawned(Cube cube)
    {
        _bombSpawner.Spawn(cube.transform.position);
    }
}