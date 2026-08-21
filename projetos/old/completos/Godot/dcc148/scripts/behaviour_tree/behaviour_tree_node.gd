@abstract
class_name BehaviourTreeNode
extends Object
	
var children = []
	
@abstract
func process() -> bool

func add_child(node: BehaviourTreeNode):
	children.append(node)
