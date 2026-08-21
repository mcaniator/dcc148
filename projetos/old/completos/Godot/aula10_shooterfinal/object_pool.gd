#extends Node
#
#@export var objeto : PackedScene
#@export var num_objetos : int
#
#var lista_objetos : Array[Node2D]
#
#func _ready() -> void:
	#lista_objetos.resize(num_objetos)
	#for i in range(num_objetos):
		#lista_objetos[i] = objeto.instantiate()
		#lista_objetos[i].name = "Tiro" + str(i)
		#lista_objetos[i].hide()
		#lista_objetos[i].process_mode = Node.PROCESS_MODE_DISABLED
		#add_child(lista_objetos[i])
		#
#func is_active(obj) -> bool:
	#return not obj.is_processing()
#
#func get_from_pool() -> Node2D:
	#var indice = lista_objetos.find_custom(is_active.bind())
	#if indice == -1:
		#return null
	#
	#var obj = lista_objetos.pop_at(indice)
	#lista_objetos.append(obj)
	#obj.show()
	#obj.process_mode = Node.PROCESS_MODE_ALWAYS
	#return obj

class_name ObjectPool

var objeto : PackedScene
var num_objetos : int
var lista_objetos : Array[Node2D]
var nome : String

func _init(objeto, num_objetos, nome, cena) -> void:
	self.objeto = objeto
	self.num_objetos = num_objetos
	self.nome = nome
	
	instancia_objetos(cena)

func instancia_objetos(cena) -> void:
	lista_objetos.resize(num_objetos)
	for i in range(num_objetos):
		lista_objetos[i] = objeto.instantiate()
		lista_objetos[i].name = nome + str(i)
		lista_objetos[i].hide()
		lista_objetos[i].process_mode = Node.PROCESS_MODE_DISABLED
		cena.add_child.call_deferred(lista_objetos[i])
		#cena.add_sibling.call_deferred(lista_objetos[i])
		
func esta_disponivel(obj) -> bool:
	return obj.process_mode == Node.PROCESS_MODE_DISABLED

func get_from_pool() -> Node2D:
	var indice = lista_objetos.find_custom(esta_disponivel.bind())
	if indice == -1:
		return null
	
	var obj = lista_objetos.pop_at(indice)
	lista_objetos.append(obj)
	obj.show()
	obj.process_mode = Node.PROCESS_MODE_ALWAYS
	return obj
