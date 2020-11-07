using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class NewBehaviourScript : MonoBehaviour
{
    [SerializeField] private GameObject hexPrefab;
    [SerializeField] private GameObject CollumnHolder;

    [SerializeField] private float yOffset = 0.867f;

    [SerializeField] private float xOffsetOddRow = 0.5f;

    [SerializeField] private int magnitude = 7;


    // Start is called before the first sframe update
    void Awake()
    {
        GenerateXYHexMap(5, 3);
    }

    private void GenerateXYHexMap(int X, int Y) 
    {
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

    private void GenerateLargeHex() 
    {
           for (int i = 0; i < magnitude; i++)
            {
                if ((i % 2) == 0)
                {
                    spawnEvenRow(magnitude + i, (0 - i) / 2, i, true);
                }
                else
                {
                    spawnEvenRow(magnitude + i, ((0 - i) / 2) - 1, i, false);
                }
            }

            for (int i = magnitude - 2; i >= 0; i--)
            {
                if ((i % 2) == 0)
                {
                    spawnEvenRow(magnitude + i, (0 - i) / 2, (magnitude * 2 - 1) - i - 1, true);
                }
                else
                {
                    spawnEvenRow(magnitude + i, ((0 - i) / 2) - 1, (magnitude * 2 - 1) - i - 1, false);
                }
            }
    }

    private void SpawnCollumn(int iCollumnLength, int iRow, bool iEven) 
    {
        float x = iRow * yOffset;

        GameObject collumn = GameObject.Instantiate(CollumnHolder, new Vector3(x, 0f, 0f), Quaternion.identity);
        collumn.name = (iRow + 1).ToString() + ". Collumn";

        for (int i = 0; i < iCollumnLength; i++)
        {
            float y = i + (iEven ? 0 : xOffsetOddRow);
            GameObject go = GameObject.Instantiate(hexPrefab, new Vector3(x, y, 0.0f), Quaternion.identity);
            go.transform.SetParent(collumn.transform);
            collumn.name = (i + 1).ToString() + ". Hex";

            //      go.transform.rotation = new Quaternion(0.0f, 0.0f, 5.0f, 1.0f);
        }
    }


    private void spawnEvenRow(int iRowLength, int iRowBeggining, int iYaxis, bool iEven) 
    {
        for (int i = 0; i < iRowLength; i++)
        {
            float x = iRowBeggining + i + (iEven ? 0 : xOffsetOddRow);
            float y = iYaxis * yOffset;
            GameObject go = GameObject.Instantiate(hexPrefab, new Vector3(x, y, 0.0f), Quaternion.identity);
        }
    }
}


