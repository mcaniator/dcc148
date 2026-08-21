extends Camera2D

enum CameraMode {Scroll, Follow, JumpSection, SmoothSection, Rect}

@export var mode : CameraMode = CameraMode.Scroll:
	set(value):
		if value == mode : return
		mode = value
		notify_property_list_changed()
		
@export_group("Scroll Camera")
@export var scroll_speed : float = 0.0
@export_group("Follow Camera")
@export var player : Node2D
@export_group("Smooth Section Camera")
@export var adjustment_speed : float

func _process(delta: float) -> void:
	match mode:
		CameraMode.Scroll: scroll(delta)
		CameraMode.Follow: follow()
		CameraMode.JumpSection: jump()
		CameraMode.SmoothSection: smooth(delta)
		CameraMode.Rect: rect()
		
func scroll(delta: float) -> void:
	translate(Vector2.RIGHT * scroll_speed * delta)

func follow() -> void:
	position.x = player.position.x
	
func jump() -> void:
	var viewport = get_viewport_rect()
	
	var screen_number = int((player.position.x - limit_left) / viewport.size.x)
	position.x = limit_left + screen_number * viewport.size.x

func smooth(delta: float) -> void:
	var viewport = get_viewport_rect()
	
	var player_screen_number = int((player.position.x - limit_left) / viewport.size.x)
	var camera_screen_number = int((position.x - limit_left) / viewport.size.x)
	
	print("Player: ", player_screen_number, " - Camera: ", camera_screen_number)
	
	if player_screen_number != camera_screen_number:
		position.x += adjustment_speed * delta * (player_screen_number - camera_screen_number)

func rect() -> void:
	var viewport = get_viewport_rect()
	
	var screen_left = position.x - viewport.size.x * 0.5
	var screen_right = position.x + viewport.size.x * 0.5
	
	var right_border = screen_left + viewport.size.x * 0.75
	var left_border = screen_left + viewport.size.x * 0.25
	
	print(player.position.x)
	print("Left: ", left_border, " - Right: ", right_border)
	
	var dx = 0
	if player.position.x > right_border:
		dx = player.position.x - right_border
	elif player.position.x < left_border:
		dx = player.position.x - left_border
	
	position.x = clampf(position.x + dx, left_border, right_border)
	
