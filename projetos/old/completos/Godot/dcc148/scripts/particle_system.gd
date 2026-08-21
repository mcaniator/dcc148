class_name MyParticleSystem

extends Node3D

@export var particle_scene : PackedScene
@export var number_of_particles : int

var particles: Array[Particle] = []
var active: int = 0

func _ready() -> void:
	particles.resize(number_of_particles)
	print(number_of_particles)
	for i in range(number_of_particles):
		var obj = particle_scene.instantiate()
		particles[i] = Particle.new(obj, 2)
		print(particles[i])
		add_child(obj)
		
		particles[i].restart()

func _physics_process(delta: float) -> void:
	for i in range(number_of_particles):
		particles[i].update(delta)
		if !particles[i].active:
			particles[i].restart()
