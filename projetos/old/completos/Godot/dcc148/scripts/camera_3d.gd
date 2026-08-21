extends Camera3D

@export var eye_speed: float
@export var move_speed: float

var base_orientation: Quaternion
var mouse_h: float = 0
var mouse_v: float = 0

func _ready() -> void:
	base_orientation = quaternion

func _input(event: InputEvent) -> void:
	if event is InputEventMouseMotion:
		mouse_h = mouse_h + event.relative.x
		mouse_v = clampf(mouse_v + event.relative.y, -90, 90)

func _process(delta: float) -> void:
	var dx = Input.get_axis("move_left", "move_right")
	var dy = Input.get_axis("fly_down", "fly_up")
	var dz = Input.get_axis("move_forward", "move_back")
	
	var angle_y = mouse_h * eye_speed * delta
	var angle_x = mouse_v * eye_speed * delta
	var rot_y = Quaternion(Vector3.UP, -angle_y)
	var rot_x = Quaternion(Vector3.LEFT, angle_x)
	
	quaternion = base_orientation * rot_y
	translate(Vector3(dx, dy, dz).normalized() * move_speed * delta)
	quaternion *= rot_x
	
	
