extends Area2D


func _on_fallzone_body_entered(body: Node2D) -> void:
	if body is CharacterBody2D:
		body.call_deferred("queue_free")
		get_tree().call_deferred("reload_current_scene")
