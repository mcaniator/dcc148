extends Area3D

@export var speed: float

func _physics_process(delta: float) -> void:
	var dir = (get_viewport().get_camera_3d().position - position).normalized()
	
	position += dir * speed * delta

func _on_area_entered(area: Area3D) -> void:
	queue_free()
