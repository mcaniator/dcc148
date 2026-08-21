extends Node

var botoes : Array
var sequencia : Array
var tamanho_sequencia = 3
var rng = RandomNumberGenerator.new()

func _ready() -> void:
	botoes.append($Botao)
	botoes.append($Botao2)
	botoes.append($Botao3)
	botoes.append($Botao4)

func novo_jogo() -> void:
	sequencia.clear()
	for i in range(tamanho_sequencia):
		sequencia.append(rng.randi_range(0, tamanho_sequencia-1))
	
	for i in range(tamanho_sequencia):
		print(sequencia[i])
		botoes[ sequencia[i] ].play()
		await get_tree().create_timer(1.0).timeout


func _on_iniciar_jogo_pressed() -> void:
	$BackgroundMusic.stop()
	$IniciarJogo.hide()
	novo_jogo()
