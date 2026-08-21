extends Node

@export var speed: float

func _process(delta: float) -> void:	
	var dist = $Camera3D.position.z - $Cube.position.z
	var fov_div_2 = deg_to_rad($Camera3D.fov * 0.5)
	var half_height = dist * tan(fov_div_2)
	
	var viewport_size = Vector2(get_viewport().size)
	var aspect = viewport_size.x / viewport_size.y
	var half_width = half_height * aspect
	
	#print(halfWidth, " - ", halfHeight)
	
	var dx = Input.get_axis("ui_left", "ui_right")
	var dy = Input.get_axis("ui_up", "ui_down")
	var dz = Input.get_axis("ui_home", "ui_end")
	
	var dv = Vector3(dx, dy, dz).normalized() * speed * delta
	
	var limit_left = $Camera3D.position.x - half_width
	var limit_right = $Camera3D.position.x + half_width
	var limit_top = $Camera3D.position.y - half_height
	var limit_bottom = $Camera3D.position.y + half_height
	$Cube.position.x = clampf($Cube.position.x + dv.x, limit_left, limit_right)
	$Cube.position.y = clampf($Cube.position.y + dv.y, limit_top, limit_bottom)
	$Cube.position.z += dv.z
