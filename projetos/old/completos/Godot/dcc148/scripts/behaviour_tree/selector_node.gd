class_name SelectorNode 
extends BehaviourTreeNode
	
func process() -> bool:
	for node in children:
		if node.process():
			return true
	return false
