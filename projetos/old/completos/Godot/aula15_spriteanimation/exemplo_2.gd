extends AnimatedSprite2D

@export var speed: float

func _process(delta: float) -> void:
	var dx = Input.get_axis("ui_left", "ui_right")
	position.x += speed * delta * dx
	if dx == 0:
		play("idle")
	else:
		if dx < 0:
			flip_h = true
		else:
			flip_h = false
		play("run")
