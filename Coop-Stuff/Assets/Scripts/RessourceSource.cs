using System.Collections;
using UnityEngine;

public class RessourceSource : MonoBehaviour
{
    [Header("Parameters")]
    public RessourceType RessourceType;
    [SerializeField] private int _amountOfRessourcesLeft;

    public void Harvest(int Amount, float Delay)
    {
        StartCoroutine(DelayTheHarvest(Amount, Delay));
    }


    IEnumerator DelayTheHarvest(int Amount, float Delay)
    {
        yield return new WaitForSeconds(Delay);


        Amount = Mathf.Clamp(Amount, 0, _amountOfRessourcesLeft);
        _amountOfRessourcesLeft -= Amount;

        RessourceManager.Instance.IncreaseRessource(RessourceType, Amount);

        if (_amountOfRessourcesLeft <= 0)
        {
            Destroy(gameObject);
        }

    }
}



