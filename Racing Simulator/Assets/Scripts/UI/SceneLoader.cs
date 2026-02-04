using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void LoadSceneByName(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
    public void QuitApplication()
    {
#if UNITY_EDITOR
        // В редакторе Unity: останавливаем режим Play
        UnityEditor.EditorApplication.isPlaying = false;
#else
        // В сборке: закрываем приложение
        Application.Quit();
#endif
    }
}