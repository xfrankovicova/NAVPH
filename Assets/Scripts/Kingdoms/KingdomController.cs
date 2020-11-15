using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KingdomController : MonoBehaviour
{
    private static KingdomController _instance;
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

    public void AssignHex(Hexagon hex)
    {

        int key = hex.CurrentKingdomId;
        if (key <= 0)
            return;

        if (kingdoms.ContainsKey(key))
        {
            kingdoms[key].AddHex(hex);
        }
        else
        {
            kingdoms.Add(key, new Kingdom(key));
        }
    }

    public void UpdateBorders() 
    {
        foreach (var kingdom in kingdoms.Values)
        {
            kingdom.UpdateBorder();
            Debug.Log("Updating border mat on kingdom #" + kingdom.KingdomId.ToString() + ". Number of hexes: " + kingdom.KingdomSize + ".");
        }
    }
}
