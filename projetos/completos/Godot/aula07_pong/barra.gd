extends CharacterBody2D

enum TipoJogador {CPU, PLAYER_1, PLAYER_2}

@export var tipo_jogador : TipoJogador
@export var velocidade : float

func _physics_process(delta: float) -> void:
	match tipo_jogador:
		TipoJogador.CPU: cpu_process(delta)
		TipoJogador.PLAYER_1: player_process(delta, 1)
		TipoJogador.PLAYER_2: player_process(delta, 2)

func cpu_process(delta: float) -> void:
	pass

func player_process(delta: float, player: int) -> void:
	var dy = 0
	if player == 1:
		dy = Input.get_axis("cima_p1", "baixo_p1")
	else:
		dy = Input.get_axis("cima_p2", "baixo_p2")
	
	#translate(Vector2(0, dy) * delta * velocidade)
	move_and_collide(Vector2(0, dy) * delta * velocidade)
