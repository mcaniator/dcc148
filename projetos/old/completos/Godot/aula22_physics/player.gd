extends RigidBody3D

func _physics_process(delta: float) -> void:
	if Input.is_mouse_button_pressed(MOUSE_BUTTON_LEFT):
		apply_force(Vector3.FORWARD * 100)
