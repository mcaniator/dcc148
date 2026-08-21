extends Camera2D

@export var speed : float
@export var follow: Node2D

func _process(delta: float) -> void:
	position.x = follow.position.x
