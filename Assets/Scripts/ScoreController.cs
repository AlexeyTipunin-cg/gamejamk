using System;
using System.Collections;
using UnityEngine;

public class ScoreController : MonoBehaviour
{
    [SerializeField] private float timeToGetOnePoint;
    private PlayerHealthComponent _health;
    private bool canScore = true;
    
    private int score = 0;

    private void Start()
    {
        StartCoroutine(UpdateScore());
        _health = FindFirstObjectByType<PlayerHealthComponent>();
        _health.OnDeath += () => { canScore = false; };
    }
    
    public static event Action<int> OnScoreUpdate;

    private IEnumerator UpdateScore()
    {
        while (canScore)
        {
            yield return new WaitForSeconds(timeToGetOnePoint);
            if (canScore)
            {
                score += 1;
                OnScoreUpdate?.Invoke(score);
            }
        }
    }
}
