using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneConnection : MonoBehaviour
{
    private static EngineConfig _engineConfig;
    private static BodyConfig _bodyConfig;

    public static EngineConfig engineConfig => _engineConfig;
    public static BodyConfig bodyConfig => _bodyConfig;

    private static SceneConnection instance;
    
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public static void LoadGameScene(EngineConfig engineConfig, BodyConfig bodyConfig)
    {
        _engineConfig = engineConfig;
        _bodyConfig = bodyConfig;
        
        SceneManager.LoadScene("MainScene");
    }
}
