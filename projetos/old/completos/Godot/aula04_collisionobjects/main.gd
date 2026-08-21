extends Node

func desativar_todos() -> void:
	$Area/Objeto.desativar()
	$Area2/Objeto.desativar()
	$CharacterBody/Objeto.desativar()
	$CharacterBody2/Objeto.desativar()
	$StaticBody/Objeto.desativar()
	$StaticBody2/Objeto.desativar()
	$RigidBody/Objeto.desativar()
	$RigidBody2/Objeto.desativar()

func _process(delta: float) -> void:		
	if Input.is_key_pressed(KEY_1):
		desativar_todos()
		$Area/Objeto.ativar()
	elif Input.is_key_pressed(KEY_2):
		desativar_todos()
		$Area2/Objeto.ativar()
	elif Input.is_key_pressed(KEY_3):
		desativar_todos()
		$CharacterBody/Objeto.ativar()
	elif Input.is_key_pressed(KEY_4):
		desativar_todos()
		$CharacterBody2/Objeto.ativar()
	elif Input.is_key_pressed(KEY_5):
		desativar_todos()
		$StaticBody/Objeto.ativar()
	elif Input.is_key_pressed(KEY_6):
		desativar_todos()
		$StaticBody2/Objeto.ativar()
	elif Input.is_key_pressed(KEY_7):
		desativar_todos()
		$RigidBody/Objeto.ativar()
	elif Input.is_key_pressed(KEY_8):
		desativar_todos()
		$RigidBody2/Objeto.ativar()
