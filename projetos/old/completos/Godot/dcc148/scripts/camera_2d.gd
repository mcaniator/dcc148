extends Camera2D

@export var speed: float
@export var min_zoom: Vector2 = Vector2(0.1, 0.1)
@export var max_zoom: Vector2 = Vector2(10, 10)
@export var zoom_speed: float

func _process(_delta: float) -> void:
	free_look()

func free_look() -> void:
	var dx = Input.get_axis("move_left", "move_right")
	var dy = Input.get_axis("move_up", "move_down")
	var dz = Input.get_axis("zoom_out", "zoom_in")
	
	translate(Vector2(dx, dy).normalized() * speed)
	zoom = clamp(zoom + Vector2(dz, dz).normalized() * zoom_speed, min_zoom, max_zoom)
