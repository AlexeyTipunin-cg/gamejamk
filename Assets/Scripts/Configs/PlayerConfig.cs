using UnityEngine;

[CreateAssetMenu(fileName = "PlayerConfig", menuName = "ScriptableObjects/EnemyConfig", order = 1)]
public class PlayerConfig  : ScriptableObject
{
    
    public float speed;
    public Vector2 noseRotationAngle;
    [Range(0, 1)]
    public float gravityValue;
    
    public float healthValue;

    public float verticalSpeed;
}
