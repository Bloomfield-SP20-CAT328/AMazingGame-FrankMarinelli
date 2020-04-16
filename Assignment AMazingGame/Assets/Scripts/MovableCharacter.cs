using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovableCharacter : MonoBehaviour
{

	//This should work out of the gate.
    //If not... then I suck.

	public float moveSpeed = 3.0f;
	protected Color baseColor = Color.blue;
	protected string title = "Movable Player";
	protected string tag = "Untagged";
	protected Point targetLocation = new Point(0, 0);
	protected Direction direction = Direction.None;

    // Start is called before the first frame update
    void Start()
    {
		gameObject.name = title;
		gameObject.tag = tag;
		gameObject.GetComponent<Renderer>().material.color = baseColor;
    }

    protected void MoveCharacter()
	{
        if(direction != Direction.None)
		{
			Vector3 target = new Vector3(targetLocation.x, transform.position.y, targetLocation.y);
            if(transform.position != target)
			{
				transform.position = Vector3.MoveTowards(transform.position, target, moveSpeed * Time.deltaTime);
			} else
			{
				direction = Direction.None;
			}
		}
	}

    protected bool ChangeDirectionIfOpen(Direction whichDirection)
	{
		bool result = false;
		Point offset = Movement.Offsets[(int)whichDirection];
		Point currentPosition = new Point((int)gameObject.transform.position.x, (int)gameObject.transform.position.z);
		Point checkPoint = currentPosition + offset;
		if (GameController.Instance.maze.IsOpen(checkPoint))
		{
			result = true;
			targetLocation = new Point(checkPoint.x, checkPoint.y);
			direction = whichDirection;
		}

		return result;
	}

    // Update is called once per frame
    void Update()
    {
        
    }
}
