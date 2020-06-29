using Godot;
using System;

public class SoundsManager : Node2D
{
	public static AudioStreamPlayer2D select;
	public static AudioStreamPlayer2D death;
	public static AudioStreamPlayer2D fire;
	public static AudioStreamPlayer2D crush;
	public static AudioStreamPlayer2D spike;
	private AudioStreamPlayer2D music;

	public override void _Ready()
	{
		select = GetNode<AudioStreamPlayer2D>("Select");
		death = GetNode<AudioStreamPlayer2D>("Death");
		fire = GetNode<AudioStreamPlayer2D>("Fire");
		crush = GetNode<AudioStreamPlayer2D>("Crush");
		spike = GetNode<AudioStreamPlayer2D>("Spike");
		music = GetNode<AudioStreamPlayer2D>("Music");
	}

	private void OnMusicFinished()
	{
		music.Play();
	}

	public override void _Process(float delta)
	{
		if (Player.player != null)
		{
			GlobalPosition = Player.player.GlobalPosition;
		}
	}
}
