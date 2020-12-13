using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TownUI : MonoBehaviour
{
    [SerializeField] private Button   closeWindowBtn;
    [SerializeField] private TMP_Text townName;
    [SerializeField] private Button   kingBtn;
    [SerializeField] private TMP_Text kingName;
    [SerializeField] private Button   kingdomBtn;
    [SerializeField] private TMP_Text kingdomName;
    [SerializeField] private TMP_Text wealth;
    [SerializeField] private TMP_Text freeMen;
    [SerializeField] private Button   buildingBtn1;
    [SerializeField] private TMP_Text buildingName1;
    [SerializeField] private Button   buildingBtn2;
    [SerializeField] private TMP_Text buildingName2;
    [SerializeField] private Button   buildingBtn3;
    [SerializeField] private TMP_Text buildingName3;
    [SerializeField] private Button   buildingBtn4;
    [SerializeField] private TMP_Text buildingName4;
    [SerializeField] private Button   buildingBtn5;
    [SerializeField] private TMP_Text buildingName5;
    [SerializeField] private Button   buildingBtn6;
    [SerializeField] private TMP_Text buildingName6;
    [SerializeField] private Button hireArmyBtn;

    // Start is called before the first frame update
    void Start()
    {
        Initialize("Town", "King", "Kingdom", "100", "10", "Building1", "Building2", "Building3", "Building4", "Building5", "Building6");
    }

    // Update is called once per frame
    void Update()
    {
        closeWindowBtn.onClick.AddListener(CloseWindow);
    }

    private void Initialize(string townName, string kingName, string kingdomName, string wealth, string freeMen,
                            string buildingName1, string buildingName2, string buildingName3, 
                            string buildingName4, string buildingName5, string buildingName6)
    {
        this.townName.text = townName;
        this.kingName.text = kingName;
        this.kingdomName.text = kingdomName;
        this.wealth.text = wealth;
        this.freeMen.text = freeMen;
        this.buildingName1.text = buildingName1;
        this.buildingName2.text = buildingName2;
        this.buildingName3.text = buildingName3;
        this.buildingName4.text = buildingName4; 
        this.buildingName5.text = buildingName5;
        this.buildingName6.text = buildingName6;
    }

    private void CloseWindow()
    {
        this.gameObject.SetActive(false);
    }
}
