using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Kingdom
{
    [SerializeField]
    public KingdomData data;
    [SerializeField]
    private int kingdomId;
    public int KingdomId => kingdomId;
    public int KingdomSize => data.hexes.Count;

    public Kingdom overlord = null;

    private string Title()
    {
        string title = "";
        if (overlord == null)
        {
            title = "Duchy";
        }
        else
        {
            if (overlord.overlord == null)
            {
                title = "County";
            }
            else
            {
                title = "Baronny";
            }
        }
        return title;
    }

    public string FullName => Title() + " of " + data.kingdomName;

    public Kingdom(int iKingdomId)
    {
        kingdomId = iKingdomId;
        data = Resources.Load<KingdomData>("Kingdoms/kingdom_" + kingdomId.ToString());
        data.hexes = new List<Hexagon>();

        Debug.Log("Kingdom #" + kingdomId + " hexes ");

        foreach (var item in data.hexes)
        {
            Debug.Log("Kingdom #" + kingdomId + ". " + item.name);
        }
        data.borderMat = Resources.Load<Material>("Materials/mat" + kingdomId.ToString());
    }

    public void AddHex(Hexagon hex)
    {
        if (!data.hexes.Contains(hex))
        {
            data.hexes.Add(hex);
        }
    }

    public void UpdateBorder()
    {
        foreach (var hex in data.hexes)
        {
            hex.SetBorderMat(data.borderMat);
        }
    }

    public void LoadData() 
    {
        if (data.overLordId != 0)
        {
            var v = KingdomController.Instance.getKingdom(data.overLordId);
            if (v.data.id != data.id)
            {
                overlord = v;
            }
        }
        GetFurhermost();
    }

    public void GetFurhermost() 
    {
        int xMax = -1;
        int yMax = -1;
        int xMin = Constants.gridSizeX + 1;
        int yMin = Constants.gridSizeY + 1;
        foreach (var item in data.hexes)
        {
            if (item.X > xMax) 
            {
                xMax = item.X;
                yMax = item.Y;
            }
            if (item.X < xMin) 
            {
                xMin = item.X;
                yMin = item.Y;
            }
        }

        var start = GetHex(xMax, yMax);
        var end = GetHex(xMin, yMin);
    //    DrawLine(start.transform.position + new Vector3(0, 0.5f, 0), end.transform.position + new Vector3(0, 0.5f, 0));
        Vector3 v = (start.transform.position + end.transform.position) / 2 + new Vector3(0, 0.5f, 0);
        Vector3 targetDir = end.transform.position - start.transform.position;
        float angle = Vector3.Angle(targetDir, start.transform.forward);
        Debug.Log(FullName + " " + angle.ToString());
        KingdomController.Instance.CreateKingdomNameUI(v, FullName, angle - 90f);
    }

    public void DrawLine(Vector3 start, Vector3 end)
    {
        Debug.Log(start + " " + end);
        //For creating line renderer object
        var lineRenderer = new GameObject("Line").AddComponent<LineRenderer>();
        lineRenderer.startColor = data.borderMat.color;
        lineRenderer.endColor = data.borderMat.color;
        lineRenderer.startWidth = 0.01f;
        lineRenderer.endWidth = 0.01f;
        lineRenderer.positionCount = 2;
        lineRenderer.useWorldSpace = true;

        //For drawing line in the world space, provide the x,y,z values
        lineRenderer.SetPosition(0, start); //x,y and z position of the starting point of the line
        lineRenderer.SetPosition(1, end); //x,y and z position of the starting point of the line
    }

    public Hexagon GetHex(int x, int y) {
        foreach (var item in data.hexes)
        {
            if (item.X == x && item.Y == y)
                return item;
        }
        Debug.LogError("Kingdom: " + data.id + ", has no hex [" + x + "," + y + "]");
        return null;
    }
}
