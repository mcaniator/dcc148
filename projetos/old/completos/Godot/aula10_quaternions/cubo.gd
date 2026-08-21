extends CSGBox3D

@export var velocidade : float
@onready var pivot: Marker3D = $"../Pivot"

var rotacionar = false

func _process(delta: float) -> void:
	# EXEMPLO 1: gerando quatérnio a partir de ângulos de Euler
	var dx = Input.get_axis("ui_left", "ui_right")
	#var dy = Input.get_axis("ui_bottom", "ui_up")
	#var dz = 1
	#
	##rotate_x(dx * velocidade * delta)
	##rotate_y(dy * velocidade * delta)
	##rotate_z(dz * velocidade * delta)
	#
	#var rot = Vector3(dx, dy, dz) * velocidade * delta
	#quaternion *= Quaternion.from_euler(rot)
	
	var eixo_rotacao = Vector3(1, 0, 0).normalized()
	var angulo = deg_to_rad(-45)
	
	# EXEMPLO 2: gerando quatérnio a partir de um eixo de rotação e um ângulo
	#var q = Quaternion(eixo_rotacao, angulo)
	#quaternion *= q
	
	# EXEMPLO 3: rotacionando um ponto usando um quatérnio construído a partir de suas componentes
	#var pos = pivot.position
	#print("Antes: ", pos)
	#
	#var q = Quaternion(sin(angulo*0.5)*eixo_rotacao.x, sin(angulo*0.5)*eixo_rotacao.y, sin(angulo*0.5)*eixo_rotacao.z, cos(angulo*0.5))
	#var inv_q = q.inverse()	
	#var p = Quaternion(pos.x, pos.y, pos.z, 0)
	#
	#var rot_p = q * p * inv_q
	#
	#pos = Vector3(rot_p.x, rot_p.y, rot_p.z)
	#
	#print("Depois: ", pos)
	
	# EXEMPLO 4: rotacionando um objeto em torno de um ponto
	if Input.is_key_pressed(KEY_SPACE):
		rotacionar = true
	
	if rotacionar:
		#position = Vector3(0, 0, 2)
		#quaternion = Quaternion.IDENTITY
		
		var vp = position - pivot.position
		var qr = Quaternion(eixo_rotacao, dx * delta)
		
		quaternion = quaternion * qr
		var vr = qr * vp
		position = pivot.position + vr
		
		print("Resultado: ", position)
	
