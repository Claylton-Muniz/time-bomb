using Godot;
using System;

public partial class Player : CharacterBody2D
{
	[Export] public float Speed = 300.0f;
	[Export] public float JumpVelocity = -400.0f;

	private PlayerTexture playerSprite;

	public override void _Ready()
	{
		playerSprite = GetNode<PlayerTexture>("Texture");
	}

	public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = Velocity;

		// Add the gravity.
		if (!IsOnFloor())
		{
			velocity += GetGravity() * (float)delta;
		}

		// Handle Jump.
		if (
			Input.IsActionJustPressed("ui_accept") ||
			Input.IsActionJustPressed("up_button") &&
			IsOnFloor()
			)
		{
			velocity.Y = JumpVelocity;
		}

		// Get the input direction and handle the movement/deceleration.
		// As good practice, you should replace UI actions with custom gameplay actions.
		Vector2 direction = Input.GetVector("left_button", "right_button", "up_button", "down_button");
		if (direction != Vector2.Zero)
		{
			velocity.X = direction.X * Speed;
		}
		else
		{
			velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);
		}

		Velocity = velocity;
		MoveAndSlide();
		playerSprite.Animate(velocity);
	}
}
