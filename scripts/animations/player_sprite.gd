extends AnimatedSprite2D
class_name PlayerSprite


@export var animation_path: NodePath
@onready var anim_player: AnimationPlayer = get_node(animation_path) as AnimationPlayer

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
		anim_player.play("run")
	else:
		anim_player.play("idle")

func vertical_behavior(velocity: Vector2) -> void:
	if velocity.y < 0:
		anim_player.play("jump")
