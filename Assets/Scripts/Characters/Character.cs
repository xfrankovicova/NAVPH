using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    private string characterName => data.characterName + " " + data.pseudonym;
    private string FullName => characterName + ", " + Title() + " of " + kingdom.Name;
    [SerializeField]
    private bool isPlayer;
    private Character overlord => TurnController.Instance.GetRuller(data.overLordId);
    [SerializeField]
    private CharacterData data;
    public CharacterData Data => data;
    [SerializeField]
    private Kingdom kingdom;

    private List<Attribute> traits;
    public List<Attribute> Traits => traits;

    private string Title()
    {
        string title = "";
        if (overlord == null)
        {
            title = "Duke";
        }
        else
        {
            if (overlord.overlord == null)
            {
                title = "Count";
            }
            else
            {
                title = "Baronn";
            }
        }
        return title;
    }

    // Start is called before the first frame update
    private void Start()
    {
        isPlayer = false;
    }

    public void setPlayer() 
    {
        isPlayer = true;
        Debug.Log(data.id + ". character is set us Player");
    }

    public Character(CharacterData iData) 
    {
        data = iData;
        isPlayer = false;
        kingdom = KingdomController.Instance.getKingdom(data.land.id);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeTurn() 
    {
        if (isPlayer)
            Debug.Log(FullName + " Player started his turn, wait until he ends.");
        else
            Debug.Log(FullName + " is makeing moves.");
    }
}

