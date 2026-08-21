extends CharacterBody3D

@export var speed: float

func _physics_process(delta: float) -> void:
	var dx = Input.get_axis("move_left", "move_right")
	var dz = Input.get_axis("move_up", "move_down")
	
	var motion_vector = Vector3(dx, 0, dz) * speed * delta
	
	move_and_collide(motion_vector)
