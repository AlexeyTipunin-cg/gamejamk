using Unity.Mathematics;
using UnityEngine;

public class EnemyPlaneFollower : MonoBehaviour
{
    private EnemyHealthComponent enemyHealthComponent { get; set; }
    private PlayerHealthComponent playerHealthComponent { get; set; }
    [SerializeField] private ParticleSystem particles;
    [SerializeField] private float spawnFrequency;
    [SerializeField] private Rigidbody2D rb;

    private bool _isSpawned;

    private void Awake()
    {
        playerHealthComponent = FindFirstObjectByType<PlayerHealthComponent>();
        //enemyHealthComponent = FindFirstObjectByType<EnemyHealthComponent>();
        InvokeRepeating("TryToSpawn", spawnFrequency, spawnFrequency);
        _isSpawned = false;
        
        _enemyHealthComponent = gameObject.GetComponent<EnemyHealthComponent>();
        _enemyHealthComponent.OnDeath += OnDeath;

    }

    private void TryToSpawn()
    {
        if (_isSpawned)
            return;

        float weightMod = (SceneConnection.engineConfig.enginePower - SceneConnection.engineConfig.weight - SceneConnection.bodyConfig.weight) / SceneConnection.engineConfig.enginePower + 0.05f;
        float rand = UnityEngine.Random.Range(0f, 1f);
        Debug.Log("Follower spawn attempt " + rand.ToString() + " & w: " + weightMod.ToString());

        if (rand > math.max(weightMod, 0.25))
        {
            Debug.Log("Follwer spawned");
            _isSpawned = true;
            transform.position = playerHealthComponent.transform.position + Vector3.left * 25;
            _enemyHealthComponent.RefillHealth();
        }
    }

    private void Move(float speed)
    {
        //transform.position = transform.position + Vector3.right * 4;

        rb.MovePosition(rb.position + Vector2.right * speed + Vector2.up * (playerHealthComponent.transform.position.y - transform.position.y));
        //rb.MovePosition(rb.position + Vector2.right * speed + Vector2.up * (player.transform.position.y - transform.position.y));
    }

    private void FixedUpdate()
    {
        if(!_isSpawned) return;

        Debug.Log("Player position " + playerHealthComponent.transform.position.x.ToString() + "; " + transform.position.x.ToString());
        if (playerHealthComponent.transform.position.x - transform.position.x > 15)
        {
            Debug.Log("Player position " + playerHealthComponent.transform.position.x);
            Move(0.4f);
        }
        else
        {
            Move(0.2f);
            if (!particles.isPlaying)
            {
                Debug.Log("Spawned enemy shoots");
                particles.Play();
            }
        }
    }
    
    
    private bool shoot = false;
    private EnemyHealthComponent _enemyHealthComponent;


    private void OnParticleCollision(GameObject other)
    {
        if (other.TryGetComponent(out DamageComponent component))
        {
            _enemyHealthComponent.ReduceHealth(component.GetDamage(), false);
        }
    }
    
    private void OnDeath()
    {
        _isSpawned = false;
        transform.position = -Vector3.down * 10000;
        particles.Stop();
        Debug.Log("Spawned enemy death");
    }
}