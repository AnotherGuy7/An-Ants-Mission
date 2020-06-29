using Godot;
using System;

public class CaveMap : Node2D
{
	[Export]
	public PackedScene weaponSet1;

	[Export]
	public PackedScene weaponSet2;

	[Export]
	public PackedScene weaponSet3;

	[Export]
	public PackedScene weaponSet4;

	private Camera2D titleCam;
	private Timer deathTimer;
	private Position2D spawnPoint;      //the remnants of a bad approach I was about to go for
	private Label winingLabel;
	private AnimationPlayer winningScene;
	private Node2D weaponsContainer;
	/*private Position2D weaponPos1;
	private Position2D weaponPos2;
	private Position2D weaponPos3;
	private Position2D weaponPos4;
	private Position2D weaponPos5;
	private Position2D weaponPos6;
	private Position2D weaponPos7;
	private Position2D weaponPos8;
	private Position2D weaponPos9;
	private Position2D weaponPos10;
	private Position2D weaponPos11;
	private Position2D weaponPos12;
	private Position2D weaponPos13;*/

	private Random rand = new Random();


	public override void _Ready()
	{
		titleCam = GetNode<Camera2D>("TitleCam");
		deathTimer = GetNode<Timer>("DeathTimer");
		spawnPoint = GetNode<Position2D>("SpawnPoint");
		winingLabel = GetNode<Label>("Player/WinningLabel");
		winningScene = GetNode<AnimationPlayer>("WinningScene");
		weaponsContainer = GetNode<Node2D>("WeaponsContainer");
		/*weaponPos1 = GetNode<Position2D>("WeaponPos1");
		weaponPos2 = GetNode<Position2D>("WeaponPos2");
		weaponPos3 = GetNode<Position2D>("WeaponPos3");
		weaponPos4 = GetNode<Position2D>("WeaponPos4");
		weaponPos5 = GetNode<Position2D>("WeaponPos5");
		weaponPos6 = GetNode<Position2D>("WeaponPos6");
		weaponPos7 = GetNode<Position2D>("WeaponPos7");
		weaponPos8 = GetNode<Position2D>("WeaponPos8");
		weaponPos9 = GetNode<Position2D>("WeaponPos9");
		weaponPos10 = GetNode<Position2D>("WeaponPos10");
		weaponPos11 = GetNode<Position2D>("WeaponPos11");
		weaponPos12 = GetNode<Position2D>("WeaponPos12");
		weaponPos13 = GetNode<Position2D>("WeaponPos13");*/
	}

	private void OnFoodEntered(object body)
	{
		if (body.GetType().ToString() == "Player")
		{
			GameData.won = true;
		}
	}

	public override void _Process(float delta)
	{
		if (GameData.dead && deathTimer.IsStopped())
		{
			deathTimer.Start();
		}
		if (GameData.won)
		{
			winingLabel.Visible = true;
			winingLabel.Text = "You have obtained enough food in this mission to last 2 weeks... Good Job!\nRemaining time: " + GameData.minutesLeft + ":" + GameData.secondsLeft;
			winningScene.Play("WinningAnim");
			GameData.paused = true;
		}
	}

	private void OnWinningAnimDone(String anim_name)
	{
		GameData.paused = true;
		titleCam.Current = true;
		titleCam.Visible = true;
		Player.playerCam.Current = false;
	}

	private void OnDeathTimerOut()
	{
		deathTimer.Stop();
		GameData.dead = false;
		Player.playerAnim.Visible = true;
		Player.player.GlobalPosition = spawnPoint.GlobalPosition;
		if (GameData.gameOver)
		{
			GameData.paused = true;
			titleCam.Current = true;
			titleCam.Visible = true;
			Player.playerCam.Current = false;
		}
	}

	private void OnPlayPressed()		//resetting variables upon pressing, as well as generating the traps
	{
		winingLabel.Visible = false;
		GameData.paused = false;
		titleCam.Current = false;
		titleCam.Visible = false;
		Player.playerCam.Current = true;
		GameData.minutesLeft = 1;
		GameData.secondsLeft = 30;
		GameData.won = false;
		GameData.dead = false;
		GameData.gameOver = false;
		Player.player.Position = spawnPoint.GlobalPosition;
		for (int i = 0; i < GetChildCount(); i++)
		{
			Node potentialWeaponPoint = GetChild(i);
			if (potentialWeaponPoint.GetType().ToString() == "Godot.Position2D")
			{
				Position2D weaponPoint = potentialWeaponPoint as Position2D;
				if (weaponPoint.Name.Contains("Weapon"))		//separating player spawn and weapon spawns
				{
					switch (rand.Next(0, 4))
					{
						case 0:
							Node2D weapon = (Node2D)weaponSet1.Instance();
							weapon.GlobalPosition = weaponPoint.GlobalPosition;
							weaponsContainer.AddChild(weapon);
							break;
						case 1:
							Node2D weapon2 = weaponSet2.Instance() as Node2D;
							weapon2.GlobalPosition = weaponPoint.GlobalPosition;
							weaponsContainer.AddChild(weapon2);
							break;
						case 2:
							StaticBody2D weapon3 = weaponSet3.Instance() as StaticBody2D;
							weapon3.GlobalPosition = weaponPoint.GlobalPosition - new Vector2(24f, 0f);
							weaponsContainer.AddChild(weapon3);
							break;
						case 3:
							StaticBody2D weapon4 = weaponSet4.Instance() as StaticBody2D;
							weapon4.GlobalPosition = weaponPoint.GlobalPosition - new Vector2(24f, 0f);
							weaponsContainer.AddChild(weapon4);
							break;
					}

				}
			}
		}
	}
}
