class Bola(object):
    def __init__(self, x, y, vy):
        self.setPosicao(x, y)
        self.vy = vy
    
    def getX(self):
        return self.x
    
    def getY(self):
        return self.y

    def getPosicao(self):
        return self.x, self.y

    def getVelY(self):
        return self.vy
    
    def setPosicao(self, x, y):
        self.x = x
        self.y = y

    def setVelY(self, vy):
        self.vy = vy
