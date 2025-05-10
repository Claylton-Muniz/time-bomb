extends Area2D
class_name TimeZone

@export var speed_boost_factor: float = 1.5
@export var is_slow_zone: bool = false


func _on_time_zone_body_entered(body: Node2D) -> void:
	if body.get_parent() is MovingPlatform:
		print("Entrou: " + body.name)
		var platform: MovingPlatform = body.get_parent() as MovingPlatform

		if is_slow_zone:
			platform.move_speed = 100.0
			platform.move_platform()
		else:
			platform.move_speed *= 1.5
			platform.move_platform()
	elif body is Player:
		var player: Player = body as Player
		
		if is_slow_zone:
			# Aplica lentidão se ainda não está lento
			if not player.has_meta("slowed_by_area"):
				player.apply_speed_slow(0.5) # ou outro fator que desejar
				player.apply_physics_scale(0.5)
		else:
			# Se já está com boost não faz nada
			if player.has_meta("boosted_by_area") and player.get_meta("boosted_by_area") and player.speed >= (player.get_default_speed() * speed_boost_factor * 0.99):
				return
			player.apply_speed_boost(speed_boost_factor)
			player.apply_physics_scale(speed_boost_factor)

func _on_time_zone_body_exited(body: Node2D) -> void:
	if body is Player:
		var player: Player = body as Player
		if is_slow_zone:
			player.remove_speed_slow()
			player.reset_physics_scale()
		else:
			player.start_boost_deceleration()
			player.reset_physics_scale()
	
	if body.get_parent() is MovingPlatform:
		var platform: MovingPlatform = body.get_parent() as MovingPlatform
		
		platform.move_speed = 700.0
		platform.move_platform()
