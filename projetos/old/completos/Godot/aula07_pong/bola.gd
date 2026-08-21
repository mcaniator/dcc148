extends CharacterBody2D

@export var velocidade : float

var direcao : Vector2

func _ready() -> void:
	direcao = Vector2(randf_range(-0.25, 0.25), randf_range(-0.25, 0.25))
	direcao = direcao.normalized()

func _physics_process(delta: float) -> void:
	var dv = direcao * delta * velocidade
	var colisao = move_and_collide(dv)
	
	if colisao:
		direcao = direcao.bounce(colisao.get_normal())
	elif position.y <= 0:
		direcao = direcao.bounce(Vector2.DOWN)
	elif position.y >= 600:
		direcao = direcao.bounce(Vector2.UP)
	
