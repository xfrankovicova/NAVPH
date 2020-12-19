using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class BattleController : MonoBehaviour
{
	private static BattleController _instance;
	public static BattleController Instance
	{
		get
		{
			return _instance;
		}
	}

	[SerializeField]
	private GameObject gridHolder;

	public BattleHexagon[,] Grid => grid;

    public GameObject SelectedUnit { get => selectedUnit; set => selectedUnit = value; }

    private BattleHexagon[,] grid = new BattleHexagon[Constants.battleMapSizeX, Constants.battleMapSizeY];


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
		gridHolder = GameObject.FindGameObjectWithTag("Main battle grid");
		for (int i = 0; i < gridHolder.transform.childCount; i++)
		{
			var column = gridHolder.transform.GetChild(i);
			for (int j = 0; j < column.transform.childCount; j++)
			{
				grid[i, j] = column.transform.GetChild(j).GetComponent<BattleHexagon>();
			}
		}

		GeneratePathfindingGraph();
	}

	public BattleHexagon GetRandomHex()
    {
		int x = UnityEngine.Random.Range(0, Constants.battleMapSizeX);
		int y = UnityEngine.Random.Range(0, Constants.battleMapSizeY);

		return grid[x, y];
	}

	
		#region Pathfinding

		BattleNode[,] graph;

		void GeneratePathfindingGraph()
		{
			// Initialize the array
			graph = new BattleNode[Constants.battleMapSizeX, Constants.battleMapSizeY];

			// Initialize a BattleNode for each spot in the array
			for (int x = 0; x < Constants.battleMapSizeX; x++)
			{
				for (int y = 0; y < Constants.battleMapSizeY; y++)
				{
					graph[x, y] = new BattleNode();
					graph[x, y].x = x;
					graph[x, y].y = y;
				}
			}

			// Now that all the nodes exist, calculate their neighbours
			for (int x = 0; x < Constants.battleMapSizeX; x++)
			{
				for (int y = 0; y < Constants.battleMapSizeY; y++)
				{
					var v = graph[x, y].GetNeighboursIndexes();
					foreach (var item in v)
						graph[x, y].neighbours.Add(graph[item.Item1, item.Item2]);
				}
			}
		}

		[SerializeField]
		private GameObject selectedUnit;
		public bool UnitCanEnterTile(int x, int y)
		{

			// We could test the unit's walk/hover/fly type against various
			// terrain flags here to see if they are allowed to enter the tile.
			if (grid[x, y].HasUnit())
				return false;
			return (grid[x, y].HexType != BattleHextypes.Water);
		}

		public float CostToEnterTile(int sourceX, int sourceY, int targetX, int targetY)
		{

			//TileType tt = tileTypes[tiles[targetX, targetY]];

			if (UnitCanEnterTile(targetX, targetY) == false)
				return Mathf.Infinity;

			float cost = Constants.costToEnterBattle[grid[targetX, targetY].HexType]; // tt.movementCost;

			if (sourceX != targetX && sourceY != targetY)
			{
				// We are moving diagonally!  Fudge the cost for tie-breaking
				// Purely a cosmetic thing!
				cost += 0.001f;
			}

			return cost;

		}

		public void GeneratePathTo(int x, int y)
		{
			// Clear out our unit's old path.
			selectedUnit.GetComponent<BattleUnit>().currentPath = null;

			if (UnitCanEnterTile(x, y) == false)
			{
				// We probably clicked on a mountain or something, so just quit out.
				return;
			}

			Dictionary<BattleNode, float> dist = new Dictionary<BattleNode, float>();
			Dictionary<BattleNode, BattleNode> prev = new Dictionary<BattleNode, BattleNode>();

			// Setup the "Q" -- the list of nodes we haven't checked yet.
			List<BattleNode> unvisited = new List<BattleNode>();

			BattleNode source = graph[
								selectedUnit.GetComponent<BattleUnit>().tileX,
								selectedUnit.GetComponent<BattleUnit>().tileY
								];

			BattleNode target = graph[
								x,
								y
								];

			dist[source] = 0;
			prev[source] = null;

			// Initialize everything to have INFINITY distance, since
			// we don't know any better right now. Also, it's possible
			// that some nodes CAN'T be reached from the source,
			// which would make INFINITY a reasonable value
			foreach (BattleNode v in graph)
			{
				if (v != source)
				{
					dist[v] = Mathf.Infinity;
					prev[v] = null;
				}

				unvisited.Add(v);
			}

			while (unvisited.Count > 0)
			{
				// "u" is going to be the unvisited BattleNode with the smallest distance.
				BattleNode u = null;

				foreach (BattleNode possibleU in unvisited)
				{
					if (u == null || dist[possibleU] < dist[u])
					{
						u = possibleU;
					}
				}

				if (u == target)
				{
					break;  // Exit the while loop!
				}

				unvisited.Remove(u);

				foreach (BattleNode v in u.neighbours)
				{
					//float alt = dist[u] + u.DistanceTo(v);
					float alt = dist[u] + CostToEnterTile(u.x, u.y, v.x, v.y);
					if (alt < dist[v])
					{
						dist[v] = alt;
						prev[v] = u;
					}
				}
			}

			// If we get there, the either we found the shortest route
			// to our target, or there is no route at ALL to our target.

			if (prev[target] == null)
			{
				// No route between our target and the source
				return;
			}

			List<BattleNode> currentPath = new List<BattleNode>();

			BattleNode curr = target;

			// Step through the "prev" chain and add it to our path
			while (curr != null)
			{
				currentPath.Add(curr);
				curr = prev[curr];
			}

			// Right now, currentPath describes a route from out target to our source
			// So we need to invert it!

			currentPath.Reverse();

			selectedUnit.GetComponent<BattleUnit>().currentPath = currentPath;
		}

		public Vector3 TileCoordToWorldCoord(int x, int y)
		{
			return new Vector3(grid[x, y].gameObject.transform.position.x, grid[x, y].gameObject.transform.position.y + 0.2f, grid[x, y].gameObject.transform.position.z);
		}

		#endregion
}
