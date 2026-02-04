using UnityEngine;

public class EscapeMenu : MonoBehaviour
{
    [SerializeField] GameObject _canvas;  // Назначьте канвас в Инспекторе!

    private void Awake()
    {
        // Проверяем, что канвас назначен
        if (_canvas == null)
        {
            Debug.LogError("Канвас не назначен в инспекторе!");
            return;
        }

        // Изначально скрываем канвас (если нужно)
        _canvas.SetActive(false);
    }

    private void Update()
    {
        // Обрабатываем нажатие Escape только при нажатии
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ToggleCanvas();
        }
    }

    private void ToggleCanvas()
    {
        // Переключаем видимость канваса
        _canvas.SetActive(!_canvas.activeSelf);

        // Дополнительно: можно остановить/запустить время
        Time.timeScale = _canvas.activeSelf ? 0f : 1f;
    }
}