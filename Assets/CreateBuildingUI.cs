using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreateBuildingUI : MonoBehaviour
{
    [SerializeField] private Button createBtn;
    [SerializeField] private Button cancelBtn;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        cancelBtn.onClick.AddListener(Cancel);
    }


    private void Cancel()
    {
        this.gameObject.SetActive(false);
    }
}
