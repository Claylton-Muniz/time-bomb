using Godot;
using System;

public partial class TimeZone : Area2D
{
	private float memSpeed;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		this.BodyEntered += OnBodyEntered;
		this.BodyExited += OnBodyExited;
	}

	private void OnBodyEntered(Node2D body)
	{
		if (body is Player player)
		{
			memSpeed = player.Speed;
			player.Speed = 500.0f;
		}
	}

	private void OnBodyExited(Node2D body)
	{
		if (body is Player player) player.Speed = memSpeed;
	}
}
