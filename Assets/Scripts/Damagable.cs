using UnityEngine;

public class Damagable : MonoBehaviour
{
    [SerializeField] private PlayerHealthComponent _playerHealth;

    private void AddDamage(float damage)
    {
        _playerHealth.ReduceHealth(damage);
    }
    

    private void OnParticleCollision(GameObject other)
    {
        if (other.TryGetComponent(out DamageComponent damage))
        {
            AddDamage(damage.GetDamage());
        }
    }
}