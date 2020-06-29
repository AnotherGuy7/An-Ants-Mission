using Godot;

public class Player : KinematicBody2D
{
	public static Camera2D playerCam;
	public static Player player;
	public static AnimatedSprite playerAnim;

	private bool playedSound = false;
	private float moveSpeed = 1f;

	public override void _Ready()
	{
		playerCam = GetNode<Camera2D>("PlayerCam");
		playerAnim = GetNode<AnimatedSprite>("PlayerAnim");
		player = this;
	}

	public override void _PhysicsProcess(float delta)
	{
		Vector2 velocity = Vector2.Zero;

		if (!GameData.dead)
		{
			if (Input.IsActionPressed("move_up"))
			{
				velocity.y -= moveSpeed;
				RotationDegrees = 0;
			}
			if (Input.IsActionPressed("move_down"))
			{
				velocity.y += moveSpeed;
				RotationDegrees = 180;
			}
			if (Input.IsActionPressed("move_left"))
			{
				velocity.x -= moveSpeed;
				RotationDegrees = 270;
			}
			if (Input.IsActionPressed("move_right"))
			{
				velocity.x += moveSpeed;
				RotationDegrees = 90;
			}

			if (velocity == Vector2.Zero)
			{
				playerAnim.Play("Idle");
			}
			else
			{
				playerAnim.Play("Walking");
			}

			MoveAndCollide(velocity);
		}
	}

	public override void _Process(float delta)
	{
		if (GameData.dead)
		{
			if (!playedSound)
			{
				SoundsManager.death.Play();
				playedSound = true;
			}
			playerAnim.Play("Dead");
		}
		else
		{
			if (playedSound)
			{
				playedSound = false;
			}
		}
	}
}
