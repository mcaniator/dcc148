extends Area2D

@export var velocidade : float

@export var objeto : PackedScene
@export var num_objetos : int
var pool : ObjectPool

func _ready() -> void:
	pool = ObjectPool.new(objeto, num_objetos, "Tiro", get_tree().root)

func _physics_process(delta: float) -> void:
	var dx = Input.get_axis("move_tras", "move_frente")
	var dy = Input.get_axis("move_cima", "move_baixo")
	
	var v = Vector2(dx, dy).normalized() * velocidade * delta
	
	translate(v)
	
	if Input.is_action_just_pressed("atirar"):
		var tiro = pool.get_from_pool()
		#var tiro = $ObjectPool.get_from_pool()
		if tiro:
			tiro.global_position = global_position
			
