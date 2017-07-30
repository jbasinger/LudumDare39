using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random = UnityEngine.Random;
using System.Xml.Linq;
using UnityEngine.UI;

public enum Direction{
	UP,
	DOWN,
	LEFT,
	RIGHT

}

public static class DirectionMethods{
	public static Direction RandomDirection(){
		Direction[] directions = new Direction[]{ Direction.UP, Direction.DOWN, Direction.LEFT, Direction.RIGHT };
		return directions [Random.Range (0, directions.Length)];
	}
	public static Direction OppositeDirection(Direction d){
		switch (d) {
		case Direction.UP:
			return Direction.DOWN;
		case Direction.DOWN:
			return Direction.UP;
		case Direction.LEFT:
			return Direction.RIGHT;
		case Direction.RIGHT:
			return Direction.LEFT;
		default:
			return Direction.UP;
		}
	}
}

public interface IRoomGenerator{
	Room GenerateRoom (Level level, int startingX, int startingY, int roomWidth, int roomHeight);
}

public class Level{

	public Rect rect;

	public int levelNumber;
	public Tile[,] tiles;
	public List<Room> rooms;
	public GameObject gameObject;
	public Room BackgroundRoom;

	public Level(int width, int height){
		rect = new Rect (0, 0, width, height);
		rooms = new List<Room> ();
		tiles = new Tile[width, height];
	}

	public Room RandomRoom(){
		if (rooms.Count == 0) {
			return null;
		}
		return rooms [Random.Range (0, rooms.Count)];
	}

	public Tile TileAtLocation(int x, int y){

		if (x < 0 || y < 0 || x >= rect.width || y >= rect.height) {
			return null;
		}

		return tiles [x, y];

	}

	public Tile TileAtLocation(Vector3 v){
		return TileAtLocation(Mathf.RoundToInt(v.x),Mathf.RoundToInt(v.y));
	}

	public Room RoomAtLocation (int x, int y){

		return TileAtLocation (x, y).room;

	}

	public bool IsEdgeOfMap(int x, int y){
		return x <= 0 || y <= 0 || x >= rect.width-1 || y >= rect.height-1;
	}

	public List<Tile> GetEightAdjacentTilesFromPosition(int x,int y){
		
		List<Tile> adjacent = new List<Tile> ();

		Tile bl = TileAtLocation (x - 1, y - 1);
		Tile b  = TileAtLocation (x    , y - 1);
		Tile br = TileAtLocation (x + 1, y - 1);
		Tile l  = TileAtLocation (x - 1, y    );
		Tile r  = TileAtLocation (x + 1, y    );
		Tile tl = TileAtLocation (x - 1, y + 1);
		Tile t  = TileAtLocation (x    , y + 1);
		Tile tr = TileAtLocation (x + 1, y + 1);

		if (bl != null && !IsEdgeOfMap (bl.x, bl.y)) {
			adjacent.Add (bl);
		}
		if (b != null && !IsEdgeOfMap (b.x, b.y)) {
			adjacent.Add (b);
		}
		if (br != null && !IsEdgeOfMap (br.x, br.y)) {
			adjacent.Add (br);
		}
		if (l != null && !IsEdgeOfMap (l.x, l.y)) {
			adjacent.Add (l);
		}
		if (r != null && !IsEdgeOfMap (r.x, r.y)) {
			adjacent.Add (r);
		}
		if (tl != null && !IsEdgeOfMap (tl.x, tl.y)) {
			adjacent.Add (tl);
		}
		if (t != null && !IsEdgeOfMap (t.x, t.y)) {
			adjacent.Add (t);
		}
		if (tr != null && !IsEdgeOfMap (tr.x, tr.y)) {
			adjacent.Add (tr);
		}

		return adjacent;
	}

}

public class Room{
	
	public List<Tile> tiles;
	public List<Tile> exits;
	public GameObject gameObject;

	public Level level;

	public Room(Level level, string name){
		this.tiles = new List<Tile> ();
		this.exits = new List<Tile> ();
		this.level = level;
		gameObject = new GameObject (name);
		gameObject.transform.parent = level.gameObject.transform;
	}

	public Tile TileAtPosition(int x, int y){
		foreach (Tile t in tiles) {
			if (t.x == x && t.y == y) {
				return t;
			}
		}
		return null;
	}

	public Tile RandomTile(){
		if (tiles.Count == 0) {
			return null;
		}
		return tiles [Random.Range (0, tiles.Count)];
	}

	public Tile RandomEdgeTile(){

		Tile t = RandomTile ();
		Direction dir = DirectionMethods.RandomDirection ();

		return EdgeTileFromPositionInDirection (t.x, t.y, dir);
	}

