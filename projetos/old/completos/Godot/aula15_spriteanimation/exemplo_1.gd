extends Sprite2D

@export var speed: float
@export var walk_start: int
@export var walk_end: int

func _process(delta: float) -> void:
	var dx = Input.get_axis("ui_left", "ui_right")
	position.x += speed * delta * dx
	frame = walk_start + ((frame + int(dx) - 1) % (walk_end - walk_start + 1))
