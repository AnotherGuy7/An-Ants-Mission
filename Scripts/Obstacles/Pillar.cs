using Godot;
using System;

public class Pillar : StaticBody2D
{
	private Area2D hurtArea;
	private CollisionShape2D pillarShape;
	private AnimatedSprite pillarAnim;
	private Timer restTimer;

	private bool resting = true;
	private bool hitWall = false;

	public override void _Ready()
	{
		hurtArea = GetNode<Area2D>("HurtArea");
		pillarShape = GetNode<CollisionShape2D>("PillarShape");
		pillarAnim = GetNode<AnimatedSprite>("PillarAnim");
		restTimer = GetNode<Timer>("RestTimer");
	}

	public override void _Process(float delta)
	{
		hurtArea.Position = new Vector2(pillarAnim.Frame * 2.90909f, 1f);
		pillarShape.Scale = new Vector2(pillarAnim.Frame * 2.90909f, 1f);       //I got 2.90909 by doing 32/11, which is basically max size * 2 / max amount of frames
		if (!resting && !hitWall)
		{
			if (pillarAnim.Animation == "NotResting" && pillarAnim.Frame == 11)
			{
				resting = true;
				hitWall = true;
				pillarAnim.Play("NotResting", true);
				if ((GlobalPosition - Player.player.GlobalPosition).Length() <= 65f)
				{
					SoundsManager.crush.Play();
				}
				restTimer.Stop();
				restTimer.Start();

			}
		}
		if (GameData.won || GameData.gameOver)
		{
			QueueFree();
		}
	}

	private void OnRestTimerOut()		//to go hit the wall again
	{
		resting = false;
		hitWall = false;
		restTimer.Stop();
		pillarAnim.Play("NotResting");
	}

	private void OnHurtAreaBodyEntered(object body)
	{
		if (!resting && body == Player.player)
		{
			GameData.dead = true;
			Player.playerAnim.Visible = false;
		}
	}
}
