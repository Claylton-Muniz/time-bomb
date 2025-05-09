extends AnimatedSprite2D

func _ready():
	animation_finished.connect(_on_animation_finished)

func _on_animation_finished():
	match animation:
		"open":
			play("run")
			await get_tree().create_timer(3.0).timeout
			play("close")
		"close":
			owner.queue_free() # Libera a cena inteira TimeZone
