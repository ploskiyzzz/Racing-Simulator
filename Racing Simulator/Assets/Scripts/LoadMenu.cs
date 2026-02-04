using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadMenu : MonoBehaviour
{
    public void LoadMenuScene()
    {
        Time.timeScale = 1; // Восстанавливаем скорость времени
        SceneManager.LoadScene("Menu"); // Замените "MenuScene" на имя вашей сцены меню
    }
}
