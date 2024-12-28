using UnityEngine;


[CreateAssetMenu(fileName = "WeaponSlotConfig", menuName = "ScriptableObjects/WeaponSlotConfig", order = 1)]
public class WeaponSlotConfig : ScriptableObject
{
    public int index;
    public Vector2 positionInConstructWindow;
    public float angleInConstructWindow;
    public float scale;

    public Vector2 positionInGame;
    public float angleInGame;
    public Vector2 angles;
}