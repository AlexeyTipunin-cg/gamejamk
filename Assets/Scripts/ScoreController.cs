using System;
using System.Collections;
using UnityEngine;

public class ScoreController : MonoBehaviour
{
    public static bool hasHighScore = false;
    public static bool isNewRecord = false;
    public static int oldRecordScore = 0;
    
    
    private const string scoreKey = "high_score";
    [SerializeField] private float timeToGetOnePoint;
    private PlayerHealthComponent _health;
    private bool canScore = true;
    
    private int score = 0;

    public static event Action OnScoreResult;

    private void Start()
    {
        
        hasHighScore = false;
        isNewRecord = false;
        oldRecordScore = 0;
        
        StartCoroutine(UpdateScore());
        _health = FindFirstObjectByType<PlayerHealthComponent>();
        _health.OnDeath += () =>
        {
            canScore = false;
            hasHighScore = PlayerPrefs.HasKey(scoreKey);
            if (hasHighScore)
            {
                oldRecordScore = PlayerPrefs.GetInt(scoreKey, 0);
                if (score > oldRecordScore)
                {
                    isNewRecord = true;
                    PlayerPrefs.SetInt(scoreKey, score);
                }
            }
            else
            {
                PlayerPrefs.SetInt(scoreKey, score);
            }
            
            OnScoreResult?.Invoke();

        };
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
