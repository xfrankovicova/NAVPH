using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterItem : MonoBehaviour
{
    [SerializeField] private TMP_Text characterName;
    [SerializeField] private TMP_Text kingdom;
    [SerializeField] private Image portrait;
    [SerializeField] private Button showOnMapBtn;
    [SerializeField] private Button chooseCharacterBtn;

    // Start is called before the first frame update
    void Start()
    {
        Initialize("Name of character", "Kingdom");
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void Initialize(string name, string kingdom)
    {
        this.characterName.text = name;
        this.kingdom.text = kingdom;
    }
}
