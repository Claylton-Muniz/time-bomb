extends Node2D

@onready var timezone_scene: PackedScene = preload("res://scenes/effect/time_zone.tscn")
var timezone_instance: Node = null

func _input(event):
	if event is InputEventMouseButton and event.pressed and event.button_index == MOUSE_BUTTON_LEFT:
		# Garante que não existe outro TimeZone ativo
		if timezone_instance == null or not is_instance_valid(timezone_instance):
			timezone_instance = timezone_scene.instantiate()
			timezone_instance.global_position = get_global_mouse_position()
			get_tree().current_scene.add_child(timezone_instance)

			var animated_sprite = timezone_instance.get_node("AnimatedSprite2D")
			animated_sprite.play("open")
