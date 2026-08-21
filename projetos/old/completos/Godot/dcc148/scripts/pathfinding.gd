extends Node

func _ready() -> void:
	$Player/NavigationAgent3D.target_position = $Target.position

func _physics_process(delta: float) -> void:
	if not $Player/NavigationAgent3D.is_navigation_finished():
		$Player.position += $Player.position.direction_to($Player/NavigationAgent3D.get_next_path_position()) * delta
	
