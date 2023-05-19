using UnityEngine;

[CreateAssetMenu(fileName = "Axe", menuName = "Tools/Axe ", order = 1)]
public class Axe : Tool
{
    public override void LeftClickAction(GameObject axe)
    {
        axe.transform.GetComponent<Animator>().Play("AxeSwing");

        if(Physics.Raycast(axe.transform.parent.parent.position, axe.transform.parent.parent.forward, out RaycastHit hit))
        {
            if(hit.collider.transform.TryGetComponent(out Enemy enemy))
            {
                enemy.GetDamage(5);
            }


            if(hit.transform.CompareTag("Tree") && Vector3.Distance(hit.transform.position, axe.transform.position) <= Range)
            {
                hit.transform.gameObject.SetActive(false);
            }
        }
    }
}
