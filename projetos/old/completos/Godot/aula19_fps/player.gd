extends Camera3D

@export var eye_speed: float
@export var speed: float

var base_orientation: Quaternion
var mouse_h = 0
var mouse_v = 0

@onready var bullet_scene = preload("res://bullet.tscn")

func _ready() -> void:
	base_orientation = quaternion

func _input(event: InputEvent) -> void:
	if event is InputEventMouseMotion:
		mouse_h = clampf(mouse_h + event.relative.x, -90, 90)
		mouse_v = clampf(mouse_v + event.relative.y, -90, 90)

func _physics_process(delta: float) -> void:
	# movimento do jogador no plano XZ
	var dx = Input.get_axis("move_left", "move_right")
	var dz = Input.get_axis("move_forward", "move_backward")
	
	# orienta a visão do jogador
	var angle_y = mouse_h * eye_speed * delta
	var angle_x = mouse_v * eye_speed * delta
	var rot_y = Quaternion(Vector3.UP, -angle_y)
	var rot_x = Quaternion(Vector3.LEFT, angle_x)
	
	# a aplicação das rotações foi quebrada em 2 partes para que a rotação em torno do eixo x
	# não afete o deslocamento do jogador no plano XZ (caso contrário, a direção forward do
	# jogador poderia se confundir com a direção forward da câmera)
	quaternion = base_orientation * rot_y
	translate(Vector3(dx, 0, dz).normalized() * speed * delta)
	quaternion *= rot_x
	
	if Input.is_action_just_pressed("shoot"):
		shoot()

func shoot() -> void:
	var bullet = bullet_scene.instantiate()
	bullet.position = position
	bullet.dir = -basis.z
	get_tree().root.add_child(bullet)
	
