using UnityEngine;


[CreateAssetMenu(fileName = "EngineConfig", menuName = "ScriptableObjects/EngineConfig", order = 1)]
public class EngineConfig : ScriptableObject
{
    public string engineName;
    public float weight;
    public int enginePower;
}