using System;
using System.Collections.Generic;
using UnityEngine;

public class MainBuilding : MonoBehaviour, IInteractable
{
    [Serializable]
    private struct Upgrade
    {
        public int WoodCost;
        public int RockCost;
        public Mesh NewModel;
        public Mesh NewColliderMesh;
    }

    [SerializeField] private List<Upgrade> _upgrades = new();

    private MeshFilter _meshFilter;
    private MeshCollider _meshCollider;

    private int _currentUpgradeIndex;


    private void Start()
    {
        _meshFilter = GetComponent<MeshFilter>();
        _meshCollider = GetComponent<MeshCollider>();
    }


    public void Interact()
    {
        if(RessourceManager.Instance.Wood >= _upgrades[_currentUpgradeIndex].WoodCost && RessourceManager.Instance.Rock >= _upgrades[_currentUpgradeIndex].RockCost)
        {
            _meshFilter.mesh = _upgrades[_currentUpgradeIndex].NewModel;
            _meshCollider.sharedMesh = _upgrades[_currentUpgradeIndex].NewModel;

            RessourceManager.Instance.IncreaseRessource(RessourceType.Wood, - _upgrades[_currentUpgradeIndex].WoodCost);
            RessourceManager.Instance.IncreaseRessource(RessourceType.Rock, - _upgrades[_currentUpgradeIndex].RockCost);

            _currentUpgradeIndex++;
        }
    }
}
