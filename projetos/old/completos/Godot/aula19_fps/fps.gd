extends Node

@onready var enemy_scene = preload("res://enemy.tscn")

func _ready() -> void:
	Input.mouse_mode = Input.MOUSE_MODE_HIDDEN

func _on_timer_timeout() -> void:
	var enemy = enemy_scene.instantiate()
	enemy.position.x = randf_range(-10, 10)
	enemy.position.y = randf_range(0, 5)
	enemy.position.z = -10
	add_child(enemy)
