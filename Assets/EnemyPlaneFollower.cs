using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class EnemyPlaneFollower : MonoBehaviour
{
    private PlayerHealthComponent player { get; set; }
    [SerializeField] private ParticleSystem particles;
    [SerializeField] private float spawnFrequency;
    [SerializeField] private Rigidbody2D rb;

    private bool _isSpawned;

    private void Awake()
    {
        player = FindFirstObjectByType<PlayerHealthComponent>();
        InvokeRepeating("TryToSpawn", spawnFrequency, spawnFrequency);
        _isSpawned = false;
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
            _isSpawned = true;
            transform.position = player.transform.position + Vector3.left * 100;
        }
    }

    private void Move()
    {
        //transform.position = transform.position + Vector3.right * 4;
        rb.MovePosition(Vector3.right);
    }

    private void FixedUpdate()
    {
        if(!_isSpawned) return;

        Debug.Log("Player position " + player.transform.position.x.ToString() + "; " + transform.position.x.ToString());
        if (player.transform.position.x - transform.position.x > 20)
        {
            Debug.Log("Player position " + player.transform.position.x);
            Move();
        }
        else
        {
            if (particles.isPlaying)
            {
                particles.Play();
            }
        }
    }

    private void Update()
    {
        if(_isSpawned)
        {
            particles.Play();
        }
        //CalculateRotation(player.transform);
    }
    
    private bool shoot = false;

    private void CalculateRotation(Transform TargetObjTransform)
    {
        // float angle = Mathf.Atan2(TargetObjTransform.position.y - gun.position.y,
        //     TargetObjTransform.position.x - gun.position.x) * Mathf.Rad2Deg;

        //float angle = Vector2.Angle(new Vector2(TargetObjTransform.position.x - gun.position.x, TargetObjTransform.position.y - gun.position.y), Vector3.left);
        
        
        if (!shoot)
        {
            particles.Play();

            shoot = true;
        }
        /*
        else
        {
            shoot = false;
            particles.Stop();
            StopAllCoroutines();
        }
        */
        
        //Debug.Log(angle);
    }

    private void OnParticleCollision(GameObject other)
    {
        var t = other.GetComponent<ParticleSystem>();
        if (!t.customData.enabled)
        {
            _isSpawned = false;
            transform.position = -Vector3.down * 10000;
            particles.Stop();
        }
    }
}