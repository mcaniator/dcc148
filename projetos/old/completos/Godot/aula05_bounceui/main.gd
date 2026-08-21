extends Node

var pontos = 0

func _on_jogador_tocou_chao() -> void:
	pontos += 1
	$Pontos.text = str(pontos)
	
	if pontos % 10 == 0:
		$BolaSuperior.aumenta_velocidade(1.2)
		$BolaInferior.aumenta_velocidade(1.2)
