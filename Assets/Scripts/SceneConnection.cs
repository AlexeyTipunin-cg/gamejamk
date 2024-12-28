using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneConnection : MonoBehaviour
{
    private static WeaponConfig[]  _weapons;
    private static EngineConfig _engineConfig;
    private static BodyConfig _bodyConfig;

    public static EngineConfig engineConfig => _engineConfig;
    public static BodyConfig bodyConfig => _bodyConfig;

    private static SceneConnection instance;
    
    public static WeaponConfig[]  weapons => _weapons;
    public static EngineConfig engineConfigs => _engineConfig;
    
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

    public static void LoadGameScene(EngineConfig engineConfig, BodyConfig bodyConfig, WeaponConfig[]  weapons)
    {
        _engineConfig = engineConfig;
        _bodyConfig = bodyConfig;
        _weapons = weapons;
        
        SceneManager.LoadScene("MainScene");
    }
    
    public static void LoadPlaneScene()
    {
        SceneManager.LoadScene("CreatePlane");
    }
}
