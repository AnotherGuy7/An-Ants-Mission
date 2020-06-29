using Godot;

public class FlameGeyser : StaticBody2D		//3s on, 2s off
{
	private Timer shootTimer;
	private Timer restTimer;
	private AnimatedSprite geyserSprite;
	private Area2D flameBox;

	private bool playedSound = false;
	private bool scannedForBodies = false;
	private bool flameOut;
	
	public override void _Ready()
	{
		shootTimer = GetNode<Timer>("ShootTimer");
		restTimer = GetNode<Timer>("RestTimer");
		geyserSprite = GetNode<AnimatedSprite>("GeyserSprite");
		flameBox = GetNode<Area2D>("FlameBox");
	}

	public override void _Process(float delta)
	{
		if (flameOut)
		{
			geyserSprite.Play("On");
			if (flameOut && (GlobalPosition - Player.player.GlobalPosition).Length() <= 65f && !playedSound)
			{
				SoundsManager.fire.Play();
				playedSound = true;
			}
			if (flameOut && !scannedForBodies)
			{
				foreach (object body in flameBox.GetOverlappingBodies())     //because if you stand on top of a spike and not move while the spike is active, you're counted as not entering
				{
					if (body == Player.player)
					{
						GameData.dead = true;
					}
				}
				scannedForBodies = true;
			}
		}
		else
		{
			if (SoundsManager.fire.Playing)
			{
				SoundsManager.fire.Stop();
			}
			geyserSprite.Play("Off");
		}
		if (GameData.won || GameData.gameOver)
		{
			QueueFree();
		}
	}

	private void OnFlameBoxBodyEntered(object body)
	{
		if (flameOut && body == Player.player)
		{
			GameData.dead = true;
			Player.playerAnim.Visible = false;
		}
	}

	private void OnShootTimerOut()		//done shooting
	{
		flameOut = false;
		restTimer.Start();
		shootTimer.Stop();
	}
	
	private void OnRestTimerOut()		//done resting
	{
		flameOut = true;
		playedSound = false;
		scannedForBodies = false;
		shootTimer.Start();
		restTimer.Stop();
	}
}
