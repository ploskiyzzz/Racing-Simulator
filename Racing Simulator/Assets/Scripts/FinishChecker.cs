using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FinishChecker : MonoBehaviour
{

    public GameObject exitMenuButton; // Кнопка «Выход в меню»

    void Start()
    {
        // Скрываем кнопку «Выход в меню» в начале игры
        exitMenuButton.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Игрок достиг финиша!");

            // Останавливаем движение машины
            other.GetComponent<Rigidbody>().linearVelocity = Vector3.zero;

            // Останавливаем игровой процесс
            Time.timeScale = 0;

            // Показываем панель меню и кнопки
         
            exitMenuButton.SetActive(true); // Показываем кнопку «Выход в меню»
        }
    }
}
