extends Area2D

@export var velocidade : float
@export var velocidade_aproximacao : float

func _physics_process(delta: float) -> void:
	translate(Vector2.LEFT * velocidade * delta)
	scale += Vector2.ONE * velocidade_aproximacao * delta
