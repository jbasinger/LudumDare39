using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml.Linq;
using System;
using Random = UnityEngine.Random;



public class HallwayGenerator : MonoBehaviour {

	public GameObject floorPrefab;
	public GameObject wallPrefab;

	public Room GenerateHallway(Level level, Tile exit){

		Room r = null;
//
//		int x, y;
//		Direction dir;
//
//		Tile up = level.TileAtLocation (exit.x, exit.y + 1);
//		Tile down = level.TileAtLocation (exit.x, exit.y - 1);
//		Tile left = level.TileAtLocation (exit.x-1, exit.y);
//		Tile right = level.TileAtLocation (exit.x+1, exit.y);
//
//		if (up != null && !level.IsEdgeOfMap (up.x, up.y)) {
//			x = up.x;
//			y = up.y;
//			dir = Direction.UP;
//		} else if (down != null && !level.IsEdgeOfMap (down.x, down.y)) {
//			x = down.x;
//			y = down.y;
//			dir = Direction.DOWN;
//		} else if (left != null && !level.IsEdgeOfMap (left.x, left.y)) {
//			x = left.x;
//			y = left.y;
//			dir = Direction.LEFT;
//		} else if (right != null && !level.IsEdgeOfMap (right.x, right.y)) {
//			x = right.x;
//			y = right.y;
//			dir = Direction.RIGHT;
//		} else {
//			return null; //Something fudged up
//		}
//
//		r = new Room (3, 1, x, y);
//		r.gameObject = new GameObject ("RandomHallway");
//		Dig (level, r, x, y, dir);
//
		return r;

	}
//
//	void Dig(Level level, Room room, int x, int y, Direction dir){
//
//		switch (dir) {
//		case Direction.UP:
//			y++;
//			break;
//		case Direction.DOWN:
//			y--;
//			break;
//		case Direction.LEFT:
//			x--;
//			break;
//		case Direction.RIGHT:
//			x++;
//			break;
//		}
//
//		Tile nextTile = level.TileAtLocation (x, y);
//		if (nextTile != null) {
//			//We hit something
//			//Eat through it
//			if (nextTile.isPassable) {
//				return;
//			}
//
//			return;
//
//		} else if (nextTile == null && level.IsEdgeOfMap (x, y)) {
//			//We hit the edge of the map
//			//Pick another direction and keep going
//			Direction newDir;
//			if (dir == Direction.UP || dir == Direction.DOWN) {
//				newDir = (new Direction[]{ Direction.LEFT, Direction.RIGHT }) [Random.Range (0, 1)];
//			} else {
//				newDir = (new Direction[]{ Direction.UP, Direction.DOWN }) [Random.Range (0, 1)];
//			}
//
//			Dig (level, room, x, y, newDir);
//		} else {
//
//			if (dir == Direction.UP || dir == Direction.DOWN) {
//
//				Tile here = new Tile (x, y);
//				Tile left = new Tile (x - 1, y);
//				Tile right = new Tile (x + 1, y);
//
//				here.gameObject = Instantiate (floorPrefab, new Vector3 (x, y, 0), Quaternion.identity, room.gameObject.transform);
//				left.gameObject = Instantiate (wallPrefab, new Vector3 (x-1, y, 0), Quaternion.identity, room.gameObject.transform);
//				right.gameObject = Instantiate (wallPrefab, new Vector3 (x+1, y, 0), Quaternion.identity, room.gameObject.transform);
//
//				here.isPassable = true;
//				left.isPassable = false;
//				right.isPassable = false;
//
//				here.room = room;
//				left.room = room;
//				right.room = room;
//
//			} else {
//				Tile here = new Tile (x, y);
//				Tile up = new Tile (x, y+1);
//				Tile down = new Tile (x, y-1);
//
//				here.gameObject = Instantiate (floorPrefab, new Vector3 (x, y, 0), Quaternion.identity, room.gameObject.transform);
//				up.gameObject = Instantiate (wallPrefab, new Vector3 (x, y+1, 0), Quaternion.identity, room.gameObject.transform);
//				down.gameObject = Instantiate (wallPrefab, new Vector3 (x, y-1, 0), Quaternion.identity, room.gameObject.transform);
//
//				here.isPassable = true;
//				up.isPassable = false;
//				down.isPassable = false;
//
//				here.room = room;
//				up.room = room;
//				down.room = room;
//			}
//
//			//Keep digging if we can
//			Dig(level,room,x,y,dir);
//		}
//
//	}

}
