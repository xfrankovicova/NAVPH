using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KingdomController : MonoBehaviour
{
    private static KingdomController _instance;

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

    public void Update()
    {
        Debug.Log(GetSuperKingdomId(1).ToString());
        Debug.Log(GetSuperKingdomId(2).ToString());
        Debug.Log(GetSuperKingdomId(3).ToString());
        Debug.Log(GetSuperKingdomId(4).ToString());
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
