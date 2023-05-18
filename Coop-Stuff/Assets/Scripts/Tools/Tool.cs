using UnityEngine;

public class Tool : ScriptableObject
{
    public Mesh Model;
    public int MiningSpeed;
    public float Range;

    public virtual void LeftClickAction(GameObject tool)
    {

    }
}
