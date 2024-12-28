using TMPro;
using UnityEngine;

public class HUDController : MonoBehaviour
{
    [SerializeField] private Transform _playerHealthProgressBar;
    [SerializeField] private PlayerHealthComponent _playerHealth;
    [SerializeField] private TMP_Text _scoreText;
    [SerializeField] private EndGamePopup _endgamePopup;
    private float _maxHealth;

    private void Awake()
    {
        _maxHealth = SceneConnection.bodyConfig.health;
        SetProgressBar(_maxHealth);

        _playerHealth.OnHealthChanged += SetProgressBar;
        _playerHealth.OnDeath += ShowEndGameWindow;
        
        _scoreText.text = "0";

        ScoreController.OnScoreUpdate += UpdateScore;
    }

    private void ShowEndGameWindow()
    {
        _endgamePopup.gameObject.SetActive(true);
        _endgamePopup.SetScore(_score);
    }

    private int _score;
    private void UpdateScore(int newScore)
    {
        _score = newScore;
        _scoreText.text = newScore.ToString();
    }

    private void SetProgressBar(float health)
    {
        var scale = _playerHealthProgressBar.localScale;
        scale.x = Mathf.Max(health / _maxHealth, 0);
        _playerHealthProgressBar.localScale = scale;
    }
}