using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterUI : MonoBehaviour
{
    [SerializeField] private Button   closeWindowBtn;
    [SerializeField] private TMP_Text characterName;
    [SerializeField] private Image portrait;
    [SerializeField] private TMP_Text age;
    [SerializeField] private TMP_Text title;
    [SerializeField] private Button   kingdomBtn;
    [SerializeField] private TMP_Text kingdomName;
    [SerializeField] private Button   overlordBtn;
    [SerializeField] private TMP_Text overlordName;
    [SerializeField] private TMP_Text statistic1;
    [SerializeField] private TMP_Text statistic2;
    [SerializeField] private TMP_Text statistic3;
    [SerializeField] private TMP_Text statistic4;


    // Start is called before the first frame update
    void Start()
    {
        Initialize("Edric", "40", "King", "Kingdom", "Overlord", "5", "6", "7", "8");
    }

    // Update is called once per frame
    void Update()
    {
        closeWindowBtn.onClick.AddListener(CloseWindow);
    }

    private void Initialize(string characterName, string age, string title, string kingdomName, string overlordName,
                    string statistic1, string statistic2, string statistic3, string statistic4)
    {
        this.characterName.text = characterName;
        this.age.text = age;
        this.title.text = title;
        this.kingdomName.text = kingdomName;
        this.overlordName.text = overlordName;
        this.statistic1.text = statistic1;
        this.statistic2.text = statistic2;
        this.statistic3.text = statistic3;
        this.statistic4.text = statistic4;
    } 

    private void CloseWindow()
    {
        this.gameObject.SetActive(false);
    }
}
