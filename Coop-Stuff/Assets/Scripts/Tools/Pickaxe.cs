using UnityEngine;

[CreateAssetMenu(fileName = "Pickaxe", menuName = "Tools/Pickaxe ", order = 2)]
public class Pickaxe : Tool
{
    public override void LeftClickAction(GameObject pickaxe)
    {
        pickaxe.transform.GetComponent<Animator>().Play("AxeSwing");

        if (Physics.Raycast(pickaxe.transform.parent.position, pickaxe.transform.parent.forward, out RaycastHit hit))
        {
            if (hit.transform.CompareTag("Rock") && Vector3.Distance(hit.transform.position, pickaxe.transform.position) <= Range)
            {
                hit.transform.gameObject.SetActive(false);
            }
        }
    }
}
