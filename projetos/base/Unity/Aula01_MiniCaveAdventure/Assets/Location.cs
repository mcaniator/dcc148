using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Classe que implementa uma localização acessível pelo jogador
/// </summary>
public class Location
{
    private string shortDescription;                    // descrição curta do local
    private string longDescription;                     // descrição longa do local
    private Dictionary<int, List<int>> travelTable;     // dicionário mapeando um local de destino com uma lista de ids de palavras que levam a este destino

    /// <summary>
    /// Construtor que cria uma localização nula
    /// </summary>
    public Location()
    {
        shortDescription = "";
        longDescription = "";
        travelTable = new Dictionary<int, List<int>>();
    }

    /// <summary>
    /// Construtor que cria uma localização válida (pode ou não ter descrição curta), com uma tabela de deslocamentos vazia
    /// </summary>
    public Location(string longDesc, string shortDesc = "")
    {
        longDescription = longDesc;
        shortDescription = shortDesc;
        travelTable = new Dictionary<int, List<int>>();
    }

    /// <summary>
    /// Propriedade para acesso externo à descrição curta
    /// </summary>
    public string ShortDescription
    {
        get { return shortDescription; }
        set { shortDescription = value; }
    }

    /// <summary>
    /// Propriedade para acesso externo à descrição longa
    /// </summary>
    public string LongDescription
    {
        get { return longDescription; }
        set { longDescription = value; }
    }

    /// <summary>
    /// Adiciona uma nova informação de deslocamento à localização
    /// </summary>
    /// <para>
    /// A posição 0 do vetor info contém o id da localização atual, enquanto a posição 1 contém o id da
    /// localização de destino. As demais posições contêm índices de palavras que disparam a ação de
    /// deslocamento da posição atual para o destino. Por simplicidade, estamos desconsiderando os destinos
    /// com índice acima de 300, pois estes índices têm significados especiais no jogo original que não
    /// serão implementados aqui.
    /// </para>
    /// <param name="info">Vetor de strings contendo os dados de deslocamento</param>
    public void AddTravelInfo(string[] info)
    {
        int destinationID = int.Parse(info[1])-1;
        if(destinationID < 300)
        {
            travelTable[destinationID] = new List<int>();
            for(int i = 2; i < info.Length; i++)
            {
                int motionVerbId = int.Parse(info[i]);
                travelTable[destinationID].Add(motionVerbId);
            }
        }
    }

    /// <summary>
    /// Encontra e retorna o id de destino correspondente ao id da palavra passada como parâmetro
    /// </summary>
    /// <param name="word">Id de uma palavra válida</param>
    /// <returns>O id de destino entrado, ou -1 caso não o encontre</returns>
    public int FindDestination(int word)
    {
        // =================================================================

        // Implemente aqui a sua solução
        // Você pode usar travelTable.Keys para obter a lista de chaves do dicionário
        // No C# você tem a estrutura "foreach", cuja sintaxe é:
        //      foreach(<tipo> <variável> in <coleção>)
        // Documentação: https://learn.microsoft.com/pt-br/dotnet/api/system.collections.generic.dictionary-2?view=net-7.0

        return -1;

        // =================================================================
    }
}
