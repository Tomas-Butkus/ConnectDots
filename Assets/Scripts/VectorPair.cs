using UnityEngine;

public struct VectorPair
{
    public Vector3 startVector;
    public Vector3 endVector;
    public int dotIndex;

    public VectorPair(Vector3 startVector, Vector3 endVector, int dotIndex)
    {
        this.startVector = startVector;
        this.endVector = endVector;
        this.dotIndex = dotIndex;
    }
}
