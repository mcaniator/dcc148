extends Area2D

func _ready() -> void:
	$Objeto/Label.text = 'AREA'
	$Objeto/Sprite2D

func _physics_process(delta: float) -> void:
	if $Objeto.esta_ativo():
		var dx = Input.get_axis("ui_left", "ui_right")
		var dy = Input.get_axis("ui_down", "ui_up")
		
		position += Vector2(dx * delta, -dy * delta).normalized() * $Objeto.velocidade
