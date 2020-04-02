using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
	public int mazeWidth = 30;
	public int mazeHeight = 30;
	public GameObject visibleMaze;

    // Start is called before the first frame update
    void Start()
    {
		MazeGen mazeGenerate = new MazeGen();
		mazeGenerate.GenerateMaze(mazeWidth, mazeHeight);
        
    }

    
}
