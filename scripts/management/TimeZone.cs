using Godot;
using System;

public partial class TimeZone : Area2D
{
	private float slowSpeed = 500.0f;

	public override void _Ready()
	{
		this.BodyEntered += OnBodyEntered;
		this.BodyExited += OnBodyExited;
	}

	private void OnBodyEntered(Node2D body)
	{
		if (body is Player player)
		{
			player.targetSpeed = slowSpeed;
			player.boostGravity = 1.5f;
		}
	}

	private void OnBodyExited(Node2D body)
	{
		if (body is Player player)
		{
			player.targetSpeed = player.defaultSpeed;
			player.boostGravity = 1.0f;
		}
	}
}
