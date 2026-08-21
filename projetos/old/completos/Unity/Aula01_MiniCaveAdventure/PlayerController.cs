using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Classe que controla o jogador.
/// 
/// Parametrizacão dos objetos de jogo:
/// DisplayText (objeto do Canvas)
/// - Componente "Rect Transform"
/// ==> PosX, PosY, PosZ: 0
/// ==> Width: 500
/// ==> Height: 100
/// - Componente "Text"
/// ==> Vertical Overflow: Overflow
/// ==> Alignment: Centralizado (horizontalmente e verticalmente)
/// 
/// UserInput (objeto do Canvas)
/// - Componente "Rect Transform"
/// ==> PosX, PosZ: 0
/// ==> PosY: -200
/// ==> Width: 900
/// ==> Height: 40
/// 
/// Placeholder (objeto do UserInput)
/// - Componente "Text"
/// ==> Text: "Digite o comando..."
/// 
/// Main Camera:
/// - Componente "Camera"
/// ==> Clear Flags: Solid Color
/// ==> Background: Preto
/// - Componente "Player Controller (Script)"
/// ==> Display: DisplayText (Text)
/// ==> User Input: UserInput (InputField)
/// </summary>
public class PlayerController : MonoBehaviour {

	int playerPosition = 0;			// armazena o id do local onde o jogador se encontra
	List<Location> locations;		// lista com todos os locais importados do arquivo
	Dictionary<string, int> words;  // dicionário associando as palavras válidas aos seus respectivos ids

	public Text display;			// objeto usado para exibir informacão de jogo na tela
	public InputField userInput;	// objeto usado para capturar os comandos do usuário

	// Use this for initialization
	void Start () {
		// Cria a lista de locais e carrega os dados do arquivo, usando as funcões de manipulacão de arquivos do C#
		locations = new List<Location> ();
		StreamReader file = File.OpenText ("Assets/Resources/cave.dat");
		
		LoadLongDescriptions(file);
		LoadShortDescriptions(file);
		LoadTravelTable(file);

		// Cria a tabela de palavras de ação e carrega a quarta secão do arquivo
		words = new Dictionary<string, int>();
		LoadActionWords(file);

		// Coloca o foco na caixa de texto
		userInput.Select ();
		userInput.ActivateInputField ();

		// Jogo iniciado com a descrição longa da localização inicial
		display.text = locations[playerPosition].LongDescription;

		file.Close ();
	}
	
	/// <summary>
	/// Carrega as descrições longas do arquivo
	/// </summary>
	/// <para>
	/// Lê o arquivo linha por linha, enquanto não encontrar uma linha contendo "-1", que designa fim de secão.
	/// Para cada linha lida, quebra a string em partes usando a funcão Split() e adiciona a segunda parte
	/// (que representa a descricão do local) na lista. A maioria das descrições ocupa mais de uma linha, razão
	/// pela qual a concatenação é realizada. O índice do local é representado aqui pelo próprio índice da lista
	/// (ou seja, a localização X está armazenada no índice X-1).
	/// </para>
	/// <param name="file">Arquivo contendo os dados do jogo</param>	
	void LoadLongDescriptions(StreamReader file)
	{
		string line;
		line = file.ReadLine(); // lê a primeira linha, que indica o número da seção
		Debug.Log("Reading Section " + line + "...");
		line = file.ReadLine();
		string[] parts = line.Split('\t');
		int previous = 1;
		string description = "";
		while (line != null && parts[0] != "-1") 
		{
			int current = int.Parse(parts[0]);
			if(current == previous)
				description += parts[1] + " ";
			else
			{
				locations.Add(new Location(description));
				description = parts[1] + " ";
			}
			line = file.ReadLine();
			parts = line.Split('\t');
			previous = current;
		}
		locations.Add(new Location(description)); // adiciona a última localização
		Debug.Log("Finished reading section");
	}