	Tile EdgeTileFromPositionInDirection(int x, int y, Direction dir){

		Tile t = level.TileAtLocation (x,y);

		Tile up = level.TileAtLocation (t.x, t.y + 1);
		Tile down = level.TileAtLocation (t.x, t.y - 1);
		Tile left = level.TileAtLocation (t.x-1, t.y);
		Tile right = level.TileAtLocation (t.x+1, t.y);

		if (!up.isPassable || !down.isPassable || !left.isPassable || !right.isPassable) {
			return t;
		}

		int dx = 0, dy = 0;

		switch (dir) {
		case Direction.UP:
			dy = 1;
			break;
		case Direction.DOWN:
			dy = -1;
			break;
		case Direction.LEFT:
			dx = -1;
			break;
		case Direction.RIGHT:
			dx = 1;
			break;
		}

		return EdgeTileFromPositionInDirection (x + dx, y + dy, dir);

	}

}

public class Tile{
	
	public GameObject gameObject;
	public bool isPassable;
	public int x;
	public int y;
	public Room room;

	public Tile(int x,int y,Room room){
		this.x = x;
		this.y = y;
		this.room = room;
	}

	public Vector3 GetVector3(){
		return new Vector3(x,y);
	}
}

public class EnvironmentBuilder : MonoBehaviour {

	public int levelHeight;
	public int levelWidth;

	public int maxRoomSize;
	public int minRoomSize;

	public int minNumberOfRooms;
	public int maxNumberOfRooms;

	public int minNumberOfExits;
	public int maxNumberOfExits;

	public int maxBreakablesPerRoom;

	public GameObject playerPrefab;
	public GameObject wallPrefab;
	public GameObject floorPrefab;
	public GameObject enemy;
	public GameObject stairsDown;
	public GameObject breakablePrefab;

	public Text winLoseLabel;
	public Image playerLifeBar;
	public Text levelLabel;

	public static Level level;

	private GameObject currentStairs;
	private GameObject player;

	public void NextLevel(){
		BuildEnvironment(level.levelNumber - 1);
	}

	// Use this for initialization
	void Start () {
		BuildEnvironment ();
	}

	public void NewGame(){

		ClearEnvironment ();

		if (currentStairs != null) {
			Destroy (currentStairs);
		}

		if (player != null) {
			player.GetComponent<HitPoints> ().MaxHealing ();
		}

		BuildEnvironment ();
	}

	public void ClearEnvironment(){

		if (level != null) {
			Destroy (level.gameObject);
		} 

	}

	public void BuildEnvironment(int levelNumber = 5){

		winLoseLabel.gameObject.SetActive (false);

		ClearEnvironment ();

		if (levelNumber == 0) {
			winLoseLabel.gameObject.SetActive (true);
			winLoseLabel.text = "YOU WIN!";
			levelLabel.text = "LEVEL: -";
			return;
		}

		level = new Level (levelWidth, levelHeight);
		level.levelNumber = levelNumber;

		level.gameObject = new GameObject ("Level " + levelNumber);
		level.gameObject.transform.parent = this.transform;

		BuildBackground ();
		BuildRooms ();
		BuildHallways ();
		PlaceBreakables ();
		PlaceStairsDown ();
		PlacePlayer ();

	}

	void BuildBackground(){
		Room background = new Room (level, "Background");

		level.BackgroundRoom = background;

		for (int y = 0; y < levelHeight; y++) {
			for (int x = 0; x < levelWidth; x++) {
				Tile t = new Tile (x, y, background);
				t.gameObject = Instantiate (wallPrefab, new Vector3 (x, y), Quaternion.identity, background.gameObject.transform);
				t.isPassable = false;
				t.room = background;
				level.tiles [x, y] = t;
			}
		}


	}

	void BuildRooms(){

		int roomCount = Random.Range (minNumberOfRooms, maxNumberOfRooms);
		Debug.Log ("Building " + roomCount + " rooms.");

		for (int i = 0; i < roomCount; i++) {

			int roomWidth = Random.Range (minRoomSize, maxRoomSize);
			int roomHeight = Random.Range (minRoomSize, maxRoomSize);

			int xPos = Random.Range(1,(int)level.rect.width-roomWidth);
			int yPos = Random.Range(1,(int)level.rect.height-roomHeight);

			//			Debug.Log (String.Format ("x: {0} y: {1} w: {2} h: {3}", xPos, yPos, roomWidth, roomHeight));

			Room r = new Room (level, "RandomRoom");
			level.rooms.Add (r);

			for (int y = yPos; y < yPos + roomHeight; y++) {
				for (int x = xPos; x < xPos + roomWidth; x++) {

					if (level.IsEdgeOfMap (x, y)) {
						Debug.Log ("Edge of map!");
						continue;
					}

					TurnWallToFloor (x, y, r);

				}
			}

			if (Random.value <= 0.5) {
				Tile t = r.RandomTile ();
				Instantiate (enemy, new Vector3 (t.x, t.y, 0), Quaternion.identity, r.gameObject.transform);
			}

		}

	}

