# Processa o arquivo de dados 'cave.dat', contendo informações sobre locais, palavras-chave e a tabela de deslocamentos
def carregaDados():
    locais = {}
    palavras = {}
    tabelaDeslocamento = {}
    with open('cave.dat', 'r') as arquivo:
        # Pula cabeçalho da Seção 1
        linha = arquivo.readline()
        id_anterior = -1
        descricao_local = ''

        linha = arquivo.readline()
        partes = linha.split()
        id_local = int(partes[0])
        while id_local != -1:

            if id_local != id_anterior:
                if id_anterior != -1:
                    locais[id_anterior] = Local(id_anterior, descricao_local)
                descricao_local = ''
                id_anterior = id_local
            
            descricao_local += ' '.join(partes[1:]) + ' '

            linha = arquivo.readline()
            partes = linha.split()
            id_local = int(partes[0])

            # print(linha)
        
        locais[id_anterior] = Local(id_anterior, descricao_local)
        
        # Pula cabeçalho da Seção 3
        linha = arquivo.readline()

        linha = arquivo.readline()
        while linha[0] != '-':
            partes = linha.split()

            id_origem = int(partes[0])
            id_destino = int(partes[1])
            ids_palavras = partes[2:]

            locais[id_origem].novoDestino(id_destino, ids_palavras)

            linha = arquivo.readline()

            # print(linha)

        # Pula cabeçalho da Seção 4
        linha = arquivo.readline()

        linha = arquivo.readline()
        while linha[0] != '-':
            partes = linha.split()

            id_palavra = int(partes[0])
            palavra = partes[1]

            palavras[palavra] = id_palavra

            linha = arquivo.readline()

            # print(linha)
    
    return locais, palavras, tabelaDeslocamento




# Classe que representa uma localização do jogo
# Atributos:
# - id (int): valor numérico que identifica unicamente uma localização (primeira coluna da Seção 1 do arquivo de dados)
# - descricao (str): descrição longa de uma localização (segunda coluna da Seção 1 do arquivo de dados)
# - destinos (dict): um dicionário que associa ids de palavras a ids de localizações
class Local(object):

    # Construtor da classe
    def __init__(self, id, descricao):
        self.id = id
        self.descricao = descricao
        self.destinos = {}
    
    # Adiciona um novo destino (usada apenas durante o carregamento do arquivo de dados)
    def novoDestino(self, id_local, lista_palavras):
        for p in lista_palavras:
            self.destinos[int(p)] = id_local

    # Retorna a descrição do local    
    def getDescricao(self):
        return self.descricao

    # Retorna o id do local de destino, dado o id de uma palavra fornecida como parâmetro. Se o id da palavra não puder
    # ser encontrado no dicionário, então não é possível se deslocar até algum destino a partir da palavra-chave fornecida,
    # retornando None. Por exemplo, a palavra "N" (norte) permite que o jogador se mova do Local 1 para o Local 2. 
    # Mas a palavra "ROCK" não permite o deslocamento do Local 1 para nenhum outro local. As palavras válidas (e seus ids) 
    # estão na Seção 4 do arquivo de dados. A tabela de deslocamento está na Seção 3.
    def getDestino(self, id_palavra):
        for p in self.destinos.keys():
            if p == id_palavra:
                return self.destinos[p]
        return None

    # Permite o uso da função print com um objeto da classe
    def __str__(self):
        return self.getDescricao()

# Função main
if __name__ == '__main__':

    # Obtém as estruturas de dados necessárias para a execução do jogo
    locais, palavras, tabelaDeslocamento = carregaDados()

    # Define a posição inicial do jogador como sendo o Local 1
    pos_jogador = 1

    # Usuário digita uma palavra válida
    comando = input('Digite um comando: ')

    # Encontra o id correspondente à palavra digitada (o método upper() converte a palavra para maiúsculas)
    id_palavra = palavras[comando.upper()]

    # Encontra o destino a partir da palavra digitada, caso a palavra seja válida
    destino = locais[pos_jogador].getDestino(id_palavra)

    # Imprime o local
    print(locais[destino])

