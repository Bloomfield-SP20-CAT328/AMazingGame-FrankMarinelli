using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{

	public int mazeWidth = 40;
	public int mazeHeight = 40;
	public float mazeComplexity = 0.75f;
	public float mazeDensity = 0.75f;
	public Text widthTextElement;
	public Text heightTextElement;
	public Slider complexitySliderUIElement;
	public Slider densitySliderUIElement;
	public Text complexityTextElement;
	public Text densityTextElement;
	Maze myMazeGen = new MazeGen();
	GameObject visibleMaze;

	// Start is called before the first frame update
	void Start()
	{
		UpdateDensityTextUI();
		UpdateComplexityTextUI();
		visibleMaze = new GameObject("VisibleMaze GameObject");
		GenerateMaze();
		DrawTheMaze();

	}

	public void RegenerateButton(string regenerateMaze)
	{
		SceneManager.LoadScene(regenerateMaze);
	}

	public void UpdateDensityTextUI()
	{
		densityTextElement.text = "Density: " + densitySliderUIElement.value.ToString("00.00");
	}

	public void UpdateComplexityTextUI()
	{
		complexityTextElement.text = "Complexity: " + complexitySliderUIElement.value.ToString("00.00");
	}

	public void GenerateMaze_ClickHandler()
	{
		if (widthTextElement.text == "" || heightTextElement.text == "")
		{
			return;
		}
		int width = int.Parse(widthTextElement.text);
		int height = int.Parse(heightTextElement.text);
		float complexity = complexitySliderUIElement.value;
		float density = densitySliderUIElement.value;
		mazeWidth = width;
		mazeHeight = height;
		mazeComplexity = compelxity;
		mazeDensity = density;
		ClearMaze();
		GenerateMaze();
		DrawTheMaze();
	}

	protected void GenerateMaze()
	{
		myMazeGen.GenerateMaze(mazeWidth, mazeHeight, mazeComplexity, mazeDensity);

	}

	protected void DrawTheMaze()
	{
		//Warning, the colors I chose for this portion of the code might make your eyes vomit. I take no responsibility if your eyes vomit from my colors.
		GameObject mazeFloor = GameObject.CreatePrimitive(PrimitiveType.Plane);
		mazeFloor.transform.position = new Vector3(myMazeGen.Width / 2, 0, myMazeGen.Height / -2);
		mazeFloor.transform.localScale = new Vector3(myMazeGen.Width / 10.0f, 1.0f, myMazeGen.Height / 10.0f);
		mazeFloor.transform.parent = visibleMaze.transform;
		mazeFloor.GetComponent<Renderer>().material.color = new Color(1.0f, 0.0f, 0.0f);
		for (int y = 0; y < myMazeGen.Height; y++)
		{
			for (int x = 0; x < myMazeGen.Width; x++)
			{
				if (myMazeGen.IsAWall(x, y))
				{
					GameObject mazeCube = GameObject.GetPrimitive(PrimitiveType.Cube);
					mazeCube.transform.position = new Vector3(x, 0.5f, y * -1);
					mazeCube.transform.localScale = new Vector3(1, 1, 1);
					mazeCube.transform.parent = visibleMaze.transform;
					mazeCube.GetComponent<Renderer>().material.color = new Color(1.0f, 0.0f, 1.0f);
				}
			}
		}
		float cameraY = (myMazeGen.Width < myMazeGen.Height) ? myMazeGen.Height + 2 : myMazeGen.Width + 2;
		Camera.main.transform.position = new Vector3(myMazeGen.Width / 2.0f, cameraY, -1 * myMazeGen.Height / 2.0f);
	}
	protected void ClearMaze()
	{
		Destroy(visibleMaze);
		visibleMaze = new GameObject("VisibleMaze GameObject");
	}

}
