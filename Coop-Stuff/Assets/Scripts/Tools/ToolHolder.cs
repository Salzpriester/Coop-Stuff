using System.Collections.Generic;
using System.Data.Common;
using UnityEngine;

public class ToolHolder : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject _toolGameObject;
    [SerializeField] private List<Tool> _tools = new();


    private int _toolIndex;
    public int ToolIndex
    {
        get { return _toolIndex; }
        set 
        { 
            _toolIndex = value; 
            OnToolIndexChange();
        }
    }


    [SerializeField] private Tool _currentTool;
    

    private float _hitDelay;
    private Animator _toolAnimator;
    private MeshFilter _toolMeshFilter;

    private void Start()
    {
        _currentTool = _tools[_toolIndex];

        _toolAnimator = _toolGameObject.GetComponent<Animator>();
        _toolMeshFilter = _toolGameObject.GetComponent<MeshFilter>();
    }



    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && _hitDelay <= 0)
        {
            _currentTool.LeftClickAction(_toolGameObject);
            _hitDelay = _currentTool.TimeBetweenHits;
        }

        _hitDelay -= Time.deltaTime;

        ToolIndex += (int) Input.mouseScrollDelta.y;
    }



    private void OnToolIndexChange()
    {
        if(ToolIndex > _tools.Count - 1) 
        {
            ToolIndex = 0;
            return;
        }
        if(ToolIndex < 0)
        {
            ToolIndex = _tools.Count - 1;
            return;
        }


        _currentTool = _tools[ToolIndex];
        _toolMeshFilter.mesh = _currentTool.Model;
        _toolAnimator.runtimeAnimatorController = _currentTool.Controller;
        _toolAnimator.Play("");
    }
}
