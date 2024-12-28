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
        string text = "";
        if (ScoreController.hasHighScore)
        {
            if (ScoreController.isNewRecord)
            {
                text = $"Вы 1-ый по очкам: {score}. Поздравляем!!!\nПредыдущий результат: {ScoreController.oldRecordScore}";
            }
            else
            {
                text = $"Лучший результат: {ScoreController.oldRecordScore}\nВаш результат: {score}";
            }
        }
        else
        {
            text = "Ваши очки: " + score;
        }

        scoreText.text = text;
    }
    
}
