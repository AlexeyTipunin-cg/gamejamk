using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneManager : MonoBehaviour
{

    void Start()
    {
        EndGamePopup.OnEndGame += OnDeath;
    }

    private void OnDeath()
    {
        SceneConnection.LoadPlaneScene();
    }
    
}
