extends Node

enum Method { Circle, AABB, CircleAABB };

@export var method : Method

func _process(delta: float) -> void:
	var dx = Input.get_axis("ui_left", "ui_right")
	var dy = Input.get_axis("ui_up", "ui_down")
	
	$Object1.position += Vector2(dx, dy).normalized() * delta * 300
	
	if check_collision():
		print("Colidiu!")
		
func check_collision() -> bool:
	match method:
		Method.Circle: return circle()
		Method.AABB: return aabb()
		Method.CircleAABB: return circle_aabb()
	
	return false

func circle() -> bool:
	var c1 = $Object1.position
	var c2 = $Object2.position
	var r1 = $Object1/CollisionShape2D.shape.get_rect().size.x * 0.5
	var r2 = $Object2/CollisionShape2D.shape.get_rect().size.x * 0.5
	
	if c1.distance_squared_to(c2) <= (r1+r2)*(r1+r2):
		return true
	else:
		return false

func aabb() -> bool:
	var r1 = $Object1/CollisionShape2D.shape.get_rect()
	var r2 = $Object2/CollisionShape2D.shape.get_rect()
	
	var min1 = $Object1.position + r1.position
	var max1 = $Object1.position + r1.end
	var min2 = $Object2.position + r2.position
	var max2 = $Object2.position + r2.end
	
	#var check_x = (min1.x >= min2.x and min1.x <= max2.x) or \
				  #(max1.x <= max2.x and max1.x >= min2.x)
	#var check_y = (min1.y >= min2.y and min1.y <= max2.y) or \
				  #(max1.y <= max2.y and max1.y >= min2.y)
	
	var check_x = max1.x >= min2.x and min1.x <= max2.x
	var check_y = max1.y >= min2.y and min1.y <= max2.y
	
	return check_x and check_y

func circle_aabb() -> bool:
	var circle_to_aabb = ($Object2.position - $Object1.position).normalized
	var extreme_pt = $Object1.position + circle_to_aabb * $Object1/CollisionShape2D.shape.get_rect().size.x * 0.5
	
	var r2 = $Object2/CollisionShape2D.shape.get_rect()
	var a2 = $Object2.position + r2.position
	var b2 = $Object2.position + r2.end
	
	if extreme_pt.x >= a2.x and extreme_pt.x <= b2.x and \
	   extreme_pt.y <= b2.y and extreme_pt.y >= a2.y:
		return true
	else:
		return false
	
	
	
	
	
	
	
	
	
