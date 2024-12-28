using System;
using TMPro;
using UnityEngine;

using UnityEngine.UI;

public class EndGamePopup : MonoBehaviour
{
    
    [SerializeField]private Button endGameBtn;
    [SerializeField] private TMP_Text scoreText;
    public static event Action OnEndGame;
    // Start is called before the first frame update
    void Start()
    {
        endGameBtn.onClick.AddListener(FinishGame);
    }

    private void FinishGame()
    {
        OnEndGame?.Invoke();
    }

    public void SetScore(int score)
    {
        scoreText.text = "Набранные очки: " + score;
    }
    
}