	void BuildHallways(){
		
		foreach (Room r in level.rooms) {

			int exits = Random.Range (minNumberOfExits, maxNumberOfExits);
			for (int i = 0; i < exits; i++) {

				Tile t = r.RandomEdgeTile ();
				r.exits.Add (t);

				Tile up = level.TileAtLocation (t.x, t.y + 1);
				Tile down = level.TileAtLocation (t.x, t.y - 1);
				Tile left = level.TileAtLocation (t.x-1, t.y);
//				Tile right = level.TileAtLocation (t.x+1, t.y);

				Direction dir;

				if (!up.isPassable) {
					dir = Direction.UP;
				} else if (!down.isPassable) {
					dir = Direction.DOWN;
				} else if (!left.isPassable) {
					dir = Direction.LEFT;
				} else {
					dir = Direction.RIGHT;
				}

				Room hallway = new Room (level, "RandomHallway");

				Dig (hallway, t.x, t.y, dir);

			}

		}

	}

	void PlaceBreakables(){

		foreach (Room r in level.rooms) {

			int numBreakables = Random.Range (0, maxBreakablesPerRoom);
			for (int i = 0; i < numBreakables; i++) {
				Tile t = r.RandomTile ();
				Instantiate (breakablePrefab, new Vector3 (t.x, t.y), Quaternion.identity, t.gameObject.transform);
			}

		}

	}

	void PlaceStairsDown(){
		
		Room exitRoom = level.RandomRoom ();
		Tile stairsTile = exitRoom.RandomTile ();

		Destroy (stairsTile.gameObject);
		stairsTile.gameObject = Instantiate (stairsDown, new Vector3 (stairsTile.x, stairsTile.y), Quaternion.identity, this.transform);

		StairsDown stairs = stairsTile.gameObject.GetComponent<StairsDown> ();
		stairs.levelBuilder = this.gameObject;

		currentStairs = stairs.gameObject;

	}

	void PlacePlayer(){
		Room startingRoom = level.RandomRoom ();
		Tile startingTile;

		do {
			startingTile = startingRoom.RandomTile ();
		} while(!startingTile.isPassable);

		if (player == null) {
			player = Instantiate (playerPrefab, new Vector3 (startingTile.x, startingTile.y), Quaternion.identity);
			LifeBar lb = player.GetComponent<LifeBar> ();
			lb.bar = playerLifeBar;

			Camera.main.GetComponent<CameraFollow> ().target = player;
		} else {
			player.transform.position = new Vector3 (startingTile.x, startingTile.y);
		}
	}

	void Dig(Room hallway, int x, int y, Direction dir){

		int dx = 0, dy = 0;

		switch (dir) {
		case Direction.UP:
			dy = 1;
			break;
		case Direction.DOWN:
			dy = -1;
			break;
		case Direction.LEFT:
			dx = -1;
			break;
		case Direction.RIGHT:
			dx = 1;
			break;
		}

		if (level.IsEdgeOfMap (x + dx, y + dy)) {
			Direction newDir;
			do {
				newDir = DirectionMethods.RandomDirection ();
			} while(newDir == dir || newDir == DirectionMethods.OppositeDirection(dir));
			Dig (hallway, x, y, newDir);
			return;
		}

		Tile nextTile = level.tiles [x + dx, y + dy];

		if (nextTile.isPassable) {
			return;
		}

		TurnWallToFloor (nextTile.x, nextTile.y, hallway);
		Dig (hallway, nextTile.x, nextTile.y, dir);
	}

	void TurnWallToFloor(int x, int y, Room r){
		
		Tile t = level.TileAtLocation (x, y);
		level.BackgroundRoom.tiles.Remove (t);
		t.isPassable = true;
		Destroy (t.gameObject);
		t.gameObject = Instantiate (floorPrefab, new Vector3 (x, y), Quaternion.identity, r.gameObject.transform);
		t.room = r;
		level.tiles [x, y] = t;
		r.tiles.Add (t);
	}

	void Update(){
		if (player == null) {
			winLoseLabel.gameObject.SetActive (true);
			winLoseLabel.text = "YOU LOSE!";
			winLoseLabel.color = Color.red;
		}
		levelLabel.text = "LEVEL: " + level.levelNumber;
	}

}
