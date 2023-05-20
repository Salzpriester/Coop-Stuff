using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RessourceManager : MonoBehaviour
{
    public static RessourceManager Instance { get; private set; }

    [SerializeField] private TextMeshProUGUI _woodText;
    [SerializeField] private TextMeshProUGUI _rockText;
    [SerializeField] private TextMeshProUGUI _moneyText;
    


    private int _wood = 0;

	public int Wood
	{
		get { return _wood; }
		set 
		{ 
			_wood = value;
			_woodText.text = value.ToString();
		}
	}

    private int _rock = 0;

    public int Rock
    {
        get { return _rock; }
        set
        {
            _rock = value;
            _rockText.text = value.ToString();
        }
    }

    private int _money = 0;

    public int Money
    {
        get { return _money; }
        set
        {
            _money = value;
            _moneyText.text = value.ToString();
        }
    }


    private void Start()
    {
        Instance = this;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I)) IncreaseAll();
    }



    public void IncreaseRessource(RessourceType typeOfRessource, int AmountOfRessource)
    {
        switch (typeOfRessource)
        {
            case RessourceType.Rock: Rock += AmountOfRessource;
                break;
            case RessourceType.Wood: Wood += AmountOfRessource;
                break;
            case RessourceType.Money: Money += AmountOfRessource;
                break;
        }
    }



    private void IncreaseAll()
    {
        Money += Random.Range(1, 10);
        Rock += Random.Range(1, 10);
        Wood += Random.Range(1, 10);
    }
}


public enum RessourceType { Rock, Wood, Money }