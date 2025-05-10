extends Node2D
class_name LevelDesign

@onready var timezone_scene: PackedScene = preload("res://scenes/effect/time_zone.tscn")
var timezone_instance: Node = null

func _input(event: InputEvent) -> void:
	if event is InputEventMouseButton and event.pressed:
		if timezone_instance == null or not is_instance_valid(timezone_instance):
			var instance_foi_criada: bool = false
			var nova_timezone: Node = null

			if event.button_index == MOUSE_BUTTON_LEFT:
				nova_timezone = timezone_scene.instantiate()
				instance_foi_criada = true

			elif event.button_index == MOUSE_BUTTON_RIGHT:
				nova_timezone = timezone_scene.instantiate()
				
				nova_timezone.is_slow_zone = true
				
				# Pega o filho responsável pela cor no timezone
				var animated_sprite_node = nova_timezone.get_node_or_null("AnimatedSprite2D") as AnimatedSprite2D
				if animated_sprite_node:
					animated_sprite_node.modulate = Color("#e100107c") # Aplica a cor diretamente no AnimatedSprite2D
				else:
					print("AnimatedSprite2D não encontrado na cena TimeZone para aplicar a cor (botão direito).")
				
				instance_foi_criada = true

			if instance_foi_criada and nova_timezone != null:
				timezone_instance = nova_timezone
				
				timezone_instance.global_position = get_global_mouse_position()
				get_tree().current_scene.add_child(timezone_instance) # Adiciona à cena principal

				var sprite_para_animacao = timezone_instance.get_node_or_null("AnimatedSprite2D") as AnimatedSprite2D
				if sprite_para_animacao:
					sprite_para_animacao.play("open")
				else:
					print("AnimatedSprite2D não encontrado na cena TimeZone para tocar a animação 'open'.")
		else:
			if event.button_index == MOUSE_BUTTON_LEFT or event.button_index == MOUSE_BUTTON_RIGHT:
				print("Uma instância de TimeZone criada por este script já está ativa. Clique ignorado.")
