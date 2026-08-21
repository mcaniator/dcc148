@tool extends Node

enum CameraMode {Scroll, Follow, JumpSection, SmoothSection, Rect}

@export var mode : CameraMode = CameraMode.Scroll:
	set(value):
		if value == mode : return
		mode = value
		notify_property_list_changed()

var scroll_speed : float = 0.0
@onready var player : Node2D = $Player

func _get_property_list() -> Array[Dictionary]:
	var ret : Array[Dictionary]
	if Engine.is_editor_hint():
		match mode:
			CameraMode.Scroll:
				ret.append({
					"name": &"scroll_speed",
					"type": TYPE_FLOAT,
					"usage": PROPERTY_USAGE_DEFAULT
				})
			CameraMode.Follow:
				ret.append({
					"name": &"player",
					"type": TYPE_NODE_PATH
				})
	return ret
