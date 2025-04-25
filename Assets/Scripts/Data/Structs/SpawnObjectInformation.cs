using UnityEngine;

[System.Serializable]
public struct SpawnObjectInformation
{
    public GameObject ObjectToSpawn;
    public Transform PositionToSpawn;
    public Vector3 RotationToSpawn;
    public bool DirectionRight;
}
