using System.Collections;
using UnityEngine;

public class RessourceSource : MonoBehaviour
{
    [Header("Parameters")]
    [SerializeField] private RessourceType RessourceType;
    [SerializeField] private int _amountOfRessourcesLeft;
    [SerializeField] private GameObject _onHitParticle;

    public RessourceType GetRessourceType() => RessourceType;
    public GameObject GetOnHitParticle() => _onHitParticle;




    public void Harvest(int Amount, float Delay, Vector3 hitPoint)
    {
        StartCoroutine(DelayTheHarvest(Amount, Delay, hitPoint));
    }


    IEnumerator DelayTheHarvest(int Amount, float Delay, Vector3 hitPoint)
    {
        yield return new WaitForSeconds(Delay);


        Amount = Mathf.Clamp(Amount, 0, _amountOfRessourcesLeft);
        _amountOfRessourcesLeft -= Amount;

        RessourceManager.Instance.IncreaseRessource(RessourceType, Amount);

        Instantiate(_onHitParticle, hitPoint, Quaternion.identity);

        if (_amountOfRessourcesLeft <= 0)
        {
            if(transform.parent != null)
            {
                Destroy(transform.parent.gameObject);
            }

            Destroy(gameObject);
        }

    }
}



