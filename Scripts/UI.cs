using Godot;
using System;

public class UI : Control
{
	private Label timeLabel;
	private Control controlsContainer;

	public override void _Ready()
	{
		timeLabel = GetNode<Label>("Layer1/TimeLabel");
		controlsContainer = GetNode<Control>("Layer1/ControlsContainer");
	}

	public override void _Process(float delta)
	{
		timeLabel.Visible = !GameData.paused;
		controlsContainer.Visible = !GameData.paused;
		timeLabel.Text = GameData.minutesLeft + ":" + GameData.secondsLeft;
	}
}
