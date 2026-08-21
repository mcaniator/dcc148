extends CharacterBody3D

@export var speed: float
@export var player: Node3D

var behaviour_tree: BehaviourTreeNode

func _ready() -> void:
	behaviour_tree = create_behaviour_tree()
	
func create_behaviour_tree() -> BehaviourTreeNode:
	var alternate_sequence = SequenceNode.new()
	alternate_sequence.add_child(LowHP.new())
	alternate_sequence.add_child(Hide.new())
		
	var main_sequence = SequenceNode.new()
	main_sequence.add_child(PlayerVisible.new())
	main_sequence.add_child(Walk.new())
		
	var root = SelectorNode.new()
	root.add_child(alternate_sequence)
	root.add_child(main_sequence)
	
	return root


func _physics_process(delta: float) -> void:
	behaviour_tree.process()
	
	$NavigationAgent3D.target_position = player.position
	var motion_vector = position.direction_to($NavigationAgent3D.get_next_path_position()) * speed * delta
	move_and_collide(motion_vector)
