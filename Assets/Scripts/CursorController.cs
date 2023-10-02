using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CursorController : MonoBehaviour
{
    Vector3 cursorPosition;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //follow mouse
        if(TraceForTerrain())
        {
            transform.position = cursorPosition;
        }
        
    }

    bool TraceForTerrain()
    {
        int layerMask = 1 << 3;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
        {
            
            cursorPosition = hit.point;
            return true;
        }
        cursorPosition = Vector3.zero;  
        return false;
    }
}
