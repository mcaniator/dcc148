import pygame
import sys

WINDOW_WIDTH = 800          # largura da janela
WINDOW_HEIGHT = 600         # altura da janela
BALL_RADIUS = 10            # raio da bola (para fins de renderização)
VEL_X = 150.0               # velocidade da bola na horizontal
VEL_Y0 = -700.0             # velocidade inicial da bola na vertical
GRAVITY = 1000.0            # aceleração da gravidade

def atualiza(ball, dt, vy):
    # ***************************************************** //

    # INCLUA AQUI O SEU CÓDIGO

    # determina se as setas esquerda e direita do teclado foram pressionadas
    keys = pygame.key.get_pressed()
    if keys[pygame.K_LEFT]:
        # INCLUA AQUI INSTRUÇÕES QUE DEVEM SER EXECUTADAS QUANDO O USUÁRIO PRESSIONAR A SETA ESQUERDA DO TECLADO
        pass
    if keys[pygame.K_RIGHT]:
        # INCLUA AQUI INSTRUÇÕES QUE DEVEM SER EXECUTADAS QUANDO O USUÁRIO PRESSIONAR A SETA DIREITA DO TECLADO
        pass




    # ***************************************************** //
    
    return vy

def renderiza(window, ball, font, info):
    # limpa a tela
    window.fill((0, 0, 0))

    # desenha os objetos da cena
    white = (255, 255, 255)
    pygame.draw.circle(window, white, (ball[0], ball[1]), BALL_RADIUS)

    img = font.render(info, True, white)
    window.blit(img, (5, 5))

    # exibe a cena
    pygame.display.update()


# função principal
if __name__ == '__main__':
    pygame.init()

    window = pygame.display.set_mode((WINDOW_WIDTH, WINDOW_HEIGHT))
    pygame.display.set_caption('Bounce')

    font = pygame.font.SysFont('DejaVuSansMono.ttf', 36)
    fpsInfo = ''

    # definindo a bola como uma lista de 2 elementos, que representam o par de coordenadas (x, y)
    ball = [WINDOW_WIDTH / 2, WINDOW_HEIGHT * 0.95]

    vy = VEL_Y0

    clock = pygame.time.Clock()

    nframes = 0
    dtAcumulado = 0
    dt = 0
    dtPendente = 0
    dtFixo = 0.01

    lag = False

    # game loop
    while True:
        # processa os eventos da janela
        for event in pygame.event.get():
            # evento disparado quando o usuário fecha a janela
            if event.type == pygame.QUIT:
                pygame.quit()
                sys.exit()
            
            if event.type == pygame.KEYDOWN: 
                if event.key == pygame.K_l:
                    lag = not lag

        clock.tick()
        dt = clock.get_time() / 1000.0
        dtPendente += dt
        dtAcumulado += dt

        vy = atualiza(ball, dt, vy)
        renderiza(window, ball, font, fpsInfo)