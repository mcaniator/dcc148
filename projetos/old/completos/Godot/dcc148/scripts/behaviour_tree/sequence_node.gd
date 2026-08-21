class_name SequenceNode 
extends BehaviourTreeNode
	
func process() -> bool:
	for node in children:
		if !node.process():
			return false
	return true
