using UnityEngine;

[CreateAssetMenu(fileName = "Pickaxe", menuName = "Tools/Pickaxe ", order = 1)]
public class Pickaxe : Tool
{
    public override void LeftClickAction()
    {
        Debug.Log("Left click with pickaxe");
    }
}
