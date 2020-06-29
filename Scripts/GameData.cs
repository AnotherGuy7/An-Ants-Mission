using Godot;
using System;

public class GameData : Node2D
{
	public static bool paused = true;
	public static bool dead = false;
	public static bool gameOver = false;
	public static bool won = false;

	public static int minutesLeft = 1;
	public static int secondsLeft = 30;

	private int counter;

	public override void _Process(float delta)
	{
		if (!paused)
		{
			counter += 2;
			if (counter >= 60)
			{
				secondsLeft--;
				counter = 0;
			}
			if (secondsLeft <= -1)
			{
				minutesLeft--;
				secondsLeft = 59;
			}
			if (minutesLeft <= 0 && secondsLeft <= 0)
			{
				dead = true;
				gameOver = true;
				minutesLeft = 0;
				secondsLeft = 0;
			}
		}
		GetTree().Paused = paused;
	}
}
