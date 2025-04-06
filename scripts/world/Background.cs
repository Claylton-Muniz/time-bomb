using Godot;
using System;

public partial class Background : ParallaxBackground
{
	[Export] private bool canProcess;
	[Export] private int layer_speed;

	private ParallaxLayer layer;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		layer = GetChild<ParallaxLayer>(5);
		if (!canProcess)
		{
			SetPhysicsProcess(false);
		}
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _PhysicsProcess(double delta)
	{
		layer.MotionOffset = new Vector2(
				layer.MotionOffset.X + (float)(delta * layer_speed), layer.MotionOffset.Y
			);
	}

}
