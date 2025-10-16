extends Area2D

@export var velocidade : float

var direcao = 1

func _physics_process(delta: float) -> void:
	position.x += velocidade * delta * direcao
	
	if position.x > 1100:
		direcao = -1
	elif position.x < 0:
		direcao = 1

	
