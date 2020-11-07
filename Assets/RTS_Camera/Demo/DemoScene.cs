using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DemoScene : MonoBehaviour 
{
    public Button btn45;
    public Button btn90;
    public Dropdown brushType;
    public Button plusKingdomId;
    public Button minusKingdomId;
    public Text kingdomIdText;
    public Toggle hextypeToggle;
    public Toggle kingdomIdToggle;

    private void Start()
    {
        Transform camT = Camera.main.transform;
        btn45.onClick.AddListener(() => SetXRotation(camT, 45f));
        btn90.onClick.AddListener(() => SetXRotation(camT, 90f));
        brushType.ClearOptions();
        foreach (var type in (Hextypes[])Hextypes.GetValues(typeof(Hextypes)))
        {
            brushType.options.Add(new Dropdown.OptionData(type.ToString()));
        }
        brushType.onValueChanged.AddListener(delegate {
            DropdownValueChanged(brushType);
        });
        brushType.value = 0;
        brushType.RefreshShownValue();
        plusKingdomId.onClick.AddListener(() => {
            Brush.Instance.CurrentKingdomId++;
            kingdomIdText.text = Brush.Instance.CurrentKingdomId.ToString();
            });
        minusKingdomId.onClick.AddListener(() => {
            Brush.Instance.CurrentKingdomId--;
            kingdomIdText.text = Brush.Instance.CurrentKingdomId.ToString();
        });
        kingdomIdText.text = Brush.Instance.CurrentKingdomId.ToString();
        hextypeToggle.onValueChanged.AddListener((bool v) =>
        {
            Brush.Instance.BushHextypes = v;
        });
        kingdomIdToggle.onValueChanged.AddListener((bool v) =>
        {
            Brush.Instance.BushKingdoms = v;
        });
    }
    void DropdownValueChanged(Dropdown change)
    {
        Brush.Instance.CurrentHexType = (Hextypes)change.value;
    }

    private void SetXRotation(Transform t, float angle)
    {
        t.localEulerAngles = new Vector3(angle, t.localEulerAngles.y, t.localEulerAngles.z);
    }

    public bool isUIOverride { get; private set; }

    void Update()
    {
        isUIOverride = EventSystem.current.IsPointerOverGameObject();
    }

}
