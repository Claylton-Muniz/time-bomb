using Godot;
using System;

public partial class PlayerTexture : AnimatedSprite2D
{
	[Export] public NodePath AnimationPath;
	private AnimationPlayer animationPlayer;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		animationPlayer = GetNode<AnimationPlayer>(AnimationPath);
	}

	public void Animate(Vector2 velocity)
	{
		VerifyPosition(velocity);
		if (velocity.Y != 0)
		{
			VerticalBehavior(velocity);
		}
		else
		{
			HorizontalBehavior(velocity);
		}

	}

	private void VerifyPosition(Vector2 velocity)
	{
		if (velocity.X < 0)
		{
			FlipH = true;
			Offset = new Vector2(-20, 0); // para corrigir erro no sprite
		}
		else if (velocity.X > 0)
		{
			FlipH = false;
			Offset = new Vector2(0, 0);
		}
	}

	private void VerticalBehavior(Vector2 velocity)
	{
		if (velocity.Y < 0)
		{
			animationPlayer.Play("jump");
		}
	}

	private void HorizontalBehavior(Vector2 velocity)
	{
		if (velocity.X != 0)
		{
			animationPlayer.Play("run");
		}
		else
		{
			animationPlayer.Play("idle");
		}
	}
}
