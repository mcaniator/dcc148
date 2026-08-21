extends Area2D

@export var velocidade : float
@export var velocidade_rotacao : float
@export var frequencia : float
@export var amplitude : float
@export var posicao_central: float

func _physics_process(delta: float) -> void:
	position.x -= velocidade * delta
	position.y = posicao_central + amplitude * sin(deg_to_rad(position.x) * frequencia)
	
	rotate(velocidade_rotacao * delta)
