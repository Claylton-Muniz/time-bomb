using Godot;
using System;

public partial class FallZone : Area2D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{

		this.BodyEntered += OnBodyEntered;
	}

	private void OnBodyEntered(Node2D body)
	{
		if (body is Player player) player.onHit = true;
	}
}
