using UnityEngine;

public class HUDController : MonoBehaviour
{
    [SerializeField] private Transform _playerHealthProgressBar;
    [SerializeField] private PlayerHealthComponent _playerHealth;
    private float _maxHealth;

    private void Awake()
    {
        player = FindAnyObjectByType<PlayerInputController>();

        _maxHealth = player.playerConfig.healthValue;
        SetProgressBar(_maxHealth);

        _playerHealth.OnHealthChanged += SetProgressBar;
    }

    public PlayerInputController player { get; set; }

    private void SetProgressBar(float health)
    {
        var scale = _playerHealthProgressBar.localScale;
        scale.x = Mathf.Max(health / _maxHealth, 0);
        _playerHealthProgressBar.localScale = scale;
    }
}