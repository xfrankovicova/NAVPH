//using UnityEngine;
//using System.Collections;
//using UnityEngine.EventSystems;

//public class ClickableTile : MonoBehaviour {

//	public int tileX;
//	public int tileY;
//	public TileMap map;

//	void OnMouseUp() {
//		Debug.Log ("Click!");

//		if(EventSystem.current.IsPointerOverGameObject())
//			return;

//		Debug.Log("Generating!");
//		map.GeneratePathTo(tileX, tileY);
//	}

//}
