using Godot;
using System;

public partial class LevelDesign : Node2D
{
	private Player player;
	private PlayerTexture playerTexture;
	private PackedScene timeZoneScene;
	private Node2D currentTimeZone = null;

	public override void _Ready()
	{
		player = GetNode<Player>("Player");
		playerTexture = player.GetNode<PlayerTexture>("Texture");
		timeZoneScene = GD.Load<PackedScene>("res://scenes/effect/time_zone.tscn");

		playerTexture.OnGameOver += OnGameOver;
	}

	public override void _Input(InputEvent @event)
	{
		if (@event is InputEventMouseButton mouseEvent && mouseEvent.Pressed && mouseEvent.ButtonIndex == MouseButton.Left)
		{
			if (currentTimeZone != null) return; // Já tem uma ativa

			Vector2 clickPosition = GetGlobalMousePosition();
			GD.Print("Clique detectado em: " + clickPosition);

			currentTimeZone = (Node2D)timeZoneScene.Instantiate();
			currentTimeZone.GlobalPosition = clickPosition;
			AddChild(currentTimeZone);

			// Espera 3 segundos e destrói
			GetTree().CreateTimer(3.0f).Timeout += () =>
			{
				if (IsInstanceValid(currentTimeZone))
					currentTimeZone.QueueFree();
				currentTimeZone = null;
			};
		}
	}

	private void OnGameOver()
	{
		GetTree().ReloadCurrentScene();
	}
}
