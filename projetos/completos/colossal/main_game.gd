extends Control

var locations: Dictionary = {}
var travel_table: Dictionary = {}
var words: Dictionary = {}

var player_position = 1

func load_file() -> void:
	var file = FileAccess.open("res://cave.dat", FileAccess.READ)
	var line: String
	
	# Descarta a primeira linha (identificador da Seção 1)
	line = file.get_line()
		
	line = file.get_line()
	var parts = line.split("\t")
	var id = int(parts[0])
	var previous_id = 0
	while id != -1:
		print(parts)
		if id == previous_id:
			locations[id] += " " + parts[1]
		else:
			locations[id] = parts[1]
			previous_id = id
		
		line = file.get_line()
		parts = line.split("\t")
		id = int(parts[0])
	
	# Descarta a primeira linha (identiicador da Seção 3)
	# Obs.: a Seção 2, presente no arquivo do jogo original, foi suprimida no arquivo deste projeto
	line = file.get_line()
	print(line)
	
	line = file.get_line()
	parts = line.split("\t")
	var x = int(parts[0])
	while x != -1:
		var y = int(parts[1])
		var x_to_y = Vector2i(x, y)
		
		var valid_words = Array(parts.slice(2)).map(func(wid): return int(wid))
		travel_table[x_to_y] = valid_words
		print(travel_table[x_to_y])	
		
		line = file.get_line()
		parts = line.split("\t")
		x = int(parts[0])
	
	# Descarta a primeira linha (identiicador da Seção 4)
	line = file.get_line()
	print(line)
	
	line = file.get_line()
	parts = line.split("\t")
	id = int(parts[0])
	while id != -1:
		words[parts[1]] = id
		
		line = file.get_line()
		parts = line.split("\t")
		id = int(parts[0])

func update_label() -> void:
	
	$Label.text = locations[player_position].to_lower()
	
func move_player_to(y: int) -> void:
	if y <= 140:
		player_position = y
		update_label()
	else:
		$Label.text = "NOT IMPLEMENTED"

# Called when the node enters the scene tree for the first time.
func _ready() -> void:
	load_file()
	$LineEdit.grab_focus()
	update_label()


func _on_line_edit_text_submitted(new_text: String) -> void:
	var word_id = words.get(new_text.to_upper())
	var y: int
	if word_id:
		for goto: Vector2i in travel_table:
			if goto.x == player_position and word_id in travel_table[goto]:
				print("Found: ", goto.y)
				move_player_to(goto.y)
				break
				
	$LineEdit.text = ''
