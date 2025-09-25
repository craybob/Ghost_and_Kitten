
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneAdmin: MonoBehaviour
{
    
    public void loadNewLevel()
    {
        string activeSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(activeSceneName);
    }
}
