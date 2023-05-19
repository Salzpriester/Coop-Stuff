
using UnityEngine;


public class Enemy : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _life;
    [SerializeField] private GameObject _player;
    [SerializeField] private float _range;
    [SerializeField] private float _attackrange;
    [SerializeField] private float _damage;

    private Rigidbody _rigidbody;



    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Vector3.Distance(gameObject.transform.position, _player.transform.position) <=_range) 
        {
            Vector3 direction = _player.transform.position - gameObject.transform.position;
            direction.Normalize();
            transform.LookAt(_player.transform.position);
            _rigidbody.AddForce(direction * _speed);

        }
    }

    private void Update()
    {
        if (Vector3.Distance(gameObject.transform.position, _player.transform.position) <= _attackrange) 
        {
            
        }




    }

}
