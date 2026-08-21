extends Area2D

@export var velocidade : float

func _process(delta: float) -> void:
	var dr = Input.get_axis("ui_left", "ui_right")
	
	var v_pos = $Canhao.position - $Base.position
	$Canhao.rotate(dr * velocidade * delta)
	
	$Canhao.position = $Base.position + v_pos.rotated(dr * velocidade * delta)
