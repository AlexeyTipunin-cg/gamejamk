using UnityEngine;


[CreateAssetMenu(fileName = "WeaponConfig", menuName = "ScriptableObjects/WeaponConfig", order = 1)]
public class WeaponConfig : ScriptableObject
{
    public Vector2 angles;
    public float cooldownInSeconds;
}