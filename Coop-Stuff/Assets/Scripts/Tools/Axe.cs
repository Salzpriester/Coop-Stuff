using UnityEngine;

[CreateAssetMenu(fileName = "Axe", menuName = "Tools/Axe ", order = 1)]
public class Axe : Tool
{
    public override void LeftClickAction()
    {
        Debug.Log("Left click with axe");
    }
}
