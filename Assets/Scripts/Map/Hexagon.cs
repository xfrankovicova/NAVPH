using System;
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

    public List<Hexagon> GetNeighbours() 
    {
        var v = (x % 2 == 0) ? Constants.neighboursEven : Constants.neighboursOdd;

        List<Hexagon> result = new List<Hexagon>();
        foreach (var item in v)
        {
            var i = item.Key.Item1 + x;
            var j = item.Key.Item2 + y;
            if (!(i < 0 || i >= Constants.gridSizeX || j < 0 || j >= Constants.gridSizeY))
            {
                result.Add(GridController.Instance.Grid[i, j]);
            }
        }

        return result;
    }

    public void UpdateBorders() {
        if (currentKingdomId == -1)
        {
            borders[0].SetActive(false);
            borders[1].SetActive(false);
            borders[2].SetActive(false);
            borders[3].SetActive(false);
            borders[4].SetActive(false);
            borders[5].SetActive(false);
            return;
        }
        var v = (x % 2 == 0) ? Constants.neighboursEven : Constants.neighboursOdd;
        foreach (var item in v)
        {
            if (!(item.Key.Item1 + x < 0 || item.Key.Item1 + x >= Constants.gridSizeX || item.Key.Item2 +y < 0 || item.Key.Item2 +y>= Constants.gridSizeY))
            {
                if (GridController.Instance.Grid[x + item.Key.Item1, y + item.Key.Item2].currentKingdomId == currentKingdomId)
                    borders[item.Value].SetActive(false);
                else
                    borders[item.Value].SetActive(true);
            }
            else
            {
                borders[item.Value].SetActive(true);
            }
        }
    }

    public void SetBorderMat(Material mat) 
    {
        borders[0].GetComponent<MeshRenderer>().material = mat;
        borders[1].GetComponent<MeshRenderer>().material = mat;
        borders[2].GetComponent<MeshRenderer>().material = mat;
        borders[3].GetComponent<MeshRenderer>().material = mat;
        borders[4].GetComponent<MeshRenderer>().material = mat;
        borders[5].GetComponent<MeshRenderer>().material = mat;
    }
}
