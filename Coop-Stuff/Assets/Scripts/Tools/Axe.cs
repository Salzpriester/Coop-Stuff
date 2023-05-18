using UnityEngine;

[CreateAssetMenu(fileName = "Axe", menuName = "Tools/Axe ", order = 1)]
public class Axe : Tool
{
    public override void LeftClickAction(GameObject axe)
    {
        axe.transform.GetComponent<Animator>().Play("AxeSwing");

        if(Physics.Raycast(axe.transform.parent.position, axe.transform.parent.forward, out RaycastHit hit))
        {
            if(hit.transform.CompareTag("Tree") && Vector3.Distance(hit.transform.position, axe.transform.position) <= Range)
            {
                hit.transform.gameObject.SetActive(false);
            }
        }
        
    }
}
