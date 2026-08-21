extends Node

@export var player_scene: PackedScene

var IP_ADDRESS: String = '127.0.0.1'
var PORT: int = 3000
var MAX_CLIENTS: int = 5

var connected_players = []

func _ready() -> void:
	multiplayer.peer_connected.connect(_on_player_connected)
	multiplayer.peer_disconnected.connect(_on_player_disconnected)
	multiplayer.connected_to_server.connect(_on_connected_ok)
	multiplayer.connection_failed.connect(_on_connected_fail)
	multiplayer.server_disconnected.connect(_on_server_disconnected)
	
	$MultiplayerSpawner.set_spawn_function(spawner)

@rpc("any_peer", "call_local")
func start_game() -> void:
	print("Starting...")
	
	$Overlay.visible = false
	
	#$MultiplayerSpawner.spawn([])
	
	#print("Player ID ", multiplayer.get_unique_id(), ": ", connected_players.size())
	#for id in connected_players:
		#var rand_pos = Vector3(randf_range(-2, 2), 0, randf_range(2, 2))
		#add_player.rpc(id, rand_pos)
	
@rpc("any_peer", "call_local")
func broadcast_to_server(id: int):
	connected_players.append(id)

@rpc("any_peer", "call_local")
func add_player(id: int, rand_pos: Vector3) -> void:
	if not multiplayer.is_server():
		connected_players.append(id)
	var player = player_scene.instantiate()
	player.name = str(id)
	add_child(player)
	player.global_position = rand_pos
	print("Player ID ", id, " joined the match")
	
func spawner() -> Node:
	return player_scene.instantiate()

func _on_player_connected(id: int) -> void:
	print("Player connected with ID ", id)
	#broadcast_to_server.rpc_id(1, id)

func _on_player_disconnected(id: int) -> void:
	print("Player ID ", id, " disconnected")

func _on_connected_ok() -> void:
	print("Connected to server")

func _on_connected_fail() -> void:
	print("Connection failed")

func _on_server_disconnected() -> void:
	print("Server disconnected")

func _on_host_pressed() -> void:
	var peer = ENetMultiplayerPeer.new()
	peer.create_server(PORT, MAX_CLIENTS)
	multiplayer.set_multiplayer_peer(peer)
	
	print(multiplayer.get_unique_id())

func _on_join_pressed() -> void:
	var peer = ENetMultiplayerPeer.new()
	peer.create_client(IP_ADDRESS, PORT)
	multiplayer.set_multiplayer_peer(peer)
	
	print(multiplayer.get_unique_id())

func _on_start_pressed() -> void:
	start_game.rpc()
