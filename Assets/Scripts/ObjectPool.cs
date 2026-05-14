using System;
using System.Collections.Generic;

public class ObjectPool<T> where T : UnityEngine.Object
{
    public event Action<T> ObjectCreated;
    
    private T _prefab;
    private Stack<T> _freeObjects;

    public ObjectPool(T prefab)
    {
        _prefab = prefab;
        _freeObjects = new Stack<T>();
    }

    public T Get()
    {
        if (_freeObjects.Count == 0)
            Create();

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
        ObjectCreated?.Invoke( newObject );
    }
}
