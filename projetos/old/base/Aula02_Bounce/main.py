import pygame
import sys

from bola import Bola

LARGURA_JANELA = 800            # largura da janela
ALTURA_JANELA = 600             # altura da janela
CHAO = ALTURA_JANELA * 0.95     # ponto onde a bola quica
RAIO_BOLA = 10                  # raio da bola (para fins de renderização)
VEL_X = 150.0                   # velocidade da bola na horizontal
VEL_Y0 = -700.0                 # velocidade inicial da bola no eixo y
GRAVIDADE = 1000.0              # aceleração da gravidade

def atualiza(bola, dt):

    # obtém as coordenadas x e y da posição da bola, e a velocidade da bola no eixo y
    x, y = bola.getPosicao()
    vy = bola.getVelY()

    # ***************************************************** #
    #
    # ************ INCLUA ABAIXO O SEU CÓDIGO ************* #

    # determina se as setas esquerda e direita do teclado foram pressionadas
    keys = pygame.key.get_pressed()
    if keys[pygame.K_LEFT]:
        # INCLUA AQUI INSTRUÇÕES QUE DEVEM SER EXECUTADAS QUANDO O USUÁRIO PRESSIONAR A SETA ESQUERDA DO TECLADO
        pass
    if keys[pygame.K_RIGHT]:
        # INCLUA AQUI INSTRUÇÕES QUE DEVEM SER EXECUTADAS QUANDO O USUÁRIO PRESSIONAR A SETA DIREITA DO TECLADO
        pass




    # ***************************************************** #
    
    # atualiza as coordenadas x e y da posição da bola, e a velocidade da bola no eixo y
    bola.setPosicao(x, y)
    bola.setVelY(vy)

def renderiza(window, bola, font, info):
    # limpa a tela
    window.fill((0, 0, 0))

    # desenha os objetos da cena
    white = (255, 255, 255)
    pygame.draw.circle(window, white, (bola.getX(), bola.getY()), RAIO_BOLA)

    img = font.render(info, True, white)
    window.blit(img, (5, 5))

    # exibe a cena
    pygame.display.update()


# função principal
if __name__ == '__main__':
    pygame.init()

    window = pygame.display.set_mode((LARGURA_JANELA, ALTURA_JANELA))
    pygame.display.set_caption('Bounce')

    font = pygame.font.SysFont('DejaVuSansMono.ttf', 36)
    fpsInfo = ''

    # chama o construtor da classe Bola, posicionando-a inicialmente no centro da tela (em x) e na altura do chão, 
    # com velocidade inicial em y igual a VEL_Y0
    bola = Bola(LARGURA_JANELA / 2, CHAO, VEL_Y0)

    clock = pygame.time.Clock()

    dt = 0

    # game loop
    while True:
        # processa os eventos da janela
        for event in pygame.event.get():
            # evento disparado quando o usuário fecha a janela
            if event.type == pygame.QUIT:
                pygame.quit()
                sys.exit()

        clock.tick()
        dt = clock.get_time() / 1000.0

        atualiza(bola, dt)
        renderiza(window, bola, font, fpsInfo)