extends Area2D

func acende() -> void:
	$PointLight2D.visible = true
	
	await get_tree().create_timer(1.0).timeout
	
	$PointLight2D.visible = false

func play() -> void:
	$AudioStreamPlayer2D.play()
	acende()


func _on_input_event(viewport: Node, event: InputEvent, shape_idx: int) -> void:
	if event.is_action_pressed("pressiona_botao"):
		play()
