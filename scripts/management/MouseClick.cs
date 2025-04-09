using Godot;
using System;

public partial class MouseClick : Camera2D
{
	private Area2D area2D;

	public override void _Ready()
	{
		area2D = GetNode<Area2D>("Area2D");
	}

	public override void _Process(double delta)
	{
		Vector2 cameraPosition = GlobalPosition;
		Vector2 zoom = Zoom;
		Vector2 viewportSize = GetViewportRect().Size;

		// Calcula só a parte extra do drag
		Vector2 dragMarginExtra = new Vector2(
			(viewportSize.X * DragRightMargin) + (viewportSize.X * DragLeftMargin),
			(viewportSize.Y * DragTopMargin) + (viewportSize.Y * DragBottomMargin)
		);

		Vector2 finalSize = viewportSize + dragMarginExtra;
		Vector2 halfSize = (finalSize / zoom) * 0.5f;

		// Calcula bordas brutas
		float left = cameraPosition.X - halfSize.X;
		float right = cameraPosition.X + halfSize.X;
		float top = cameraPosition.Y - halfSize.Y;
		float bottom = cameraPosition.Y + halfSize.Y;

		// Limites da câmera
		float limitLeft = LimitLeft;
		float limitRight = LimitRight;
		float limitTop = LimitTop;
		float limitBottom = LimitBottom;

		// Ajusta pra não passar dos limites
		if (left < limitLeft)
		{
			left = limitLeft;
			right = left + finalSize.X / zoom.X;
		}
		if (right > limitRight)
		{
			right = limitRight;
			left = right - finalSize.X / zoom.X;
		}
		if (top < limitTop)
		{
			top = limitTop;
			bottom = top + finalSize.Y / zoom.Y;
		}
		if (bottom > limitBottom)
		{
			bottom = limitBottom;
			top = bottom - finalSize.Y / zoom.Y;
		}

		// Atualiza a Area2D
		Vector2 areaPosition = new Vector2((left + right) / 2, (top + bottom) / 2);
		Vector2 areaSize = new Vector2(right - left, bottom - top);

		area2D.GlobalPosition = areaPosition;
		var collisionShape = area2D.GetNode<CollisionShape2D>("CollisionShape2D");
		var rectangleShape = (RectangleShape2D)collisionShape.Shape;
		rectangleShape.Size = areaSize;
	}
}
