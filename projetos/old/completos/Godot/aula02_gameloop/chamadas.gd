extends Node2D

func _init():
	print(name + ': Init')

func _ready():
	print(name + ': Ready')
	
func _process(delta: float) -> void:
	print(name + ': Process')
	
func _physics_process(delta: float) -> void:
	print(name + ': Physics Process')
	
func _draw() -> void:
	print(name + ': Draw')

func _input(event: InputEvent) -> void:
	print(name + ': Input')
