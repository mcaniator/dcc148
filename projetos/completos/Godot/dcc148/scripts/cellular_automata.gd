extends Node

enum Neighborhood { Moore, VonNeumann }

@export var max_steps: int
@export var map_size: int # mapa quadrado
@export var neighborhood_size: int
@export var neighborhood_type: Neighborhood
@export var neighborhood_threshold: int

func _ready() -> void:
	generate()
	
func generate() -> void:
	# inicializa o mapa com estados aleatórios
	var map = []
	map.resize(map_size)
	for i in range(map_size):
		map[i] = []
		map[i].resize(map_size)
		for j in range(map_size):
			if randf() < 0.5:
				map[i][j] = true
			else:
				map[i][j] = false
	
	# aplica o algoritmo do autômato
	var output = map.duplicate_deep()
	for n in range(max_steps):
		for i in range(map_size):
			for j in range(map_size):
				if check_neighborhood(map, i, j):
					output[i][j] = true
				else:
					output[i][j] = false
		
		# inverte as grades para a próxima iteração
		var aux = map
		map = output
		output = aux
		
		var test = ''
		for i in range(map_size):
			for j in range(map_size):
				if map[i][j]:
					test += 'T'
				else:
					test += ' '
			test += '\n'
		
		print(test)
		
	
	# preenche o tile map
	for i in range(map_size):
		for j in range(map_size):
			if map[i][j]:
				$TileMapLayer.set_cell(Vector2i(i, j), 0, Vector2i(0, 2))
				
func check_neighborhood(map: Array, i: int, j: int) -> bool:
	var count = 0
	for di in range(-neighborhood_size, neighborhood_size+1):
		for dj in range(-neighborhood_size, neighborhood_size+1):
			
			# se vizinhança de von Neumann, só olha para os vizinhos nas direções ortogonais
			var neighborhood_type_condition = true
			if Neighborhood.VonNeumann:
				neighborhood_type_condition = (di == 0 or dj == 0)
			
			if check_indices(i+di, j+dj) and map[i+di][j+dj] and neighborhood_type_condition:
				count += 1
	
	return count >= neighborhood_threshold

func check_indices(i: int, j: int) -> bool:
	if i >= 0 and i < map_size and j >= 0 and j < map_size:
		return true
	else:
		return false
