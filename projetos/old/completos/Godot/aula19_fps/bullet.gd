extends Area3D

@export var speed: float

var dir = Vector3.FORWARD

func _physics_process(delta: float) -> void:
	position += dir * speed * delta
	
	if position.length_squared() > 100:
		queue_free()

func _on_area_entered(area: Area3D) -> void:
	queue_free()
