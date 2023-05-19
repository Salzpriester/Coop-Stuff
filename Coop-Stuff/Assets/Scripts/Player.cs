
using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private int _health;
    [SerializeField] private int _maxHealth;

    private float _lastTimeHealthChanged;






    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating(nameof(Regenerate), 0, 1);
    }

    // Update is called once per frame
    public void GetDamage(int damage)
    {
       _health -= damage;
        Debug.Log(_health);
        CheckIfDead();
        _lastTimeHealthChanged = 2;
    }
    private void CheckIfDead() 
    {
        if (_health <= 0)
        {
            Debug.Log("PlayerDead");

        }

    }
    private void Update()
    {
        if (_lastTimeHealthChanged > 0)
        {
            _lastTimeHealthChanged -= Time.deltaTime;
        }
    }
    private void Regenerate()
    {
        if (_lastTimeHealthChanged <= 0 && _health < _maxHealth)
        {
            _health += 5;
            Debug.Log(_health);
        }
    }
   


}
