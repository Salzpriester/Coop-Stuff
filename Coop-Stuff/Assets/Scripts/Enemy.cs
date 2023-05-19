
using UnityEngine;


public class Enemy : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _health;
    [SerializeField] private GameObject _player;
    [SerializeField] private float _range;
    [SerializeField] private float _attackrange;
    [SerializeField] public int _damage;

    private Rigidbody _rigidbody;



    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        InvokeRepeating(nameof(DealDamage), 0, 1);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Vector3.Distance(gameObject.transform.position, _player.transform.position) <= _range)
        {
            Vector3 direction = _player.transform.position - gameObject.transform.position;
            direction.Normalize();
            transform.position += _speed * Time.deltaTime * transform.forward;


            var lookPos = _player.transform.position - transform.position;
            lookPos.y = 0;
            var rotation = Quaternion.LookRotation(lookPos);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 4);

        }
    }


    private void DealDamage()
    {
        if (Vector3.Distance(gameObject.transform.position, _player.transform.position) <= _attackrange)
        {
            _player.GetComponent<Player>().GetDamage(_damage);

        }
    }
    public void GetDamage(int damage)
    {
        _health -= damage;
        Debug.Log(_health + "from Enemy");
        CheckIfDead();

    }
    private void CheckIfDead()
    {
        if (_health <= 0)
        {
            Debug.Log("EnemyDead");

        }

    }
}