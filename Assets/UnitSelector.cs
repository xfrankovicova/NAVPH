using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UnitSelector : MonoBehaviour
{
    void OnMouseUp()
    {

        if (EventSystem.current.IsPointerOverGameObject())
            return;

        Debug.Log("Click!" + name);


        Debug.Log("Generating!");
        GridController.Instance.SelectedUnit = this.gameObject;
    }
}
