using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    //Nothing here should cause problems. Go to MazeGen.
    //I removed the int values.
	public int mazeWidth;
	public int mazeHeight;
	public GameObject visibleMaze;

	// Start is called before the first frame update
	void Start()
	{
		MazeGen mazeGenerate = new MazeGen();
		mazeGenerate.GenerateMaze(mazeWidth, mazeHeight);
		
	}

    public void RegenerateButton(string regenerateMaze)
	{
		SceneManager.LoadScene(regenerateMaze);
	}

}