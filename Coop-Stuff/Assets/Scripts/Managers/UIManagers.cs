using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManagers : MonoBehaviour
{
    public static UIManagers Instance {  get; private set; }

    [SerializeField] private GameObject _shopPanel;



    void Start()
    {
        Instance = this;
    }

    public void OpenToolShop()
    {

    }
}
