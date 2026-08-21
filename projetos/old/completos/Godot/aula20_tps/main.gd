extends Node

@export var speed: float
@export var turn_speed: float

func _process(delta: float) -> void:
	var dx = Input.get_axis("ui_right", "ui_left")
	var dz = Input.get_axis("ui_down", "ui_up")
	
	$Player.translate(Vector3(0, 0, dz) * speed * delta)
	$Player.rotate_y(dx * turn_speed * delta)
	
	if Input.is_action_pressed("rotate_camera"):
		var rot = Quaternion(Vector3.UP, deg_to_rad(1))
		var pivot_vector = $Player/Camera3D.position - $Player.position
		pivot_vector = rot * pivot_vector
		$Player/Camera3D.quaternion *= rot
		$Player/Camera3D.position = $Player.position + pivot_vector
	elif Input.is_action_just_released("zoom_in_camera"):
		$Player/Camera3D.translate(Vector3(0, 0, -1))
	elif Input.is_action_just_released("zoom_out_camera"):
		$Player/Camera3D.translate(Vector3(0, 0, 1))
	
	if dz != 0:
		$Player/AnimationPlayer.play("Run")
	else:
		$Player/AnimationPlayer.play("Idle")
		
	#$Player.translate(dv * speed * delta)
