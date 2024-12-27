using UnityEngine;

[CreateAssetMenu(fileName = "PlayerConfig", menuName = "ScriptableObjects/PLayerConfig", order = 1)]
public class EnemyConfig : ScriptableObject
{
    public float speed;
    public Vector2 noseRotationAngle;
    [Range(0, 1)]
    public float gravityValue;
    
    public float healthValue;
}
