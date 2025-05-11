extends CharacterBody2D
class_name Player


const JUMP_VELOCITY = -340.0

var speed: float = 300.0
var is_decelerating_from_boost: bool = false
var jump_velocity: float = JUMP_VELOCITY
var gravity_scale: float = 1.0

var is_waiting_for_rewind: bool = false
var rewind_start_time: float = 0.0

@onready var default_speed: float = speed # Para armazenar a velocidade original
@onready var player_sprite: AnimatedSprite2D = get_node("AnimatedSprite2D")

@export var boost_deceleration_rate: float = 200.0
@export var base_gravity: float = 980.0

func _physics_process(delta: float) -> void:
	# Add the gravity.
	if not is_on_floor():
		velocity += Vector2(0, base_gravity * gravity_scale * delta)

	if Input.is_action_just_pressed("rebobinar_button"):
		if RewindManager.is_rewinding:
			# Se já estiver rebobinando, cancela
			RewindManager.stop_rewind()
			is_waiting_for_rewind = false
			print("Rebobinamento cancelado")
		elif is_waiting_for_rewind:
			# Segundo clique: calcular o tempo e rebobinar
			var elapsed_time = Time.get_ticks_msec() / 1000.0 - rewind_start_time
			RewindManager.rewind_time = clamp(elapsed_time, 0.1, 10.0) # evitar tempo zero e limitar a 10s por exemplo
			RewindManager.start_rewind()
			is_waiting_for_rewind = false
			print("Rebobinando por ", RewindManager.rewind_time, " segundos")
		elif not RewindManager.is_rewinding:
			# Primeiro clique: salvar tempo atual
			rewind_start_time = Time.get_ticks_msec() / 1000.0
			is_waiting_for_rewind = true
			print("Início do tempo de rebobinação")


	# Handle jump.
	if (Input.is_action_just_pressed("ui_accept") || Input.is_action_just_pressed("up_button")) and is_on_floor():
		velocity.y = jump_velocity
		
	if is_decelerating_from_boost:
		speed = move_toward(speed, default_speed, boost_deceleration_rate * delta)
		if abs(speed - default_speed) < 0.1: # tolerância para comparação de floats
			speed = default_speed
			is_decelerating_from_boost = false
			if has_meta("boosted_by_area"):
				remove_meta("boosted_by_area")

	# Get the input direction and handle the movement/deceleration.
	# As good practice, you should replace UI actions with custom gameplay actions.
	var direction := Input.get_axis("left_button", "right_button")
	if direction:
		velocity.x = direction * speed
	else:
		velocity.x = move_toward(velocity.x, 0, speed)
		
	player_sprite.animate(velocity)

	move_and_slide()


# Funções para alterar e resetar a velocidade
func apply_speed_boost(boost_factor: float) -> void:
	is_decelerating_from_boost = false # Cancela desaceleração
	speed = default_speed * boost_factor
	set_meta("boosted_by_area", true) # Marca que o boost foi aplicado


func apply_speed_slow(slow_factor: float) -> void:
	is_decelerating_from_boost = false # Garante que não haja desaceleração acontecendo
	speed = default_speed * slow_factor
	set_meta("slowed_by_area", true)


func start_boost_deceleration() -> void:
	if speed > default_speed:
		is_decelerating_from_boost = true
	else:
		is_decelerating_from_boost = false
		if has_meta("boosted_by_area"):
			remove_meta("boosted_by_area")


func get_default_speed() -> float:
	return default_speed


func remove_speed_slow() -> void:
	if speed < default_speed:
		speed = default_speed
	if has_meta("slowed_by_area"):
		remove_meta("slowed_by_area")


func apply_physics_scale(scale_factor: float) -> void:
	gravity_scale = scale_factor
	jump_velocity = JUMP_VELOCITY * sqrt(scale_factor)


func reset_physics_scale() -> void:
	jump_velocity = JUMP_VELOCITY
	gravity_scale = 1.0


func _on_rewind_timeout():
	is_waiting_for_rewind = false
	print("Tempo para rebobinar expirou.")
