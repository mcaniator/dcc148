extends Area2D

@export var speed : float
@export var jump : float
@export var gravidade : float

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
	
	v_y += gravidade * delta
	
	position.x += dx * speed * delta
	position.y += v_y * delta
	
	var camera = get_viewport().get_camera_2d()
	position.x = clamp(position.x, camera.limit_left, camera.limit_right)
	position.y = clamp(position.y, camera.limit_top, ground)


func _on_area_entered(area: Area2D) -> void:
	if v_y > 0:
		var plat_sp : Sprite2D
		plat_sp = area.get_child(0)
		position.y = area.position.y - (plat_sp.get_rect().size.x + $Sprite2D.get_rect().size.x)
		ground = position.y
