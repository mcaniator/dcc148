extends Node

#const CuboMorte = preload("res://cenas/cubo_morte.tscn")
#const CuboPadrao = preload("res://cenas/cubo_padrao.tscn")

@export var CuboMorte : PackedScene
@export var CuboPadrao : PackedScene

var cubo_ativo : Node3D
var pontos = 0
var rng = RandomNumberGenerator.new()

func _ready() -> void:
	cubo_ativo = CuboPadrao.instantiate()
	add_child(cubo_ativo)

func novo_cubo() -> void:
	cubo_ativo.queue_free()
	
	var rand_val = rng.randf_range(0.0, 1.0)
	if rand_val > 0.8:
		cubo_ativo = CuboMorte.instantiate()
	else:
		cubo_ativo = CuboPadrao.instantiate()
	
	var rand_x = rng.randf_range(-6.0, 6.0)
	var rand_y = rng.randf_range(-3.0, 3.0)
	var pos = Vector3(rand_x, rand_y, 0)
	cubo_ativo.position = pos
	
	add_child(cubo_ativo)
	
func _process(delta: float) -> void:
	if Input.is_action_just_pressed("ui_accept"):
		if cubo_ativo.name == "CuboMorte":
			print("Pontuação: " + str(pontos))
		else:
			pontos += 1
	
		novo_cubo()
	
