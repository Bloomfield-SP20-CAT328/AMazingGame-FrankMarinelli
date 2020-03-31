using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeGen
{

	struct Point
	{
		public int x, y;
		public Point(int ptX, int ptY)
		{
			x = ptX;
			y = ptY;
		}
	}

	private byte[,] maze;

	private int width = 0;
	public int Width { get { return width; } }

	private int height = 0;
	public int Height { get { return height; } }

	private float complexity = 0.75f;
	public float Complexity { get { return complexity; } }

	private float density = 0.75f;
	public float Density { get { return density; } }

	public MazeGen() { }

	public void GenerateMaze(int mazeWidth, int mazeHeight)
	{
		GenerateMaze(mazeWidth, mazeHeight, complexity, density);

	}

	public void GenerateMaze(int mazeWidth, int mazeHeight, float mazeComplexity, float mazeDensity)
	{
		complexity = mazeComplexity;
		density = mazeDensity;
		width = (int)(mazeWidth / 2) * 2 + 1;
		height = (int)(mazeHeight / 2) * 2 + 1;
		complexity = (int)(complexity * 5 * (width + height));
		density = (float)Mathf.Floor(density * (int)(height / 2) * (int)(width / 2));
		maze = new byte[width, height];
		for (int y = 0; y < height; y++)
		{
			for (int x = 0; x < width; x++)
			{
				// If the X is the left or right most, or Y is the top or bottom value, make it a wall
				if ((y == 0) || (y == (height - 1)) || (x == 0) || (x == (width - 1)))
				{
					maze[x, y] = 1;
				}
				else
				{
					maze[x, y] = 0;
				}
			}
		}

        for (int i = 0; i < density; i++)
		{
			var x = (int)(Random.Range(0, (int)width / 2) * 2);
			var y = (int)(Random.Range(0, (int)width / 2) * 2);
			maze[x, y] = 1;
            for(var j = 0; j<complexity; j++)
			{
				Debug.Log("I'm going to go for: " + x + "," + y);
				List<Point> neighbors = new List<Point>();
				if (x > 1)
				{
					neighbors.Add(new Point(x - 2, y));
				}
				if (x < width - 2)
				{
					neighbors.Add(new Point(x + 2, y));
				}
				if (y > 1)
				{
					neighbors.Add(new Point(x, y - 2));
				}
				if (y < height - 2)
				{
					neighbors.Add(new Point(x, y + 2));
				}

				if (neighbors.Count > 0)
				{
					Point newLocation = neighbors[Random.Range(0, neighbors.Count)];
					int xx = (int)newLocation.x;
					int yy = (int)newLocation.y;

					if (maze[xx, yy] == 0)
					{
						maze[xx, yy] = 1;
						maze[(int)(xx + (x - xx) / 2), (int)(yy + (y - yy) / 2)] = 1;
						x = xx;
						y = yy;
					}
				}
				else
				{
					break;
				}

				Debug.Log(this.ToString());
			}
		}

		Debug.Log("Done!" + this.ToString());
	}

	public override string ToString()
	{
		string result = "";
		result = "Size: " + width + "," + height + " Complexity: " + complexity + "  Density: " + density + "\n";
		if (width > 0 && height > 0)
		{
			for (int y = 0; y < height; y++)
			{
				for (int x = 0; x < width; x++)
				{
					
					result += maze[x, y];
				}
				result += "\n";
			}
		}
		else
		{
			result = "A maze could not be made. Please try again.";
		}

		return result;
	}

	public bool IsWall(int x, int y)
	{
		if (x < 0 || x > width) { Debug.LogError("This X value is not valid, only 0 to width (" + width + ") is acceptable!"); }
		if (y < 0 || y > height) { Debug.LogError("This Y value is not valid, only 0 to height (" + height + ") is acceptable!"); }

		return (maze[x, y] == 0) ? false : true;
	}
}

