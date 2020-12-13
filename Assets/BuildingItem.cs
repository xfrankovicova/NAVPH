using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuildingItem : MonoBehaviour
{
    [SerializeField] private Button buildingBtn;
    [SerializeField] private Image buildingImage;
    [SerializeField] private TMP_Text buildingName;
    [SerializeField] private GameObject costItem;
    [SerializeField] private Transform costHolder;
    [SerializeField] private GameObject benefitItem;
    [SerializeField] private Transform benefitHolder;

    // Start is called before the first frame update
    void Start()
    {
        Initialize("Market");
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void Initialize(string buildingName)
    {
        this.buildingName.text = buildingName;
    }
}
