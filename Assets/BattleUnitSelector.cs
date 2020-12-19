using UnityEngine;
using UnityEngine.EventSystems;

public class BattleUnitSelector : MonoBehaviour
{
    void OnMouseUp()
    {

        if (EventSystem.current.IsPointerOverGameObject())
            return;

        Debug.Log("Click!" + name);


        Debug.Log("Generating!");
        BattleController.Instance.SelectedUnit = this.gameObject;
    }
}
