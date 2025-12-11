using UnityEngine;

[CreateAssetMenu(fileName = "SurfaceType", menuName = "Scriptable Objects/SurfaceType")]
public class SurfaceType : ScriptableObject
{
    public string SurfaceName = "Surface_Name";
    [Header("Physics set")]
    [Range(0, 1)] public float friction = 1f;          
    [Range(0, 2)] public float rollingResistance = 1f; 
    [Range(0, 1)] public float slipFactor = 0f;  
}
