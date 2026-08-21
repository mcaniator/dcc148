extends Node

enum LandscapeGenerationMethod { 
	ValueNoiseRaw, 
	ValueNoiseBilinear, 
	ValueNoiseBicubic,
	PerlinNoise,
	FaultFormation,
	DiamondSquare
}

@export var method: LandscapeGenerationMethod
@export var map_size: int
@export var amplitude: float
@export var sample_size: int
@export var frequency: float
@export var max_iterations: int
@export var height_damping: float
@export var roughness: float

func _ready() -> void:
	generate()

func generate() -> void:
	match method:
		LandscapeGenerationMethod.ValueNoiseRaw: value_noise(0)
		LandscapeGenerationMethod.ValueNoiseBilinear: value_noise(1)
		LandscapeGenerationMethod.ValueNoiseBicubic: value_noise(2)
		LandscapeGenerationMethod.PerlinNoise: perlin_noise()
		LandscapeGenerationMethod.FaultFormation: fault_formation()
		LandscapeGenerationMethod.DiamondSquare: diamond_square()
	
	$Terrain3D.data.update_maps()


func value_noise(interpolation_method: int) -> void:
	for i in range(map_size):
		for j in range(map_size):
			if interpolation_method == 0:
				$Terrain3D.data.set_height(Vector3(i, 0, j), randf() * amplitude)
			elif i % sample_size == 0 and j % sample_size == 0:
				$Terrain3D.data.set_height(Vector3(i, 0, j), randf() * amplitude)
			else:
				$Terrain3D.data.set_height(Vector3(i, 0, j), 0)
	
	if interpolation_method == 1:
		bilinear_interpolation()
	else:
		bicubic_interpolation()

func bilinear_interpolation() -> void:
	pass

func bicubic_interpolation() -> void:
	pass

func perlin_noise() -> void:
	var p = FastNoiseLite.new()
	p.noise_type = FastNoiseLite.TYPE_PERLIN
	p.frequency = frequency
	
	for i in range(map_size):
		for j in range(map_size):
			var x = float(i) / map_size
			var y = float(j) / map_size
			
			#print(p.get_noise_2d(x, y))
			$Terrain3D.data.set_height(Vector3(i, 0, j), amplitude * p.get_noise_2d(x, y))

func fault_formation() -> void:
	var h = 1.0
	for n in range(max_iterations):
		var p0 = Vector3(randf_range(0, map_size-1), randf_range(0, map_size-1), 0)
		var p1 = Vector3(randf_range(0, map_size-1), randf_range(0, map_size-1), 0)
		var line_vector = p1 - p0
		for i in range(map_size):
			for j in range(map_size):
				var pos = Vector3(i, j, 0)
				var point_to_line = pos - p0
				var cross = line_vector.cross(point_to_line)
				
				if cross.z < 0:
					var height = $Terrain3D.data.get_height(Vector3(i, 0, j)) + h * amplitude
					$Terrain3D.data.set_height(Vector3(i, 0, j), height)
		h -= height_damping

func diamond_square() -> void:
	var h = 1.0
	
	$Terrain3D.data.set_height(Vector3(0, 0, 0), randf())
	$Terrain3D.data.set_height(Vector3(map_size-1, 0, 0), randf())
	$Terrain3D.data.set_height(Vector3(0, 0, map_size-1), randf())
	$Terrain3D.data.set_height(Vector3(map_size-1, 0, map_size-1), randf())
	
	diamond_square_step(map_size, h)

func diamond_square_step(size: int, h: float) -> void:
	@warning_ignore_start("integer_division")
	print(h)
	if size > 1:
		for i in range(0, map_size-1, size):
			for j in range(0, map_size-1, size):
				var min_x = i
				var min_y = j
				var max_x = min_x + size
				var max_y = min_y + size
				
				var ci = (min_x + max_x) / 2
				var cj = (min_y + max_y) / 2
				
				# passo do diamante
				var hld = $Terrain3D.data.get_height(Vector3(min_x, 0, min_y))
				var hlu = $Terrain3D.data.get_height(Vector3(min_x, 0, max_y))
				var hrd = $Terrain3D.data.get_height(Vector3(max_x, 0, min_y))
				var hru = $Terrain3D.data.get_height(Vector3(max_x, 0, max_y))
				var height = (hld + hlu + hrd + hru) / 4 + randf_range(-h/2, h/2) * amplitude
				
				$Terrain3D.data.set_height(Vector3(ci, 0, cj), height)
		
		for i in range(0, map_size-1, size):
			for j in range(0, map_size-1, size):
				var min_x = i
				var min_y = j
				var max_x = min_x + size
				var max_y = min_y + size
				var ci = (min_x + max_x) / 2
				var cj = (min_y + max_y) / 2
				
				var hld = $Terrain3D.data.get_height(Vector3(min_x, 0, min_y))
				var hlu = $Terrain3D.data.get_height(Vector3(min_x, 0, max_y))
				var hrd = $Terrain3D.data.get_height(Vector3(max_x, 0, min_y))
				var hru = $Terrain3D.data.get_height(Vector3(max_x, 0, max_y))
				var hc = $Terrain3D.data.get_height(Vector3(ci, 0, cj))
				
				# passo do quadrado (4 vizinhos com bordas periódicas)
				var hlc_add = $Terrain3D.data.get_height(Vector3(map_size + (ci - size) if (ci-size) < 0 else (ci-size), 0, cj))
				var hcd_add = $Terrain3D.data.get_height(Vector3(ci, 0, map_size + (cj - size) if (cj-size) < 0 else (cj-size)))
				var hrc_add = $Terrain3D.data.get_height(Vector3((ci + size) % map_size, 0, cj))
				var hcu_add = $Terrain3D.data.get_height(Vector3(ci, 0, (cj + size) % map_size))
				
				var hlc = (hld + hlu + hc + hlc_add) / 4 + randf_range(-h/2, h/2) * amplitude
				var hcd = (hld + hrd + hc + hcd_add) / 4 + randf_range(-h/2, h/2) * amplitude
				var hrc = (hrd + hru + hc + hrc_add) / 4 + randf_range(-h/2, h/2) * amplitude
				var hcu = (hlu + hru + hc + hcu_add) / 4 + randf_range(-h/2, h/2) * amplitude
				
				# passo do quadrado (3 vizinhos)				
				#var hlc = (hld + hlu + hc) / 3 + randf_range(-h/2, h/2) * amplitude
				#var hcd = (hld + hrd + hc) / 3 + randf_range(-h/2, h/2) * amplitude
				#var hrc = (hrd + hru + hc) / 3 + randf_range(-h/2, h/2) * amplitude
				#var hcu = (hlu + hru + hc) / 3 + randf_range(-h/2, h/2) * amplitude
				
				$Terrain3D.data.set_height(Vector3(min_x, 0, cj), hlc)
				$Terrain3D.data.set_height(Vector3(ci, 0, min_y), hcd)
				$Terrain3D.data.set_height(Vector3(max_x, 0, cj), hrc)
				$Terrain3D.data.set_height(Vector3(ci, 0, max_y), hcu)
		
		h *= pow(2.0, -roughness)
		
		diamond_square_step(size / 2, h)
		@warning_ignore_restore("integer_division")
