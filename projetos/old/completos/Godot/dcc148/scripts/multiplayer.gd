extends Node

var IP_ADDRESS: String = '127.0.0.1'
var PORT: int = 3000
var MAX_CLIENTS: int = 5

func _ready() -> void:	
	multiplayer.peer_connected.connect(_on_player_connected)
	multiplayer.peer_disconnected.connect(_on_player_disconnected)
	multiplayer.connected_to_server.connect(_on_connected_ok)
	multiplayer.connection_failed.connect(_on_connected_fail)
	multiplayer.server_disconnected.connect(_on_server_disconnected)

func _on_player_connected() -> void:
	pass

func _on_player_disconnected() -> void:
	pass

func _on_connected_ok() -> void:
	pass

func _on_connected_fail() -> void:
	pass

func _on_server_disconnected() -> void:
	pass
	
func _exit_tree() -> void:
	multiplayer.multiplayer_peer = OfflineMultiplayerPeer.new()
	
func _on_server_connect_button_pressed() -> void:
	var peer = ENetMultiplayerPeer.new()
	peer.create_server(PORT, MAX_CLIENTS)
	multiplayer.multiplayer_peer = peer
	
	print(multiplayer.get_unique_id())
	
	$Overlay.visible = false

func _on_client_connect_button_pressed() -> void:
	var peer = ENetMultiplayerPeer.new()
	peer.create_client(IP_ADDRESS, PORT)
	multiplayer.multiplayer_peer = peer
	
	print(multiplayer.get_unique_id())
	
	$Overlay.visible = false
