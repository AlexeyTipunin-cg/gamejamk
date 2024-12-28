using UnityEngine;


[CreateAssetMenu(fileName = "WeaponConfig", menuName = "ScriptableObjects/WeaponConfig", order = 1)]
public class WeaponConfig : ScriptableObject
{
    public string name;
    public float weight;
    public int damage;
    public Vector2 angles;
    public float cooldownInSeconds;
    public Sprite weaponSprite;
}