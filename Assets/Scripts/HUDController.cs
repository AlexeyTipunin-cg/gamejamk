using UnityEngine;

public class HUDController : MonoBehaviour
{
    [SerializeField] private Transform _playerHealthProgressBar;
    [SerializeField] private PlayerHealthComponent _playerHealth;
    private float _maxHealth;

    private void Awake()
    {
        _maxHealth = SceneConnection.bodyConfig.health;
        SetProgressBar(_maxHealth);

        _playerHealth.OnHealthChanged += SetProgressBar;
    }

    private void SetProgressBar(float health)
    {
        var scale = _playerHealthProgressBar.localScale;
        scale.x = Mathf.Max(health / _maxHealth, 0);
        _playerHealthProgressBar.localScale = scale;
    }
}