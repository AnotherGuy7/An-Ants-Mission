using Godot;
using System.Collections;

public class Axe : Area2D
{
	[Export]
	public bool inverted;

	public override void _PhysicsProcess(float delta)
	{
		if (!inverted)
		{
			if (RotationDegrees >= 0f && RotationDegrees <= 180f)
			{
				RotationDegrees += 6;
			}
			else
			{
				RotationDegrees += 1.5f;
			}
		}
		else
		{
			if (RotationDegrees <= -0f && RotationDegrees >= -180f)
			{
				RotationDegrees -= 6;
			}
			else
			{
				RotationDegrees -= 1.5f;
			}
		}
		if (RotationDegrees >= 360f)
		{
			RotationDegrees = 0f;
		}
		if (RotationDegrees <= -360f)
		{
			RotationDegrees = 0f;
		}
		if (GameData.won || GameData.gameOver)
		{
			QueueFree();
		}
	}

	private void OnAxeBodyEntered(object body)
	{
		if (body == Player.player)
		{
			GameData.dead = true;
		}
	}
}
