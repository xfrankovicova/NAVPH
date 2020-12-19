using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[ExecuteAlways]
public class BattleHexagon : MonoBehaviour
{
    [SerializeField]
    private BattleHextypes _hexType;

    private BattleHextypes currentHexType;

    [SerializeField]
    private GameObject hex;

    [SerializeField]
    private int x;

    [SerializeField]
    private int y;

    public int X => x;
    public int Y => y;

    [SerializeField]
    private Dictionary<int, int> dic;

    private GameObject unitOnTile;

    public BattleHextypes HexType => _hexType;
    public bool HasUnit()
    {
        return (unitOnTile != null);
    }

    public void EntetTileWithunit(GameObject unit)
    {
        unitOnTile = unit;
    }

    public void UnitLeaveTile()
    {
        unitOnTile = null;
    }

    void Start()
    {
        //        currentHexType = Hextypes.Water;
        if (hex == null)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                var g = transform.GetChild(0).gameObject;
                if (g.tag != "Border")
                {
                    DestroyImmediate(g);
                }
            }
            hex = Instantiate(Resources.Load<GameObject>("GridGenerator/Hexagons/" + currentHexType.ToString()), this.transform);
        }
    }

    public void Initialize(BattleHextypes type, int Xcord, int Ycord)
    {
        x = Xcord;
        y = Ycord;
        currentHexType = type;
        _hexType = type;
        
        DestroyImmediate(hex);

        var v = Resources.Load<GameObject>("GridGenerator/Hexagons/" + currentHexType.ToString());
        if (v == null)
        {
            v = Resources.Load<GameObject>("GridGenerator/Hexagons/Error");
        }
        
        hex = Instantiate(v, this.transform);
        name = "Hex [" + x.ToString() + ", " + y.ToString() + "]";
        
        Debug.Log(name + "type: " + type.ToString() + "currentType: " + currentHexType.ToString());

    }
    
    void OnMouseUp()
    {
        Debug.Log(x+"Click!"+y);

        if (EventSystem.current.IsPointerOverGameObject())
            return;

        Debug.Log("Generating!");
        BattleController.Instance.GeneratePathTo(x, y);
    }
    

    public void SetHextype(BattleHextypes type)
    {
        _hexType = type;
    }

    void Update()
    {
        if (currentHexType != _hexType)
        {
            currentHexType = _hexType;
            DestroyImmediate(hex);
            var j = transform.childCount;
            for (int i = 0; i < j; i++)
            {
                var g = transform.GetChild(0).gameObject;
                if (g.tag != "Border")
                {
                    DestroyImmediate(g);
                }
            }
            var v = Resources.Load<GameObject>("GridGenerator/Hexagons/" + currentHexType.ToString());

            if (v == null)
            {
                v = Resources.Load<GameObject>("GridGenerator/Hexagons/Error");
            }
            hex = Instantiate(v, this.transform);
        }

    }

    public List<BattleHexagon> GetNeighbours()
    {
        var v = (x % 2 == 0) ? Constants.neighboursEven : Constants.neighboursOdd;

        List<BattleHexagon> result = new List<BattleHexagon>();
        foreach (var item in v)
        {
            var i = item.Key.Item1 + x;
            var j = item.Key.Item2 + y;
            if (!(i < 0 || i >= Constants.battleMapSizeX || j < 0 || j >= Constants.battleMapSizeY))
            {
                result.Add(BattleController.Instance.Grid[i, j]);
            }
        }

        return result;
    }
}
