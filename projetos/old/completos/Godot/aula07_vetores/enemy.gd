extends Node2D

@export var velocidade : float
@export var alvo : Node2D

var destino : Vector2
var direcao = Vector2.LEFT

func _ready() -> void:
	destino = $Topo.position
	
func persegue() -> bool:
	if alvo == null || not alvo.is_node_ready():
		return false
		
	if alvo.direcao.dot(direcao) < 0 and alvo.position.distance_to(position) < 500:
		return true
	else:
		return false

func _process(delta: float) -> void:
	if persegue():
		var dv = (alvo.position - position).normalized()
		translate(dv * velocidade * delta)
	else:
		position = position.move_toward(destino, velocidade*delta)
		
		if position.is_equal_approx(destino):
			if destino == $Topo.position:
				destino = $Base.position
			else:
				destino = $Topo.position
