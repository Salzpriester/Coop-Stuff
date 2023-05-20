using UnityEngine;

public class Tool : ScriptableObject
{
    public Mesh Model;
    public int TimeBetweenHits;
    public float Range;
    public float TimeWhenToolActuallyHits;

    public virtual void LeftClickAction(GameObject tool)
    {
      
    }
}
