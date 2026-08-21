extends Camera2D

enum CameraMode {Scroll, Follow, JumpSection, SmoothSection, Rect}

@export var mode : CameraMode = CameraMode.Scroll
		
@export_group("Scroll Camera")
@export var scroll_speed : float = 0.0
@export_group("Follow Camera")
@export var player : Node2D

func _process(delta: float) -> void:
	match mode:
		CameraMode.Scroll: scroll(delta)
		CameraMode.Follow: follow()
		CameraMode.JumpSection: jump()
		CameraMode.SmoothSection: pass
		CameraMode.Rect: pass
		
func scroll(delta: float) -> void:
	pass

func follow() -> void:
	pass
	
func jump() -> void:
	pass
