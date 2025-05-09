extends CharacterBody2D


const SPEED = 300.0
const JUMP_VELOCITY = -340.0


@onready var player_sprite: AnimatedSprite2D = get_node("AnimatedSprite2D")

func _physics_process(delta: float) -> void:
	# Add the gravity.
	if not is_on_floor():
		velocity += get_gravity() * delta

	# Handle jump.
	if Input.is_action_just_pressed("ui_accept") || Input.is_action_just_pressed("up_button") and is_on_floor():
		velocity.y = JUMP_VELOCITY

	# Get the input direction and handle the movement/deceleration.
	# As good practice, you should replace UI actions with custom gameplay actions.
	var direction := Input.get_axis("left_button", "right_button")
	if direction:
		velocity.x = direction * SPEED
	else:
		velocity.x = move_toward(velocity.x, 0, SPEED)
		
	player_sprite.animate(velocity)

	move_and_slide()
