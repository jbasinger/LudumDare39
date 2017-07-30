using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquareRoomGenerator : MonoBehaviour, IRoomGenerator {

	public GameObject floorPrefab;
	public GameObject wallPrefab;

	public Room GenerateRoom (Level level, int startingX, int startingY, int roomWidth, int roomHeight){

//		int height = roomHeight;
//		int width = roomWidth;
//
//		Room room = new Room (width, height, startingX, startingY);
//
//		room.gameObject = new GameObject ("TheNameOfTheRoomCanBeRandom!");
//		room.gameObject.transform.position = new Vector3 (startingX, startingY, 0);
//
//		List<Tile> walls = new List<Tile> ();
//
//		for (int y = 0; y < height; y++) {
//			
//			for (int x = 0; x < width; x++) {
//
//				Tile tile = new Tile (x + startingX, y + startingY);
//
//				if (y == 0 || y == height - 1 || x == 0 || x == width - 1) {
//
//					//Wall
//					tile.gameObject = Instantiate (wallPrefab, new Vector3 (x + startingX, y + startingY, 0), Quaternion.identity, room.gameObject.transform);
//					tile.isPassable = false;
//
//				} else {
//					
//					//Floor
//					tile.gameObject = Instantiate (floorPrefab, new Vector3 (x + startingX, y + startingY, 0), Quaternion.identity, room.gameObject.transform);				
//					tile.isPassable = true;
//
//				}
//
//				room.tiles [x, y] = tile;
//				tile.room = room;
//				if (!tile.isPassable && !(x==0 && y==0) && !(x == 0 && y == height - 1) && !(x==width-1 && y==0) && !(x==width-1 && y==height-1)) {
//					walls.Add (tile);
//				}
//
//			}
//
//		}
//
//		int maxExits;
//
//		if (height * width <= 15) {
//			maxExits = 1;
//		} else if (height * width <= 25) {
//			maxExits = 2;
//		} else {
//			maxExits = 4;
//		}
//
//		int exits = Random.Range (1, maxExits);
//
//		for (int i = 0; i < exits; i++) {
//			Tile wall = walls [Random.Range (0, walls.Count)];
//			DestroyImmediate(wall.gameObject);
//			wall.gameObject = Instantiate (floorPrefab, new Vector3 (wall.x, wall.y, 0), Quaternion.identity, room.gameObject.transform);
//			wall.isPassable = true;
//			room.tiles [wall.x-startingX, wall.y-startingY] = wall;
//			walls.Remove (wall);
//			room.exits.Add (wall);
//		}

//		return room;
		return null;

	}
}
