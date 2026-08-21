extends CharacterBody2D

@onready var background: TileMapLayer = $"../Background"

func _physics_process(_delta: float) -> void:
	var dx = Input.get_axis("ui_left", "ui_right")
	var dy = Input.get_axis("ui_up", "ui_down")
	
	var dv = Vector2(dx, dy).normalized() * 1000
	
	velocity = dv
	move_and_slide()
	
	print(background.local_to_map(global_position))
