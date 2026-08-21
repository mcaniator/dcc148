extends StaticBody2D

func _ready() -> void:
	$Objeto/Label.text = 'STATIC BODY'

func _physics_process(delta: float) -> void:
	if $Objeto.esta_ativo():
		var dx = Input.get_axis("ui_left", "ui_right")
		var dy = Input.get_axis("ui_down", "ui_up")
		
		var deslocamento = Vector2(dx * delta, -dy * delta).normalized() * $Objeto.velocidade
		
		#position += deslocamento
		move_and_collide(deslocamento)
