#include <SFML/Graphics.hpp>
#include <iostream>
#include <sstream>
#include <cmath>

using namespace std;

const int WINDOW_WIDTH = 800;       // largura da janela
const int WINDOW_HEIGHT = 600;      // altura da janela
const int BALL_RADIUS = 10;         // raio da bola (para fins de renderização)
const float VEL_X = 150;            // velocidade de movimentação na horizontal
const float VEL_Y0 = -700;          // velocidade inicial da bola na vertical
const float GRAVITY = 1000;         // aceleração da gravidade

const float dtAlvo = 1/60.0;        

std::string converteParaStr(float val)
{
    stringstream strConv;
	strConv << (int)val;
    return strConv.str();
}

void atualiza(sf::CircleShape& ball, float dt, float& vy)
{
    // ***************************************************** //

    // INCLUA AQUI O SEU CÓDIGO

    // determina se as setas esquerda e direita do teclado foram pressionadas
    if(sf::Keyboard::isKeyPressed(sf::Keyboard::Left))
    {
        // INCLUA AQUI INSTRUÇÕES QUE DEVEM SER EXECUTADAS QUANDO O USUÁRIO PRESSIONAR A SETA ESQUERDA DO TECLADO
    }
    if(sf::Keyboard::isKeyPressed(sf::Keyboard::Right))
    {
        // INCLUA AQUI INSTRUÇÕES QUE DEVEM SER EXECUTADAS QUANDO O USUÁRIO PRESSIONAR A SETA DIREITA DO TECLADO
    }




    // ***************************************************** //
}

void renderiza(sf::RenderWindow& window, sf::CircleShape& ball, sf::Text& info)
{
    // limpa a tela
    window.clear(sf::Color::Black);

    // desenha os objetos da cena
    window.draw(ball);
    window.draw(info);

    // exibe a cena
    window.display();
}

int main()
{
    
    sf::RenderWindow window(sf::VideoMode(WINDOW_WIDTH, WINDOW_HEIGHT), "Bounce");
	sf::Font font;
    if(!font.loadFromFile("DejaVuSansMono.ttf"))
    {
        cout << "Erro ao carregar fonte!" << endl;
        exit(1);
    }
	sf::Text info;
    info.setFont(font);

    sf::CircleShape ball(BALL_RADIUS);
	ball.setOrigin(BALL_RADIUS, BALL_RADIUS);

    // inicialização da posição da bola
    ball.setPosition(WINDOW_WIDTH/2, WINDOW_HEIGHT*0.95);

    sf::Clock clock;
    sf::Clock fpsClock;
    float tPrev = clock.getElapsedTime().asSeconds();
    float tNow = tPrev;
	int nframes = 0;
    float dtAcumulado = 0;
    float dt = 0;
    float dtPendente = 0;
    float dtFixo = 0.01;
    
    float vy = VEL_Y0;

    // game loop
    while (window.isOpen())
    {
        // processa os eventos da janela
        sf::Event event;
        while (window.pollEvent(event))
        {
            // evento disparado quando o usuário fecha a janela
            if (event.type == sf::Event::Closed)
                window.close();
        }

        tNow = clock.getElapsedTime().asSeconds();
        dt = tNow - tPrev;
        dtPendente += dt;
        tPrev = tNow;

        dtAcumulado += fpsClock.restart().asSeconds();

        atualiza(ball, dt, vy);
        renderiza(window, ball, info);
    }
    return 0;
}