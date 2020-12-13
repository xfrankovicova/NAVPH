using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System.Linq;
using System.Text.RegularExpressions;
using UnityScript.Steps;
using System;

[ExecuteAlways]
public class GridGenerator : EditorWindow
{
    [MenuItem("Custom/GenerateGrid %g")]
    public static void OpenWindow()
    {
        GetWindow<GridGenerator>();
    }

    void OnEnable()
    {
        hexPrefab = Resources.Load<GameObject>("GridGenerator/Hexagon");
        gridPrefab = Resources.Load<GameObject>("GridGenerator/Grid");
        collumnHolder = Resources.Load<GameObject>("GridGenerator/Collumn");
        data = Resources.Load<MapData>("GameState");
        grid = GameObject.FindGameObjectWithTag("Main grid");
        if (grid == null)
        {
            Debug.Log("Grid not found");
        }
        else
        {
            Debug.Log("Grid found");
        }
    }

    void OnGUI()
    {
        //Draw things here. Same as custom inspectors, EditorGUILayout and GUILayout has most of the things you need

        if (GUILayout.Button("Generating Grid"))
        {
            Debug.Log("Generating Grid");
            GenerateGrid();
        }

        if (GUILayout.Button("Save Grid"))
        {
            Debug.Log("Saveing grid");
            SaveGrid();
        }

        if (GUILayout.Button("Load Grid"))
        {
            Debug.Log("Loading grid");
            LoadGrid();
        }

        if (GUILayout.Button("Update borders"))
        {
            Debug.Log("Updateing borders");
            UpdateBorders();
        }

        if (GUILayout.Button("Update KID"))
        {
            Debug.Log("Updateing KID");
            UpdateKingdomsId();
        }

        if (GUILayout.Button("Debug"))
        {
            Debug.Log("debig");
            DebugMapKingdomsIDs();
        }
    }

    [SerializeField] private MapData data;
    private GameObject grid;

    [SerializeField] private GameObject hexPrefab;
    [SerializeField] private GameObject gridPrefab;
    [SerializeField] private GameObject collumnHolder;
    private GameObject gridHolder;
    private float yOffset = 0.867f;
    private float xOffsetOddRow = 0.5f;
    private int X = 40;
    private int Y = 25;

    private void GenerateGrid()
    {
        gridHolder = GameObject.Instantiate(gridPrefab, new Vector3(0f, 0f, 0f), Quaternion.identity);
        gridHolder.name = "Grid";
        gridHolder.tag = "Main grid";
        grid = gridHolder;
        for (int i = 0; i < X; i++)
        {
            if ((i % 2) == 0)
            {
                SpawnCollumn(Y, i, true);
            }
            else
            {
                SpawnCollumn(Y, i, false);
            }
        }
    }

    private void SpawnCollumn(int iCollumnLength, int iRow, bool iEven)
    {
        float x = iRow * yOffset;

        GameObject collumn = GameObject.Instantiate(collumnHolder, new Vector3(x, 0f, 0f), Quaternion.identity);
        collumn.name = (iRow + 1).ToString() + ". Collumn";
        collumn.transform.SetParent(gridHolder.transform);


        for (int i = 0; i < iCollumnLength; i++)
        {
            float y = i + (!iEven ? 0 : xOffsetOddRow);
            GameObject go = GameObject.Instantiate(hexPrefab, new Vector3(x, y, 0.0f), Quaternion.Euler(-90, 0, 0));
            go.transform.SetParent(collumn.transform);
            Debug.Log(data.mapTypes[iRow, i]);
            go.GetComponent<Hexagon>().Initialize(data.mapTypes[iRow, i], data.mapKingdoms[iRow, i], iRow, i);
        }
    }

    public void SaveGrid()
    {
        for (int i = 0; i < grid.transform.childCount; i++)
        {
            var column = grid.transform.GetChild(i);
            for (int j = 0; j < column.transform.childCount; j++)
            {
                var hex = column.transform.GetChild(j).GetComponent<Hexagon>();
                data.mapTypes[i, j] = hex.HexType;
                data.mapKingdoms[i, j] = hex.CurrentKingdomId;
            }
        }
        Debug.Log(data.mapTypes);
        Debug.Log(data.mapKingdoms);
    }

    public void UpdateKingdomsId() 
    {
        for (int i = 0; i < grid.transform.childCount; i++)
        {
            var column = grid.transform.GetChild(i);
            for (int j = 0; j < column.transform.childCount; j++)
            {
                var hex = column.transform.GetChild(j).GetComponent<Hexagon>();
                Debug.Log(data.mapKingdoms[i, j].ToString());
                hex.setKID(data.mapKingdoms[i, j]);
            }
        }
    }

    public void DebugMapKingdomsIDs()
    {
        string s = "\n";
        for (int i = 0; i < Constants.gridSizeX; i++)
        {
            for (int j = 0; j < Constants.gridSizeY; j++)
            {
                s += data.mapKingdoms[i, j].ToString() + " ";
            }
            s += '\n';
        }
        Debug.Log(s);
    }

    public void LoadGrid() 
    {
        GenerateGrid();
    }

    private void UpdateBorders() 
    {
        for (int i = 0; i < grid.transform.childCount; i++)
        {
            var column = grid.transform.GetChild(i);
            for (int j = 0; j < column.transform.childCount; j++)
            {
                var hex = column.transform.GetChild(j).GetComponent<Hexagon>();
                hex.UpdateBorders();
            }
        }
    }
}
