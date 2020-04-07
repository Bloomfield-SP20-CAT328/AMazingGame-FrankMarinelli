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

		public override string ToString()
		{
			return "(" + x + "," + y + ")";
		}
	}

	//Under the advice of TJ and Prof. Pollati, I ditched the old buggy, crappy script and tried something different.
	//This is the backtracking generator maze with no recursion. I looked at Pollati's code to help me out with some of this stuff.
	//Theres a good chance something is broke, because... well.... bugbugbugbugbugbugbugbugbug. I'm also not the best coder. That's why the world has Carlos.
	//This will most likely stay recursion free... unless I don't like the results of doing the Backtracking w/o recursion way. However, as long as its different from my old crappy code. Ill be happy.
	//Some values have changed. I hope this didn't brake anything.
	private byte[,] maze;

	private int width = 0;
    private int Width { get { return width; } }
	private int height = 0;
    private int Height { get { return height; } }


	private List<Point> track = new List<Point>();

	public MazeGen() { }
	

	public void GenerateMaze(int mazeWidth, int mazeHeight)
	{
		width = (int)(mazeWidth / 2) * 2 + 1;
		height = (int)(mazeHeight / 2) * 2 + 1;
		//This code is likely to change. It also gives me nightmares, cuz its probably what broke my previous project

		maze = new byte[width.height];

        for(int j=0; j<height; j++)
		{
            for(int i=0; i<width; i++)
			{
				maze[i, j] = 1;
			}
		}

		//this code also brings nightmares. It does not spark joy. I did not feel joy typing and fiddling with this code.
		int x = (int)(Random.Range(2, width / 2) * 2);
		int y = (int)(Random.Range(2, height / 2) * 2);
		//Hey guys, look! I stopped being a moron for 5 minutes and actually typed height instead of width again! this alone is an A!
		track.Add(new Point(x + 1, y + 1));
		maze[x, y] = 0;

		while (track.Count > 0)
		{
			//Not this again... How much nightmare inducing code goes into this???
			int lastTrackIndex = track.Count - 1;
			Point check = track[lastTrackIndex];
			x = check.x;
			y = check.y;
			List<Point> neighbors = new List<Point>();
			if (x > 2 && maze[x - 2, y] == 1)
			{ 
				neighbors.Add(new Point(x - 2, y));
			}
			if (x < width - 2 && maze[x + 2, y] == 1)
			{ 
				neighbors.Add(new Point(x + 2, y));
			}
			if (y > 2 && maze[x, y - 2] == 1)
			{ 
				neighbors.Add(new Point(x, y - 2));
			}
			if (y < height - 2 && maze[x, y + 2] == 1)
			{ 
				neighbors.Add(new Point(x, y + 2));
			}

            if(neighbors.Count = 0)
			{
				track.RemoveAt(lastTrackIndex);
			} else
			{
				Point newCheck = neighbors[Random.Range(0, neighbors.Count)];
				int xx = newCheck.x;
				int yy = newCheck.y;
				maze[xx, yy] = 0;
				maze[(int)(xx + (x - xx) / 2), (int)(yy + (y - yy) / 2)] = 0;
				track.Add(newCheck);
			}
			Debug.Log("I'm going through...\n" + this.ToString());
		}
		Debug.Log("Finished!\n" + this.ToString());
	}

    public override string ToString()
	{
		string result = "";
		result = "Size: " + width + "," + height + "\n" + "Backtrack: ";
        foreach(Point pt in track)
		{
			result += pt.ToString();
		}
		result += "\n\n";
        if(width>0 && height > 0)
		{
            for(int y = 0; y<height; y++)
			{
                for(int x = 0; x<width; x++)
				{
					result += maze[x, y];
				}
				result += "\n";
			}
			
		} else
		{
			result = "No Maze Made. Please press the Regenerate button on screen.";
		}
		return result;
	}

    public bool IsAWall(int x, int y)
	{
		if (x < 0 || x > width) { Debug.LogError("Could not generate because the X value is not valid. only 0 to width (" + width + ") is permitted."); }
		if (y < 0 || y > height) { Debug.LogError("Could not generate because the Y value is not valid. only 0 to height (" + height + ") is permitted"); }

		return (maze[x, y] == 0) ? false : true;
	}
}
