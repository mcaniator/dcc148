extends Area2D

@export var velocidade : Vector2
@export var gravidade : float
@export var impulso : float
@export var chao : float

signal tocou_chao

func _physics_process(delta: float) -> void:
	var dx = Input.get_axis("move_esquerda", "move_direita")
	position.x += dx * velocidade.x * delta
	
	velocidade.y += gravidade * delta
	position.y += velocidade.y * delta
	
	if position.y > chao:
		position.y = chao
		velocidade.y = impulso
		emit_signal("tocou_chao")

func _on_area_entered(area: Area2D) -> void:
	get_tree().change_scene_to_file("res://game_over.tscn")
