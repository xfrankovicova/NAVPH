using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KingdomController : MonoBehaviour
{
    private static KingdomController _instance;

    public GameObject polHex;
     
    public Settlement s;
    public static KingdomController Instance
    {
        get
        {
            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
        Debug.Log(_instance.ToString());
    }

    [SerializeField]
    private Dictionary<int, Kingdom> kingdoms = new Dictionary<int, Kingdom>();
    [SerializeField]
    private List<Kingdom> meh = new List<Kingdom>();

    public void AssignHex(Hexagon hex)
    {

        int key = hex.CurrentKingdomId;
        if (key <= 0)
            return;

        if (!kingdoms.ContainsKey(key))
        {
            kingdoms.Add(key, new Kingdom(key));
            kingdoms[key].AddHex(hex);
            meh.Add(kingdoms[key]);
        }
        
        kingdoms[key].AddHex(hex);
    }

    public void UpdateBorders()
    {
        foreach (var kingdom in kingdoms.Values)
        {
            kingdom.UpdateBorder();
            Debug.Log("Updating border mat on kingdom #" + kingdom.KingdomId.ToString() + ". Number of hexes: " + kingdom.KingdomSize + ".");
        }
    }

    public Kingdom getKingdom(int iKingdomId) 
    {
        if (kingdoms.ContainsKey(iKingdomId))
        {
            return kingdoms[iKingdomId];
        }
        else
        {
            Debug.LogError("No kingdom associated with id: " + iKingdomId.ToString());
            return null;
        }
    }

    [SerializeField]
    GameObject KingdomTextHolder;

    public void CreateKingdomNameUI(Vector3 position, string kingdomName, float angle) 
    {
        //var go = Instantiate(KingdomTextHolder, position, Quaternion.Euler(0, 0, angle));
        //// go.transform.rotation = Quaternion.Euler(0, 0, angle);
        //go.GetComponent<clampCamera>().text.text = kingdomName;
        //go.GetComponent<clampCamera>().text.gameObject.transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    public void LoadKingdomsData() 
    {
        foreach (var item in kingdoms.Values)
        {
            item.LoadData();
        }
        foreach (var item in kingdoms.Values)
        {
            Debug.Log(item.data.id.ToString() + ": " + item.FullName);
        }
        foreach (var item in kingdoms.Values)
        {
            item.GetMiddle();
        }
    }

    public int GetSuperKingdomId(int iIkingdomId) 
    {
        int val = 0;
        if (kingdoms.ContainsKey(iIkingdomId))
        {
            if (kingdoms[iIkingdomId].data.overLordId == 0)
            {
                val = iIkingdomId;
            }
            else
            {
                val = GetSuperKingdomId(kingdoms[iIkingdomId].data.overLordId);
            }
        }
        return val;
    }
}
