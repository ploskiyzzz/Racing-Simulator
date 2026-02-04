using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class CheckpointSaver : MonoBehaviour
{
    [Header("Настройки зоны")]
    [SerializeField, Tooltip("Размер зоны чекпоинта")]
    private Vector3 triggerSize = new Vector3(3f, 2f, 3f);

    [SerializeField, Tooltip("Смещение центра зоны от позиции объекта")]
    private Vector3 centerOffset = Vector3.zero;

    [Space]
    [SerializeField, Tooltip("Тег игрока для проверки")]
    private string playerTag = "Player";

    private BoxCollider boxCollider;

    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider>();

        // Настраиваем коллайдер
        boxCollider.size = triggerSize;
        boxCollider.center = centerOffset;
        boxCollider.isTrigger = true; // Гарантируем, что это триггер

        Debug.Log($"[CheckpointSaver] Зона настроена: размер={triggerSize}, смещение={centerOffset}");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(playerTag))
        {
            SaveCheckpoint(other.transform);
        }
    }

    private void SaveCheckpoint(Transform player)
    {
        var data = new CheckpointData
        {
            Position = player.position,
            Rotation = player.rotation,
            Timestamp = System.DateTime.UtcNow.Ticks
        };

        string filePath = GetCheckpointFilePath(data.Timestamp);
        string json = JsonUtility.ToJson(data);
        System.IO.File.WriteAllText(filePath, json);

        Debug.Log($"✅ Чекпоинт сохранён: {filePath}\n" +
                 $"Позиция: {data.Position}\n" +
                 $"Вращение: {data.Rotation}");
    }

    private string GetCheckpointFilePath(long timestamp)
    {
        string folder = Application.persistentDataPath + "/Checkpoints";
        if (!System.IO.Directory.Exists(folder))
            System.IO.Directory.CreateDirectory(folder);

        return $"{folder}/cp_{timestamp}.json";
    }

    // Отрисовка зоны в редакторе
    private void OnDrawGizmos()
    {
        // Красный цвет с прозрачностью (альфа = 0.3)
        Gizmos.color = new Color(1f, 0f, 0f, 0.3f);

        // Учитываем трансформацию объекта (вращение, масштаб)
        Gizmos.matrix = transform.localToWorldMatrix;

        // Рисуем куб по настройкам
        Gizmos.DrawCube(centerOffset, triggerSize);

        // Контур для лучшей видимости (чистый красный)
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(centerOffset, triggerSize);
    }


    // Визуализация в игровом режиме (опционально)
    private void Update()
    {
#if UNITY_EDITOR
        // Для отладки: рисуем оси в игровом режиме
        Vector3 center = transform.position + transform.TransformVector(centerOffset);
        Debug.DrawLine(center, center + Vector3.right * 0.5f, Color.red);
        Debug.DrawLine(center, center + Vector3.up * 0.5f, Color.green);
        Debug.DrawLine(center, center + Vector3.forward * 0.5f, Color.blue);
#endif

    }
}