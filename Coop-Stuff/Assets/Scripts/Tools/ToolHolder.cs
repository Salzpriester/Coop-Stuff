using UnityEngine;

public class ToolHolder : MonoBehaviour
{
    [SerializeField] private Tool _tool;
    [SerializeField] private GameObject _toolGameObject;

    private Tool _previousTool;



    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            _tool.LeftClickAction(_toolGameObject);
        }

        if(_tool != _previousTool)
        {
            GetComponent<MeshFilter>().mesh = _tool.Model;
        }

        _previousTool = _tool;
    }
}
