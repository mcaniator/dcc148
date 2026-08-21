extends Node2D

@export var velocidade : float

var ativo = false

func esta_ativo() -> bool:
	return ativo

func ativar() -> void:
	ativo = true

func desativar() -> void:
	ativo = false
