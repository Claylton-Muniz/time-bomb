extends AnimatedSprite2D
class_name PlayerSprite


@export var player_path: NodePath
@onready var player_ref: CharacterBody2D = get_node(player_path) as CharacterBody2D

func animate(velocity: Vector2) -> void:
	verify_position(velocity)
	if velocity.y != 0:
		vertical_behavior(velocity)
	else:
		horizontal_behavior(velocity)

func verify_position(velocity: Vector2) -> void:
	if velocity.x > 0:
		flip_h = false
		offset.x = 0
	elif velocity.x < 0:
		flip_h = true
		offset.x = -20

func horizontal_behavior(velocity: Vector2) -> void:
	if velocity.x != 0:
		play("run")
	else:
		play("idle")

func vertical_behavior(velocity: Vector2) -> void:
	if velocity.y < 0:
		play("jump")
