extends Camera2D

@export var speed : float

func _process(delta: float) -> void:
	position.x += speed * delta * Input.get_axis("ui_left", "ui_right")
