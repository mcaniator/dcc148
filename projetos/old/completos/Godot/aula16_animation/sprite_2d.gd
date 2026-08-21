extends Sprite2D

var tween: Tween

var start = 0
var end = 1000
var t = 0

func _ready() -> void:
	pass
	#tween = create_tween()
	#tween.tween_property(self, "position", Vector2(250, 250), 1.0)
	#tween.tween_property(self, "position", Vector2(500, 0), 1.0)

func _process(delta: float) -> void:
	#position.x = (1-t) * start + t * end
	var s =  3*(t**2) - 2*(t**3)
	position.x = (1-s)*start + s * end
	t += delta * 0.5
	
	if t >= 1:
		t = 0
