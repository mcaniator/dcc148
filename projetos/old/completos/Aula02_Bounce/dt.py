import pygame

class ControleDelta(object):
    
    def __init__(self, callback):
        self.nframes = 0
        self.fps = 0
        self.dtAcumulado = 0
        self.dtPendente = 0
        self.dtFixo = 0.01
        self.dtLimite = 0.1
        self.dtAlvo = 1.0 / 60

        self.atualiza = callback
        self.metodo = self.dt_variavel

        self.lag = False

    def calculaFPS(self, dt):
        
        self.nframes += 1
        self.dtAcumulado += dt

        if self.dtAcumulado >= 1:
            self.fps = self.nframes / self.dtAcumulado
            
            self.nframes = 0
            self.dtAcumulado = 0
        
        return self.fps
    
    def alternaLag(self):
        self.lag = not self.lag
    
    def alternaMetodo(self, tecla):
        if chr(tecla) == '0':
            print('dt livre')
            self.metodo = self.dt_livre
        elif chr(tecla) == '1':
            print('dt fixo')
            self.metodo = self.dt_fixo
        elif chr(tecla) == '2':
            print('dt variavel')
            self.metodo = self.dt_variavel
        elif chr(tecla) == '3':
            print('dt semi-fixo')
            self.metodo = self.dt_semi_fixo
        elif chr(tecla) == '4':
            print('dt fixo com fps livre')
            self.dtPendente = 0
            self.metodo = self.dt_fixo_com_fps_livre
    
    def exec(self, obj, dt):
        if self.lag:
            pygame.time.delay(200)

        self.metodo(obj, dt)

    def dt_livre(self, obj, dt):
        self.atualiza(obj, self.dtFixo)

    def dt_fixo(self, obj, dt):
        if dt < self.dtAlvo:
            tempo_restante = int((self.dtAlvo - dt) * 1000)
            pygame.time.delay(tempo_restante)
            
        self.atualiza(obj, self.dtFixo)
    
    def dt_variavel(self, obj, dt):
        self.atualiza(obj, dt)
    
    def dt_semi_fixo(self, obj, dt):
        MAX_ITER = 50
        i = 0
        while dt > 0 and i < MAX_ITER:
            dtUsado = min(dt, self.dtLimite)
            self.atualiza(obj, dtUsado)
            dt -= dtUsado
            i += 1
    
    def dt_fixo_com_fps_livre(self, obj, dt):
        self.dtPendente += dt

        MAX_ITER = 50
        i = 0
        while self.dtPendente >= self.dtFixo and i < MAX_ITER:
            self.atualiza(obj, self.dtFixo)
            self.dtPendente -= self.dtFixo
            i += 1