extends CharacterBody2D

@export var speed: float

var bomb_scene = preload("res://bomb.tscn")
var pool: ObjectPool
var direction = Vector2.RIGHT

func _ready() -> void:
	pool = ObjectPool.new(bomb_scene, 5, "Bomb", get_tree().root)

func _physics_process(delta: float) -> void:
	var dx = Input.get_axis("move_left", "move_right")
	var dy = Input.get_axis("move_up", "move_down")
	
	direction.x = 1 if (dx >= 0) else -1
	direction.y = 1 if (dy <= 0) else -1
	
	var dv = Vector2(dx, dy).normalized() * speed * delta
	
	if Input.is_action_just_pressed("drop_bomb"):
		var bomb = pool.get_from_pool()
		if bomb:
			bomb.position = position + direction*16
	
	move_and_collide(dv)
	
	animate(dv)
	
func animate(dv: Vector2) -> void:
	if dv.x != 0:
		$AnimatedSprite2D.play("move_left_right")
		if dv.x > 0:
			$AnimatedSprite2D.flip_h = false
		else:
			$AnimatedSprite2D.flip_h = true
	elif dv.y != 0:
		if dv.y > 0:
			$AnimatedSprite2D.play("move_down")
		else:
			$AnimatedSprite2D.play("move_up")
	else:
		$AnimatedSprite2D.stop()
			
