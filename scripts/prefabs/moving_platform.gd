extends Node2D
class_name MovingPlatform

@export var move_speed: float:
	get:
		return _move_speed
	set(value):
		_move_speed = value
		if is_inside_tree():
			move_platform()

var _move_speed: float = 700.0

@export var distance: int = 256
@export var move_horizontal: bool = true

@onready var platform = $platform as AnimatableBody2D

var start_position: Vector2
var end_position: Vector2
var tween: Tween
var moving_forward: bool = true


func _ready() -> void:
	start_position = platform.position
	end_position = start_position + (Vector2.RIGHT if move_horizontal else Vector2.DOWN) * distance
	move_platform()


func move_platform():
	if tween:
		tween.kill()

	var current_position = platform.position
	var target_position = end_position if moving_forward else start_position
	var distance_to_target = current_position.distance_to(target_position)
	var duration = distance_to_target / move_speed

	tween = create_tween()
	tween.tween_property(platform, "position", target_position, duration).set_trans(Tween.TRANS_LINEAR).set_ease(Tween.EASE_IN_OUT)
	tween.connect("finished", _on_tween_finished)


func _on_tween_finished():
	moving_forward = !moving_forward
	move_platform()
