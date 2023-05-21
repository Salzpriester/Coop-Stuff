using UnityEngine;

[CreateAssetMenu(fileName = "Pickaxe", menuName = "Tools/Pickaxe ", order = 2)]
public class Pickaxe : Tool
{
    public override void LeftClickAction(GameObject pickaxe)
    {
        pickaxe.transform.GetComponent<Animator>().Play("Swing");

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit) && Vector3.Distance(hit.transform.position, pickaxe.transform.position) <= Range)
        {
            if (hit.transform.TryGetComponent(out RessourceSource source) && source.GetRessourceType() == RessourceType.Rock)
            {
                source.Harvest(5, TimeWhenToolActuallyHits, hit.point);
            }
        }
    }
}
