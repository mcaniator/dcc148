extends Node

enum GenerationMethod { RandomWalk, Shaker, Subdivision }

@export var method: GenerationMethod
@export var max_steps: int
@export var map_size: int

func _ready() -> void:
	generate()

func generate() -> void:
	match method:
		GenerationMethod.RandomWalk: random_walk()
		GenerationMethod.Subdivision: 
			#for i in range(map_size):
				#for j in range(map_size):
					#$TileMapLayer.set_cell(Vector2i(i, j), 0, Vector2i(0, 2))
			subdivide(Rect2i(0, 0, map_size, map_size), max_steps)
		
func random_walk() -> void:
	var pos = Vector2i.ZERO
	
	for i in range(max_steps):
		var rng = randi_range(0, 3)
		match rng:
			0: pos.x += 1
			1: pos.x -= 1
			2: pos.y += 1
			3: pos.y -= 1
		
		$TileMapLayer.set_cell(pos, 0, Vector2i(0, 2))

func subdivide(region: Rect2i, level: int):
	print("Region: (", level, ")", region)
	if level == 1:
		var xc = region.position.x + randi() % region.size.x
		var yc = region.position.y + randi() % region.size.y
		var sx = int(region.size.x * 0.1) + randi() % int(region.size.x * 0.9)
		#var sy = randi() % region.size.y
		var sy = int(region.size.y * 0.1) + randi() % int(region.size.y * 0.9)
		@warning_ignore_start("integer_division")
		var x1 = clamp(xc - sx/2, region.position.x, region.end.x)
		var x2 = clamp(xc + sx/2, region.position.x, region.end.x)
		var y1 = clamp(yc - sy/2, region.position.y, region.end.y)
		var y2 = clamp(yc + sy/2, region.position.y, region.end.y)
		@warning_ignore_restore("integer_division")
		var room = Rect2(x1, y1, x2-x1, y2-y1)
		print(room)
		
		create_tile_room(region, room)
		
	else:
		if randf() < 0.5: # divide em y
			#var split_pos = region.position.x + randi() % region.size.x
			var split_pos = region.position.x + int(region.size.x * 0.25) + randi() % int(region.size.x * 0.5)
			var left_region = Rect2i(region.position.x, region.position.y, split_pos - region.position.x, region.size.y)
			var right_region = Rect2i(split_pos+1, region.position.y, region.end.x - split_pos-1, region.size.y)
			subdivide(left_region, level-1)
			subdivide(right_region, level-1)
		else: # divide em x
			#var split_pos = region.position.y + randi() % region.size.y
			var split_pos = region.position.y + int(region.size.y * 0.25) + randi() % int(region.size.y * 0.5)
			var top_region = Rect2i(region.position.x, region.position.y, region.size.x, split_pos - region.position.y)
			var bottom_region = Rect2i(region.position.x, split_pos+1, region.size.x, region.end.y - split_pos-1)
			subdivide(top_region, level-1)
			subdivide(bottom_region, level-1)

func create_tile_room(region: Rect2i, room: Rect2i) -> void:
	var random_tile_index = randi() % 8
	@warning_ignore_start("integer_division")
	var random_tile = Vector2(random_tile_index % 5, 2 + random_tile_index / 5)
	@warning_ignore_restore("integer_division")
	for i in range(region.position.x, region.end.x+1):
		for j in range(region.position.y, region.end.y+1):
			var tile = Vector2(i, j)
			if room.has_point(tile):
				$TileMapLayer.set_cell(tile)
			else:
				$TileMapLayer.set_cell(tile, 0, random_tile)
	#for i in range(room.position.x, room.end.x+1):
		#for j in range(room.position.y, room.end.y+1):
			#$TileMapLayer.set_cell(Vector2i(i, j))
