extends Node

func _physics_process(delta: float) -> void:
	$DirectionalLight3D.rotate_z(deg_to_rad(1))
