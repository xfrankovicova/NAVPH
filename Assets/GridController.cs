using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class GridController : MonoBehaviour
{
    private static GridController _instance;
    public static GridController Instance
    {
        get
        {
            return _instance;
        }
    }

    [SerializeField]
    private GameObject gridHolder;

    public Hexagon[,] Grid => grid;

    private Hexagon[,] grid = new Hexagon[Constants.gridSizeX, Constants.gridSizeY];


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

    private void Start()
    {
        gridHolder = GameObject.FindGameObjectWithTag("Main grid");
        for (int i = 0; i < gridHolder.transform.childCount; i++)
        {
            var column = gridHolder.transform.GetChild(i);
            for (int j = 0; j < column.transform.childCount; j++)
            {
                grid[i, j] = column.transform.GetChild(j).GetComponent<Hexagon>();
            }
        }
    }

    [SerializeField]
    private List<Hexagon> kingdom;
    [SerializeField]
    private Hexagon hex;

    public void GetKingdom() 
    {
         kingdom = hex.getMyKingdom();
        
    }

    public void Save() 
    {
        using (var sw = new System.IO.StreamWriter("outputText.txt"))
        {
            for (int i = 0; i < Constants.gridSizeX; i++)
            {
                for (int j = 0; j < Constants.gridSizeY; j++)
                {
                    sw.Write(grid[i, j].HexType + " ");
                }
                sw.Write("\n");
            }

            sw.Flush();
            sw.Close();
        }
    }
}
