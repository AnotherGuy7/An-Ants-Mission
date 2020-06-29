using Godot;
using System;

public class Spikes : Area2D
{
	private AnimatedSprite spikes;
	private Timer activationTimer;

	private bool spikesOut = false;
	private bool scannedForBodies = false;

	public override void _Ready()
	{
		spikes = GetNode<AnimatedSprite>("SpikesSprite");
		activationTimer = GetNode<Timer>("ActivationTimer");
	}

	private void OnSpikesEntered(object body)
	{
		if (spikesOut)
		{
			if (body == Player.player)
			{
				GameData.dead = true;
			}
		}
	}

	public override void _Process(float delta)
	{
		if (spikesOut)
		{
			spikes.Play("On");
		}
		else
		{
			spikes.Play("Off");
			scannedForBodies = false;
		}
		if (spikesOut && !scannedForBodies)
		{
			foreach (object body in GetOverlappingBodies())		//because if you stand on top of a spike and not move while the spike is active, you're counted as not entering
			{
				if (body == Player.player)
				{
					GameData.dead = true;
				}
			}
			scannedForBodies = true;
		}
		if (GameData.won || GameData.gameOver)
		{
			QueueFree();
		}
	}

	private void OnTimerOut()
	{
		spikesOut = !spikesOut;
		if (spikesOut && (GlobalPosition - Player.player.GlobalPosition).Length() <= 65f)
		{
			SoundsManager.spike.Play();
		}
		activationTimer.Start();
	}
}
