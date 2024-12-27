using UnityEngine;


[CreateAssetMenu(fileName = "BodyConfig", menuName = "ScriptableObjects/BodyConfig", order = 1)]
public class BodyConfig : ScriptableObject
{
    public string bodyName;
    public float weight;
    public int health;
}