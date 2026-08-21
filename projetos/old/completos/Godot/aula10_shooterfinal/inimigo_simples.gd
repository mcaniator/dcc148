extends Area2D

@export var velocidade : float

func _physics_process(delta: float) -> void:
	translate(Vector2.LEFT * velocidade * delta)
