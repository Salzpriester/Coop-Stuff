using UnityEngine;

[CreateAssetMenu(fileName = "Axe", menuName = "Tools/Axe ", order = 1)]
public class Axe : Tool
{
    public override void LeftClickAction(GameObject axe)
    {
        axe.transform.GetComponent<Animator>().Play("Swing");

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit) && Vector3.Distance(hit.transform.position, axe.transform.position) <= Range)
        {
            if(hit.collider.transform.TryGetComponent(out Enemy enemy))
            {
                enemy.GetDamage(5);
            }

            if(hit.transform.TryGetComponent(out RessourceSource source) && source.GetRessourceType() == RessourceType.Wood)
            {
                source.Harvest(5, TimeWhenToolActuallyHits, hit.point);
            }
        }
    }
}
