class_name Particle

var obj : Node3D
var lifetime: float
var time_left: float
var active: bool
var speed: float

func _init(p_obj: Node3D, p_lifetime: float) -> void:
	obj = p_obj
	lifetime = p_lifetime
	time_left = lifetime
	active = false

func restart() -> void:
	obj.position = Vector3.ZERO
	obj.quaternion = Quaternion.IDENTITY
	obj.rotate_z(randf_range(deg_to_rad(-45), deg_to_rad(45)))
	speed = randf_range(1, 10)
	time_left = lifetime
	active = true

func update(dt: float) -> void:
	if active:
		obj.position += obj.basis.y * speed * dt
		time_left -= dt
		if time_left <= 0:
			restart()
