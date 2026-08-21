extends Sprite2D

@export var speed : float
@export var jump : float
@export var gravity : float

var screen : Rect2
var v_y : float
var ground : float

func _ready() -> void:
	v_y = 0
	ground = position.y

func _process(delta: float) -> void:
	var dx = Input.get_axis("ui_left", "ui_right")
	
	if Input.is_action_just_pressed("ui_accept"):
		v_y = jump
	
	v_y += gravity * delta
	
	position.x += dx * speed * delta
	position.y += v_y * delta
	
	var camera = get_viewport().get_camera_2d()
	position.x = clamp(position.x, camera.limit_left, camera.limit_right)
	position.y = clamp(position.y, camera.limit_top, ground)
