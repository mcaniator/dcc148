extends Control

@export var title: String
@export var previous_slide: PackedScene
@export var next_slide: PackedScene

# Called when the node enters the scene tree for the first time.
func _ready() -> void:
	$Title.text = title


# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta: float) -> void:
	if Input.is_action_just_pressed("ui_right"):
		get_tree().change_scene_to_packed(next_slide)
	elif Input.is_action_just_pressed("ui_left"):
		get_tree().change_scene_to_packed(previous_slide)
