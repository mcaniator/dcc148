extends Sprite2D

@export var velocidade : float

var direcao = Vector2.RIGHT

func _process(delta: float) -> void:
	var dx = Input.get_axis("ui_left", "ui_right")
	var dv = Vector2(dx, 0) * velocidade * delta
	translate(dv)
	
	if dx < 0:
		direcao = Vector2.LEFT
	elif dx > 0:
		direcao = Vector2.RIGHT
