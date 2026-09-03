using System;
using System.Collections.Generic;

public class ObjectPool<T> where T : UnityEngine.Object
{
    private T _prefab;
    private Stack<T> _freeObjects;
    private List<T> _objects;

    public event Action<T> ObjectCreated;

    public int TotalObjects => _objects.Count;
    public int TotalSpawned { get; private set; }
    public int FreeObjects => _freeObjects.Count;

    public ObjectPool(T prefab)
    {
        _prefab = prefab;
        _freeObjects = new Stack<T>();
        _objects = new List<T>();
    }

    public T Get()
    {
        if (_freeObjects.Count == 0)
            Create();

        TotalSpawned++;
        return _freeObjects.Pop();
    }

    public void Release(T objectToRelease)
    {
        _freeObjects.Push(objectToRelease);
    }

    private void Create()
    {
        var newObject = UnityEngine.Object.Instantiate(_prefab);
        _freeObjects.Push(newObject);
        _objects.Add(newObject);
        ObjectCreated?.Invoke( newObject );
    }
}
