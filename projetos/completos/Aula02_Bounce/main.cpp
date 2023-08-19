#include <SFML/Graphics.hpp>
#include <iostream>
#include <sstream>
#include <cmath>

using namespace std;

const string FONT_PATH = "/usr/share/fonts/TTF/DejaVuSansMono.ttf";
const int WINDOW_WIDTH = 800;
const int WINDOW_HEIGHT = 600;
const int BALL_RADIUS = 10;
const float VEL_X = 30;
const float VEL_Y0 = -700;
const float ACCEL = 1000;

const float dtAlvo = 1/60.0;

std::string converteParaString(float val)
{
    stringstream strConv;
	strConv << (int)val;
    return strConv.str();
}

void atualiza(sf::CircleShape& ball, float dt, float& vy)
{
    // determina se as setas esquerda e direita do teclado foram pressionadas
    if(sf::Keyboard::isKeyPressed(sf::Keyboard::Left))
        ball.move(-VEL_X*dt, 0);
    if(sf::Keyboard::isKeyPressed(sf::Keyboard::Right))
        ball.move(VEL_X*dt, 0);

    vy += ACCEL*dt;
    if(ball.getPosition().y > WINDOW_HEIGHT*0.95)
        vy = -vy;

    ball.move(0, vy*dt);
}

void atualizaFPS(sf::Text& info, float& dt, int& nframes)
{
    nframes++;
    if(dt >= 1)
    {
        int currentFps = nframes / dt;
        
        info.setString(converteParaString(currentFps));
        nframes = 0;
        dt = 0;
    }
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
    sf::RenderWindow window(sf::VideoMode(WINDOW_WIDTH, WINDOW_HEIGHT), "Pong");

    sf::CircleShape ball(BALL_RADIUS);
	ball.setOrigin(BALL_RADIUS, BALL_RADIUS);

	sf::Font font;
    if(!font.loadFromFile(FONT_PATH))
    {
        cout << "Erro ao carregar fonte!" << endl;
        exit(1);
    }
	sf::Text info;
    info.setFont(font);

    ball.setPosition(WINDOW_WIDTH/2, WINDOW_HEIGHT*0.95);
    float vy = VEL_Y0;

    sf::Clock clock;
    sf::Clock fpsClock;
    float tPrev = clock.getElapsedTime().asSeconds();
    float tNow = tPrev;
	int nframes = 0;
    float dtAcumulado = 0;
    float dt = 0;
    float dtPendente = 0;
    float dtFixo = 0.01;

    bool lag = false;


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
            
            if(event.type == sf::Event::KeyPressed && event.key.scancode == sf::Keyboard::Scan::L)
                lag = !lag;
        }

        tNow = clock.getElapsedTime().asSeconds();
        dt = tNow - tPrev;
        dtPendente += dt;
        tPrev = tNow;

        dtAcumulado += fpsClock.restart().asSeconds();

        const int MAX_ITER = 5;
        int i = 0;
        while(dtPendente >= dtFixo && i < MAX_ITER)
        {
            atualiza(ball, dtFixo, vy);
            if(lag)
            {
                sf::sleep(sf::seconds(0.01));
            }
            dtPendente -= dtFixo;
            i++;
        }
        // atualiza(ball, dt, vy);
	    atualizaFPS(info, dtAcumulado, nframes);
        renderiza(window, ball, info);
        // window.setFramerateLimit(60);
    }
    return 0;
}