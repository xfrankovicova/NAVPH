﻿using UnityEngine;
using System.Collections.Generic;

public class Unit : MonoBehaviour {

	// tileX and tileY represent the correct map-tile position
	// for this piece.  Note that this doesn't necessarily mean
	// the world-space coordinates, because our map might be scaled
	// or offset or something of that nature.  Also, during movement
	// animations, we are going to be somewhere in between tiles.
	public int tileX;
	public int tileY;

//	public TileMap map;

	// Our pathfinding info.  Null if we have no destination ordered.
	public List<Node> currentPath = null;

	// How far this unit can move in one turn. Note that some tiles cost extra.
	int moveSpeed = 2;
	float remainingMovement=10000;


    void Update() {
		// Draw our debug line showing the pathfinding!
		// NOTE: This won't appear in the actual game view.
		if(currentPath != null) {
			int currNode = 0;

			while( currNode < currentPath.Count-1 ) {

				Vector3 start = GridController.Instance.TileCoordToWorldCoord( currentPath[currNode].x, currentPath[currNode].y ) + 
					new Vector3(0, 0, -0.5f) ;
				Vector3 end   = GridController.Instance.TileCoordToWorldCoord( currentPath[currNode+1].x, currentPath[currNode+1].y )  + 
					new Vector3(0, 0, -0.5f) ;

				Debug.DrawLine(start, end, Color.red);

				currNode++;
			}
		}

		// Have we moved our visible piece close enough to the target tile that we can
		// advance to the next step in our pathfinding?
		if(Vector3.Distance(transform.position, GridController.Instance.TileCoordToWorldCoord( tileX, tileY )) < 0.1f)
			AdvancePathing();

		// Smoothly animate towards the correct map tile.
		transform.position = Vector3.Lerp(transform.position, GridController.Instance.TileCoordToWorldCoord( tileX, tileY ), 5f * Time.deltaTime);
	}

	// Advances our pathfinding progress by one tile.
	void AdvancePathing() {
		if(currentPath==null)
			return;

		if(remainingMovement <= 0)
			return;

		// Teleport us to our correct "current" position, in case we
		// haven't finished the animation yet.
		transform.position = GridController.Instance.TileCoordToWorldCoord( tileX, tileY );

		// Get cost from current tile to next tile
		remainingMovement -= GridController.Instance.CostToEnterTile(currentPath[0].x, currentPath[0].y, currentPath[1].x, currentPath[1].y );
		
		// Move us to the next tile in the sequence
		tileX = currentPath[1].x;
		tileY = currentPath[1].y;
		
		// Remove the old "current" tile from the pathfinding list
		currentPath.RemoveAt(0);
		
		if(currentPath.Count == 1) {
			// We only have one tile left in the path, and that tile MUST be our ultimate
			// destination -- and we are standing on it!
			// So let's just clear our pathfinding info.
			currentPath = null;
		}
	}

	// The "Next Turn" button calls this.
	public void NextTurn() {
		// Make sure to wrap-up any outstanding movement left over.
		while(currentPath!=null && remainingMovement > 0) {
			AdvancePathing();
		}

		// Reset our available movement points.
		remainingMovement = moveSpeed;
	}
}
