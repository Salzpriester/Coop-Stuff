using UnityEngine;

[CreateAssetMenu(fileName = "Pickaxe", menuName = "Tools/Pickaxe ", order = 2)]
public class Pickaxe : Tool
{
    public override void LeftClickAction(GameObject pickaxe)
    {
        pickaxe.transform.GetComponent<Animator>().Play("Swing");

        if (Physics.Raycast(pickaxe.transform.parent.parent.position, pickaxe.transform.parent.parent.forward, out RaycastHit hit) && Vector3.Distance(hit.transform.position, pickaxe.transform.position) <= Range)
        {
            if (hit.transform.TryGetComponent(out RessourceSource source) && source.RessourceType == RessourceType.Rock)
            {
                Debug.Log("Raycasted");

                source.Harvest(5, TimeWhenToolActuallyHits);
            }
        }
    }
}
