extends RigidBody3D

@export var material: Material

func _ready() -> void:
	$MeshInstance3D.material_override = material
