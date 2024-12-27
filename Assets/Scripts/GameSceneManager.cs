using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneManager : MonoBehaviour
{
    
    [SerializeField]private PlayerHealthComponent playerHealthComponent;
    // Start is called before the first frame update
    void Start()
    {
        playerHealthComponent.OnDeath += OnDeath;
    }

    private void OnDeath()
    {
        SceneConnection.LoadPlaneScene();
    }
    
}
