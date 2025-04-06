using Godot;
using System;

public partial class LevelDesign : Node2D
{
	private Player player;
	private PlayerTexture playerTexture;

	public override void _Ready()
	{
		player = GetNode<Player>("Player");
		playerTexture = player.GetNode<PlayerTexture>("Texture");

		playerTexture.OnGameOver += OnGameOver;

	}

	private void OnGameOver()
	{
		GetTree().ReloadCurrentScene();
	}
}
