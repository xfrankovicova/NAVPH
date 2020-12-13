using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingsUI : MonoBehaviour
{
    [SerializeField] private Button closeWindowBtn;
    [SerializeField] private GameObject buildingItem;
    [SerializeField] private Transform buildingHolder;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        closeWindowBtn.onClick.AddListener(CloseWindow);
    }

    private void CloseWindow()
    {
        this.gameObject.SetActive(false);
    }
}
