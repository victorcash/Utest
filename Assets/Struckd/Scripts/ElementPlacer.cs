using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ElementPlacer : MonoBehaviour
{
    private GamePlayElementBehaviour currentElement;
    private GameMode gameMode => Services.GameStates.gameMode;
    private Camera editCamT => Services.Camera?.editCamera;
    private Vector2 screenPos => Mouse.current.position.ReadValue();
    private Vector3 PointerPostion(Vector3 planeNormal, Vector3 planePoint)
    {
        if (editCamT == null) return Vector3.zero;
        Math_3D.LinePlaneIntersection(
            out var pos, 
            editCamT.transform.position,
            editCamT.ScreenPointToRay(screenPos).direction,
            planeNormal, 
            planePoint);
        return pos;
    }
    public void PlaceElement(int elementId)
    {
        currentElement = Services.GamePlayElement.CreateGamePlayElement(elementId);
    }
    void Update()
    {
       if(currentElement != null)
        {
            currentElement.transform.position = PointerPostion(Vector3.up, Vector3.zero);
        }

    }

    private void OnMouseUp()
    {
        currentElement = null;
    }

    public void OnJump(InputValue value)
    {
        Debug.Log("Jumpped22222");
    }
}
