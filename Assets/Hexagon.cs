using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[ExecuteAlways]
public class Hexagon : MonoBehaviour
{
    [SerializeField]
    private Hextypes _hexType;

    private Hextypes currentHexType;

    [SerializeField]
    private GameObject hex;

    [SerializeField]
    private int x;
    [SerializeField]
    private int y;

    [SerializeField]
    private int _kingdomId = -1;

    [SerializeField]
    private int currentKingdomId = -1;

    [SerializeField]
    private GameObject[] borders = new GameObject[6];

    [SerializeField]
    private Dictionary<int, int> dic;


    public Hextypes HexType { get { return _hexType; }
    }

    public int CurrentKingdomId { get => currentKingdomId;}


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
        if (currentKingdomId == -1)
        {
            borders[0].SetActive(false);
            borders[1].SetActive(false);
            borders[2].SetActive(false);
            borders[3].SetActive(false);
            borders[4].SetActive(false);
            borders[5].SetActive(false);
        }
        //    UpdateBorders();
    }

    public void Initialize(Hextypes type, int iKingdomId, int Xcord, int Ycord) 
    {
        x = Xcord;
        y = Ycord;
        currentHexType = type;
        _hexType = type;
        currentKingdomId = _kingdomId = iKingdomId;
        DestroyImmediate(hex);
        var v = Resources.Load<GameObject>("GridGenerator/Hexagons/" + currentHexType.ToString());
        if (v == null)
        {
            v = Resources.Load<GameObject>("GridGenerator/Hexagons/Error" );
        }
        hex = Instantiate(v, this.transform);
        name = "Hex [" + x.ToString() + ", " + y.ToString() + "]";
        Debug.Log(name + "type: " + type.ToString() + "currentType: " + currentHexType.ToString());

    }

    void OnMouseUp()
    {
        Debug.Log("Click!");

        if (EventSystem.current.IsPointerOverGameObject())
            return;

        Debug.Log("Generating!");
        GridController.Instance.GeneratePathTo(x, y);
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
        if (currentKingdomId != _kingdomId)
        {
            Debug.Log("new kingdom");
            currentKingdomId = _kingdomId;
            foreach (var item in GetNeighbours())
            {
                item.UpdateBorders();
            }
            UpdateBorders();
        }

        //   UpdateBorders();

    }

    //private void OnMouseDown()
    //{
    //    if (currentHexType != Brush.Instance.CurrentHexType)
    //    {
    //        Debug.Log(this.name + "change to " + Brush.Instance.CurrentHexType.ToString());
    //        _hexType = Brush.Instance.CurrentHexType;
    //    }
    //}

    private void OnMouseOver()
    {
        if (Input.GetMouseButton(0))
        {
            if (Brush.Instance.BushHextypes && (currentHexType != Brush.Instance.CurrentHexType))
            {
                _hexType = Brush.Instance.CurrentHexType;
            }

            if (Brush.Instance.BushKingdoms && (currentHexType != Hextypes.Water && currentKingdomId != Brush.Instance.CurrentKingdomId && Brush.Instance.CurrentKingdomId > 0))
            {
                _kingdomId = Brush.Instance.CurrentKingdomId;
            }
        }
    }

    public List<Hexagon> GetNeighbours(bool iMyKingdom = false) 
    {
        List<Hexagon> neighbours = new List<Hexagon>();
        //get left
        if (x - 1 >= 0)
        {
            neighbours.Add(GridController.Instance.Grid[x - 1, y]);
        }      
        //get right 
        if (x + 1 < Constants.gridSizeX)
        {
            neighbours.Add(GridController.Instance.Grid[x + 1, y]);
        }
        //get down 
        if (y - 1 >= 0)
        {
            neighbours.Add(GridController.Instance.Grid[x, y - 1]);
        }
        //get up 
        if (y + 1 < Constants.gridSizeY)
        {
            neighbours.Add(GridController.Instance.Grid[x, y + 1]);
        }
        //if even
        if (x % 2 == 0)
        {
            if (x - 1 >= 0 && y + 1 < Constants.gridSizeY)
            {
                neighbours.Add(GridController.Instance.Grid[x - 1, y + 1]);
            }
            if (x + 1 < Constants.gridSizeX && y + 1 < Constants.gridSizeY)
            {
                neighbours.Add(GridController.Instance.Grid[x + 1, y + 1]);
            }
        }
        else
        {
            if (x - 1 >= 0 && y - 1 >= 0)
            {
                neighbours.Add(GridController.Instance.Grid[x - 1, y - 1]);
            }
            if (x + 1 < Constants.gridSizeX && y - 1 >= 0)
            {
                neighbours.Add(GridController.Instance.Grid[x + 1, y - 1]);
            }
        }

        if (iMyKingdom)
        {
            List<Hexagon> MKneighbours = new List<Hexagon>();
            foreach (var item in neighbours)
            {
                if (item.currentKingdomId == currentKingdomId)
                {
                    MKneighbours.Add(item);
                }
            }

            return MKneighbours;
        }
        return neighbours;
    }

    public List<Hexagon> getMyKingdom() 
    {
        List<Hexagon> kingdom = new List<Hexagon>();
        kingdom.Add(this);
        List<Hexagon> newNeighbours = GetNeighbours(true);
        List<Hexagon> realyNewNeighbours = new List<Hexagon>();

        foreach (var item in newNeighbours)
        {
            if (!kingdom.Contains(item))
                realyNewNeighbours.Add(item);
        }

        while (realyNewNeighbours.Count != 0) 
        {
            kingdom.AddRange(realyNewNeighbours);

            newNeighbours.RemoveRange(0, newNeighbours.Count);
            foreach (var item in realyNewNeighbours)
            {
                newNeighbours.AddRange(item.GetNeighbours(true));
            }

            realyNewNeighbours.RemoveRange(0, realyNewNeighbours.Count);

            foreach (var item in newNeighbours)
            {
                if (!kingdom.Contains(item))
                    realyNewNeighbours.Add(item);
            }
        }
        return kingdom;
    }

    public void UpdateBorders() {
        //get down 
        if (y - 1 >= 0)
        {
            if (GridController.Instance.Grid[x, y - 1].currentKingdomId == currentKingdomId)
                borders[3].SetActive(false);
            else
                borders[3].SetActive(true);
        }
        //get up 
        if (y + 1 < Constants.gridSizeY)
        {
            if (GridController.Instance.Grid[x, y + 1].currentKingdomId == currentKingdomId)
                borders[0].SetActive(false);
            else
                borders[0].SetActive(true);
        }
        if (x % 2 == 0)
        {
            if (x - 1 >= 0)
            {
                if (GridController.Instance.Grid[x - 1, y].currentKingdomId == currentKingdomId)
                    borders[4].SetActive(false);
                else
                    borders[4].SetActive(true);
            }
            if (x - 1 >= 0 && y + 1 < Constants.gridSizeY)
            {
                if(GridController.Instance.Grid[x - 1, y + 1].currentKingdomId == currentKingdomId)
                    borders[5].SetActive(false);
                else
                    borders[5].SetActive(true);
            }
            if (x + 1 < Constants.gridSizeX)
            {
                if(GridController.Instance.Grid[x + 1, y].currentKingdomId == currentKingdomId)
                    borders[2].SetActive(false);
                else
                    borders[2].SetActive(true);
            }
            if (x + 1 < Constants.gridSizeX && y + 1 < Constants.gridSizeY)
            {
                if(GridController.Instance.Grid[x + 1, y + 1].currentKingdomId == currentKingdomId)
                    borders[1].SetActive(false);
                else
                    borders[1].SetActive(true);
            }
        }
        else
        {
            if (x - 1 >= 0)
            {
                if (GridController.Instance.Grid[x - 1, y].currentKingdomId == currentKingdomId)
                    borders[5].SetActive(false);
                else
                    borders[5].SetActive(true);
            }
            if (x - 1 >= 0 && y - 1 >= 0)
            {
                if (GridController.Instance.Grid[x - 1, y - 1].currentKingdomId == currentKingdomId)
                    borders[4].SetActive(false);
                else
                    borders[4].SetActive(true);
            }
            if (x + 1 < Constants.gridSizeX)
            {
                if (GridController.Instance.Grid[x + 1, y].currentKingdomId == currentKingdomId)
                    borders[1].SetActive(false);
                else
                    borders[1].SetActive(true);
            }
            if (x + 1 < Constants.gridSizeX && y - 1 >= 0)
            {
                if (GridController.Instance.Grid[x + 1, y - 1].currentKingdomId == currentKingdomId)
                    borders[2].SetActive(false);
                else
                    borders[2].SetActive(true);
            }
        }
        if (currentKingdomId == -1)
        {
            borders[0].SetActive(false);
            borders[1].SetActive(false);
            borders[2].SetActive(false);
            borders[3].SetActive(false);
            borders[4].SetActive(false);
            borders[5].SetActive(false);
        }
    }
}