	/// <summary>
	/// Carrega as descrições curtas do arquivo
	/// </summary>
	/// <para>
	/// Lê o arquivo linha por linha, enquanto não encontrar uma linha contendo "-1", que designa fim de secão.
	/// Para cada linha lida, quebra a string em partes usando a funcão Split() e adiciona a segunda parte
	/// (que representa a descricão do local) na lista. O índice do local é representado aqui pelo próprio
	/// índice da lista (ou seja, a localização X está armazenada no índice X-1)
	/// </para>
	/// <param name="file">Arquivo contendo os dados do jogo</param>
	void LoadShortDescriptions(StreamReader file)
	{
		string line;
		line = file.ReadLine(); // lê a primeira linha, que indica o número da seção
		Debug.Log("Reading Section " + line + "...");
		line = file.ReadLine();
		string[] parts = line.Split('\t');
		while (line != null && parts[0] != "-1") 
		{
			int locationId = int.Parse(parts[0])-1;
			locations[locationId].ShortDescription = parts[1];
			line = file.ReadLine();
			parts = line.Split('\t');
		}
		Debug.Log("Finished reading section");
	}

	/// <summary>Carrega a tabela de deslocamentos do arquivo</summary>
	/// <para>
	/// Lê o arquivo linha por linha, enquanto não encontrar uma linha contendo "-1", que designa fim de secão.
	/// Para cada linha lida, quebra a string em partes usando a funcão Split(). A primeira parte é o índice da
	/// localização atual do jogador, representado aqui pelo próprio índice da lista (ou seja, a localização X 
	/// está armazenada no índice X-1). As partes seguintes representam as informações de deslocamento, que são
	/// tratadas pela classe Location.
	/// </para>
	/// <param name="file">Arquivo contendo os dados do jogo</param>
	void LoadTravelTable(StreamReader file)
	{
		string line;
		line = file.ReadLine(); // lê a primeira linha, que indica o número da seção
		Debug.Log("Reading Section " + line + "...");
		line = file.ReadLine();
		string[] parts = line.Split('\t');
		while (line != null && parts[0] != "-1") 
		{
			int locationId = int.Parse(parts[0])-1;
			locations[locationId].AddTravelInfo(parts);
			line = file.ReadLine();
			parts = line.Split('\t');
		}
		Debug.Log("Finished reading section");
	}

	/// <summary> Carrega a tabela de palavras que designam ações válidas </summary>
	/// <para>
	/// Lê o arquivo linha por linha, enquanto não encontrar uma linha contendo "-1", que designa fim de secão.
	/// Para cada linha lida, quebra a string em partes usando a funcão Split(). A primeira parte é o índice de
	/// uma ação. A segunda parte é a palavra (string) que descreve aquela ação. Como uma mesma ação (mesmo id) pode ser
	/// descrita por mais de uma palavra (strings diferentes), essa associação é armazenada através de um dicionário
	/// com chaves do tipo string e valores associados inteiros. Ex.: tanto a palavra "NORTH" quanto "N" indicam uma ação
	/// de deslocamento para o norte. Neste caso, ambas as entradas no dicionário (words["NORTH"] e words["N"]) possuem
	/// valor associado 45 (conforme arquivo cave.dat).
	/// </para>
	/// <param name="file">Arquivo contendo os dados do jogo</param>
	void LoadActionWords(StreamReader file)
	{
		
		string line;
		line = file.ReadLine(); // lê a primeira linha, que indica o número da seção
		Debug.Log("Reading Section " + line + "...");
		line = file.ReadLine();
		string[] parts = line.Split('\t');
		while (line != null && parts[0] != "-1") 
		{
			int wordId = int.Parse(parts[0]);
			words[parts[1]] = wordId;
			line = file.ReadLine();
			parts = line.Split('\t');
		}
		Debug.Log("Finished reading section");
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Return)) {
			Debug.Log("POSITION ANTES: " + playerPosition);
			
			// =================================================================

			// Implemente aqui a sua solução
			// Nesse caso específico, o if garante que só executaremos algo quando o usuário
			// pressionar Enter para enviar o comando que designa a ação.

			string capsInput = userInput.text.ToUpper();

			if(capsInput == "MORE")
				display.text = locations[playerPosition].LongDescription;
			else if(words.ContainsKey(capsInput))
			{
				int wordId = words[capsInput];
				print(wordId);
				int destinationId = locations[playerPosition].FindDestination(wordId);
				print("Destination" + destinationId);
				if(destinationId != -1)
					playerPosition = destinationId;
				
				display.text = locations[playerPosition].LongDescription;
			}

			Debug.Log("POSITION DEPOIS: " + playerPosition);

			// =================================================================
		}
	}
}
