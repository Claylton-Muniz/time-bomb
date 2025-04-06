using Godot;
using System;

public partial class PlayerTexture : AnimatedSprite2D
{
	[Signal] public delegate void OnGameOverEventHandler();
	
	[Export] public NodePath AnimationPath;
	[Export] public NodePath playerPath;

	private Player playerRef;
	private AnimationPlayer animationPlayer;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		animationPlayer = GetNode<AnimationPlayer>(AnimationPath);
		playerRef = GetNode<Player>(playerPath);

		// connection animationPlayer função animateFinish
		animationPlayer.AnimationFinished += OnAnimationFinished;
	}

	public void Animate(Vector2 velocity)
	{
		VerifyPosition(velocity);
		if (playerRef.onHit)
		{
			animationPlayer.Play("death");
			Offset = new Vector2(0, 0); // para corrigir erro na morte - culpa do sprite
		}
		else if (velocity.Y != 0)
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
			if (!playerRef.onHit)
			{
				FlipH = true;
				Offset = new Vector2(-20, 0); // para corrigir erro no sprite
			}
			else
			{
				FlipH = false;
			}
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

	private void OnAnimationFinished(StringName animName)
	{
		switch (animName)
		{
			case "death":
				playerRef.SetPhysicsProcess(false);
				EmitSignal(SignalName.OnGameOver);
				break;
		}
	}
}
