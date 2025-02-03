import pygame
import sys

WINDOW_WIDTH = 800
WINDOW_HEIGHT = 600

def renderiza(window, img):
    # limpa a tela
    window.fill((0, 0, 0))

    # desenha os objetos da cena
    # white = (255, 255, 255)
    # pygame.draw.circle(window, white, (ball[0], ball[1]), BALL_RADIUS)

    # img = font.render(info, True, white)
    window.blit(img, (5, 5))

    # exibe a cena
    pygame.display.update()


# função principal
if __name__ == '__main__':
    pygame.init()

    window = pygame.display.set_mode((WINDOW_WIDTH, WINDOW_HEIGHT))
    pygame.display.set_caption('Sprites')

    spritesheet = pygame.image.load('boom3.png').convert()

    # game loop
    while True:
        # processa os eventos da janela
        for event in pygame.event.get():
            # evento disparado quando o usuário fecha a janela
            if event.type == pygame.QUIT:
                pygame.quit()
                sys.exit()

        renderiza(window, spritesheet)