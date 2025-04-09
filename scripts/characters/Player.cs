using Godot;
using System;

public partial class Player : CharacterBody2D
{
	[Export] public float defaultSpeed = 150.0f;
	[Export] public float Speed = 150.0f;
	[Export] public float JumpVelocity = -400.0f;

	[Export] public float speedLerp = 5.0f; // Quanto maior, mais rápido volta ao normal

	public float targetSpeed;
	public float boostGravity;

	private PlayerTexture playerSprite;
	public bool onHit = false;
	public bool death = false;

	public override void _Ready()
	{
		playerSprite = GetNode<PlayerTexture>("Texture");
		targetSpeed = defaultSpeed;
		boostGravity = 1.0f;
	}

	public override void _PhysicsProcess(double delta)
	{
		// Faz a velocidade se aproximar do targetSpeed suavemente
		Speed = Mathf.Lerp(Speed, targetSpeed, (float)delta * speedLerp);

		Vector2 velocity = Velocity;

		if (!IsOnFloor())
			velocity += GetGravity() * (float)delta * boostGravity;

		if ((Input.IsActionJustPressed("ui_accept") || Input.IsActionJustPressed("up_button")) && IsOnFloor())
			velocity.Y = JumpVelocity;

		if (onHit && !death)
		{
			death = true;
			velocity.Y = JumpVelocity / 2;
			velocity.X = Speed / -4;
		}

		if (!onHit)
		{
			Vector2 direction = Input.GetVector("left_button", "right_button", "up_button", "down_button");

			if (direction != Vector2.Zero)
				velocity.X = direction.X * Speed;
			else
				velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);
		}

		Velocity = velocity;
		MoveAndSlide();
		playerSprite.Animate(velocity);
	}
}
