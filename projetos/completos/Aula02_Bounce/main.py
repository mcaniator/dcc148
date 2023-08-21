import pygame
import sys

WINDOW_WIDTH = 800
WINDOW_HEIGHT = 600
BALL_RADIUS = 10
VEL_X = 30.0
VEL_Y0 = -700.0
ACCEL = 1000.0

def atualiza(ball, dt, vy):
    # determina se as setas esquerda e direita do teclado foram pressionadas
    keys = pygame.key.get_pressed()
    if keys[pygame.K_LEFT]:
        ball[0] -= VEL_X*dt
    if keys[pygame.K_RIGHT]:
        ball[0] += VEL_X*dt
        
    vy += ACCEL*dt
    if ball[1] > WINDOW_HEIGHT*0.95:
        vy = -vy

    ball[1] += vy*dt
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
    fpsClock = pygame.time.Clock()

    clock.tick()

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

        MAX_ITER = 5
        i = 0
        while dtPendente >= dtFixo and i < MAX_ITER:
            vy = atualiza(ball, dtFixo, vy)
            if lag:
                pygame.time.delay(10) # 10 milissegundos

            dtPendente -= dtFixo
            i += 1
        
        nframes += 1
        if dtAcumulado >= 1:
            currentFps = nframes / dtAcumulado
            
            fpsInfo = str(int(currentFps))
            nframes = 0
            dtAcumulado = 0

        renderiza(window, ball, font, fpsInfo)