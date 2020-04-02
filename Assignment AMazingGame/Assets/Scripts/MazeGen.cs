using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MazeGen
{
//Ill be documenting my code from now on with this assignment. Ill be labeling concerns, stresses, anger, headaches and tears. Mainly stresses and anger.
//Documentation will tell changes I tried making with the code, if it helped or not, as well as what changing the code did for the generation.
//As a result, this script has been the largest headache to try and tackle. Based on the tinkering I did, I was able to get much faster load times, and varying generation, but also errors with each generation.
//Whatever is happening with this code to cause errors and weirdness might cause the UI portion of this Assignment(Assignment 13) to not work properly, despite being hooked up hopefully correctly.
//With that said, I expect to get ripped apart for having buggy, messy code. If that happens, Hello Class! This is Frank from 2:04pm.
	struct Point
	{
		public int x, y;
		public Point(int ptX, int ptY)
		{
			x = ptX;
			y = ptY;
		}
	}
    //Lines 23-42 should not be causing any issues.
	private byte[,] maze;

	private int width = 0;
	public int Width { get { return width; } }

	private int height = 0;
	public int Height { get { return height; } }

	private float complexity = 0.75f;
	public float Complexity { get { return complexity; } }

	private float density = 0.75f;
	public float Density { get { return density; } }

	//public GameObject mazeCubes;


	public MazeGen() { }

	public void GenerateMaze(int mazeWidth, int mazeHeight)
	{
		GenerateMaze(mazeWidth, mazeHeight, complexity, density);

	}
    //I edited some values here. Seemed to have helped with the generation. Made it more stable.
	public void GenerateMaze(int mazeWidth, int mazeHeight, float mazeComplexity, float mazeDensity)
	{
		complexity = mazeComplexity;
		density = mazeDensity;
		width = (int)(mazeWidth / 2) * 3 + 1;
		height = (int)(mazeHeight / 2) * 3 + 1;
		complexity = (int)(complexity * 3 * (width + height));
		density = (float)Mathf.Floor(density * (int)(height / 2) * (int)(width / 2));
		maze = new byte[width, height];
		for (int y = 0; y < height; y++)
		{
			for (int x = 0; x < width; x++)
			{
				
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
        //I feel like something here is giving me the most problems. Im not sure why or what. This version of the code gives on average 3 errors(no generation), 3 successes(generation).
        //The successes range from 414 messages in console, 690 messages in the console, 828 messages in the console, and 999+ messages in the console. You can tell which is which by load times.
        //414 messages takes around 2-4 seconds to load. 690 messages take around 4-6 second to load. 828 takes around 5-8 seconds to load. 999+ messages take 7-9 seconds to load. Errors load instantly. MUCH better than the 1-2 minutes it took before.
        //The error it always gives me is IndexOutOfRangeException: Index was outside the bounds of the array.
        for (int i = 0; i < density; i++)
		{
			var x = (int)(Random.Range(0, (int)width / 2) * 3);
			var y = (int)(Random.Range(0, (int)width / 2) * 3);
			maze[x, y] = 1;
            for(var j = 0; j<complexity; j++)
			{
                //I think something with these neighbors are causing severe problems. Changing their values has helped a little bit, but not by much. I tried removing it before, only to get a 100% error rate.
                //Im going to leave them in here, despite the fact that I think these are the things causing me the most serious of problems.
				Debug.Log("I'm going to go for: " + x + "," + y);
				List<Point> neighbors = new List<Point>();
				if (x > 3)
				{
					neighbors.Add(new Point(x - 2, y));
				}
				if (x < width - 4)
				{
					neighbors.Add(new Point(x + 2, y));
				}
				if (y > 3)
				{
					neighbors.Add(new Point(x, y - 2));
				}
				if (y < height - 4)
				{
					neighbors.Add(new Point(x, y + 2));
				}

				if (neighbors.Count > 1)
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
			result = "A maze could not be made. Please try again."; //Shouldn't this appear instead of an error in console?
		}

		return result;
	}

	public bool IsWall(int x, int y)
	{
		if (x < 0 || x > width) { Debug.LogError("This X value is not valid, only 0 to width (" + width + ") is acceptable!"); }
		if (y < 0 || y > height) { Debug.LogError("This Y value is not valid, only 0 to height (" + height + ") is acceptable!"); }
        //Shouldn't these two appear instead of an error in the console?
		return (maze[x, y] == 0) ? false : true;
	}
}

//Without a doubt, this has been the hardest Ive worked on trying to make something work properly. I hit so many roadblocks and I felt like I was getting absolutely nowhere.
//In the end, I was able to make tweaks to the code that have stablized it a great deal, but is no where close to where I expect it to be.
//Kinda wanted to give up sometimes. But in the end I chose to perservere, and did all I could and all I knew how to do to try and get it to a functional place. Or as functional as I could make it.
//This is not my best work. I know that. Im looking to change that, and fix that however I can, so it meets my standards.
// By the way... Build failed.