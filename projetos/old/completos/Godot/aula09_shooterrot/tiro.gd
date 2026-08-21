extends Area2D

signal hit

@export var velocidade : float

var direcao = Vector2.RIGHT

func _physics_process(delta: float) -> void:
	global_translate(direcao * velocidade * delta)


func _on_area_entered(area: Area2D) -> void:
	hit.emit()
