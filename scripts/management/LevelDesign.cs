using Godot;
using System;

public partial class LevelDesign : Node2D
{
	private Player player;
	private PlayerTexture playerTexture;
	private PackedScene timeZoneScene;

	public override void _Ready()
	{
		player = GetNode<Player>("Player");
		playerTexture = player.GetNode<PlayerTexture>("Texture");
		timeZoneScene = GD.Load<PackedScene>("res://scenes/effect/time_zone.tscn");

		playerTexture.OnGameOver += OnGameOver;

	}

	public override void _Input(InputEvent @event)
	{
		// Detecta clique com botão esquerdo do mouse
		if (@event is InputEventMouseButton mouseEvent && mouseEvent.Pressed && mouseEvent.ButtonIndex == MouseButton.Left)
		{
			// Pega posição global do mouse no mundo 2D
			Vector2 clickPosition = GetGlobalMousePosition();
			GD.Print("Clique detectado em: " + clickPosition);

			// Instancia a cena
			Node2D timeZoneInstance = (Node2D)timeZoneScene.Instantiate();
			timeZoneInstance.GlobalPosition = clickPosition;

			// Adiciona à cena atual
			AddChild(timeZoneInstance);
		}
	}

	private void OnGameOver()
	{
		GetTree().ReloadCurrentScene();
	}
}
