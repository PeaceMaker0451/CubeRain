using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class SpawnerDataUI : MonoBehaviour
{
    [SerializeField] private BaseSpawner _spawner;

    private Text _text;

    private void Awake()
    {
        _text = GetComponent<Text>();
    }

    private void Update()
    {
        _text.text = $"Текущее количество объектов: {_spawner.TotalObjects}" +
            $"\nВсего было заспавнено: {_spawner.TotalSpawned}" +
            $"\nКоличество свободных объектов: {_spawner.FreeObjects}" +
            $"\nКоличество активных объектов: {_spawner.TotalObjects - _spawner.FreeObjects}";
    }
}