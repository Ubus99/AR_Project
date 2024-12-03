using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChangeManager : MonoBehaviour
{
    public void ChangeScene(string sceneName)
    {
        // Log to confirm the method is triggered
        Debug.Log($"Changing scene to: {sceneName}");

        // Load the specified scene
        SceneManager.LoadScene(sceneName);
    }
}
