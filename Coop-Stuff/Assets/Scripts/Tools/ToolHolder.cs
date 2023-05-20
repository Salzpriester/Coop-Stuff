using UnityEngine;

public class ToolHolder : MonoBehaviour
{
    [SerializeField] private Tool _tool;
    [SerializeField] private GameObject _toolGameObject;

    private float _hitDelay;


    private Tool _previousTool;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && _hitDelay <= 0)
        {
            _tool.LeftClickAction(_toolGameObject);
            _hitDelay = _tool.TimeBetweenHits;
        }

        if(_tool != _previousTool)
        {
            _toolGameObject.GetComponent<MeshFilter>().mesh = _tool.Model;
        }

        _hitDelay -= Time.deltaTime;
        _previousTool = _tool;
    }
}
