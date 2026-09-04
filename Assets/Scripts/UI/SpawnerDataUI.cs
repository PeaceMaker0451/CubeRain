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

    private void OnEnable()
    {
        _spawner.ParticleCreated += UpdateText;
        _spawner.ParticleSpawned += UpdateText;
        
        UpdateText();
    }

    private void OnDisable()
    {
        _spawner.ParticleCreated -= UpdateText;
        _spawner.ParticleSpawned -= UpdateText;
    }

    private void UpdateText()
    {
        _text.text = $"Общее количество объектов: {_spawner.TotalObjects}" +
            $"\nВсего было заспавнено: {_spawner.TotalSpawned}" +
            $"\nКоличество свободных объектов: {_spawner.FreeObjects}" +
            $"\nКоличество активных объектов: {_spawner.TotalObjects - _spawner.FreeObjects}";
    }
}