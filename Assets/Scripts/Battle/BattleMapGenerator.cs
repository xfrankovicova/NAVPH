using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class BattleMapGenerator : MonoBehaviour
{
    //[MenuItem("Custom/GenerateBattleMap")]
    //public static void OpenWindow()
    //{
    //    GetWindow<BattleMapGenerator>();
    //}

    //void OnGUI()
    //{
    //    if (GUILayout.Button("Generate battle map"))
    //    {
    //        Debug.Log("Generating battle map");
    //        GenerateBattleMap();
    //        SetMapRandom();
    //    }
    //    if (GUILayout.Button("Make map random"))
    //    {
    //        Debug.Log("Make map random");
    //        SetMapRandom();
    //    }
    //}

    private void Start()
    {
        GenerateBattleMap();
    }

    void OnEnable()
    {
        hexPrefab = Resources.Load<GameObject>("GridGenerator/BattleHex");
        gridPrefab = Resources.Load<GameObject>("GridGenerator/BattleGrid");
        collumnHolder = Resources.Load<GameObject>("GridGenerator/Collumn");

        grid = GameObject.FindGameObjectWithTag("Main battle grid");
        if (grid == null)
        {
            Debug.Log("Grid not found");
        }
        else
        {
            Debug.Log("Grid found");
        }
    }

    private GameObject grid;
    [SerializeField] private GameObject hexPrefab;
    [SerializeField] private GameObject gridPrefab;
    [SerializeField] private GameObject collumnHolder;
    private GameObject gridHolder;
    private float yOffset = 0.867f;
    private float xOffsetOddRow = 0.5f;

    private void GenerateBattleMap()
    {
        gridHolder = GameObject.Instantiate(gridPrefab, new Vector3(0f, 0f, 0f), Quaternion.identity);
        gridHolder.transform.position = new Vector3(-15, 5, 0);
        gridHolder.name = "Battle Grid";
        gridHolder.tag = "Main battle grid";
        grid = gridHolder;
        for (int i = 0; i < Constants.battleMapSizeX; i++)
        {
            if ((i % 2) == 0)
            {
                SpawnCollumn(Constants.battleMapSizeY, i, true);
            }
            else
            {
                SpawnCollumn(Constants.battleMapSizeY, i, false);
            }
        }
        gridHolder.transform.rotation = Quaternion.Euler(90, 0, 0);
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

            go.GetComponent<BattleHexagon>().Initialize(BattleHextypes.Grassland, iRow, i);
        }
    }

    public void SetMapRandom()
    {
        int maxHextype = Enum.GetValues(typeof(BattleHextypes)).Length;
        BattleHextypes type = (BattleHextypes)UnityEngine.Random.Range(0, maxHextype);

        var config = Resources.Load<BattleMapConfig>("DefaultBattleMapConfig");
        GenerateHue(config.lakes, config, BattleHextypes.Water);
        GenerateHue(config.forests, config, BattleHextypes.Forrest);
        GenerateHue(config.hills, config, BattleHextypes.Mountain_Big);
    }

    private void GenerateHue(int count, BattleMapConfig config, BattleHextypes type)
    {
        for (int i = 0; i < count; i++)
        {
            BattleHexagon hex = BattleController.Instance.GetRandomHex();

            if (hex.HexType.Equals(BattleHextypes.Grassland))
            {
                hex.SetHextype(type);
            }

            int actualProbability = config.startingProbability;

            SetHextypeToNeighbours(type, hex, actualProbability, config);

        }
    }

    private void SetHextypeToNeighbours(BattleHextypes type,BattleHexagon actualHexagon, int actualProbability, BattleMapConfig config)
    {       
        actualProbability -= config.decreasingProbability;

        if (actualProbability > 0)
        {
            var neighbours = actualHexagon.GetNeighbours();

            foreach (var n in neighbours)
            {
                if (n.HexType.Equals(BattleHextypes.Grassland))
                {
                    if (UnityEngine.Random.Range(0, 100) < actualProbability)
                    {
                        n.SetHextype(type);
                    }

                    actualHexagon = n;

                    SetHextypeToNeighbours(type, actualHexagon, actualProbability, config);
                }
            }
        }
        else
        {
            return;
        }
    }
}
