using UnityEngine;

public class ToolHolder : MonoBehaviour
{
    [SerializeField] private Tool _tool;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            _tool.LeftClickAction();
        }
    }
}
