#include <SFML/Graphics.hpp>
#include <iostream>
#include <sstream>
#include <cmath>

using namespace std;

const string FONT_PATH = "/usr/share/fonts/TTF/DejaVuSansMono.ttf";
const int WINDOW_WIDTH = 800;
const int WINDOW_HEIGHT = 600;
const int BALL_RADIUS = 10;

std::string converteParaStr(float val)
{
    stringstream strConv;
	strConv << (int)val;
    return strConv.str();
}

int main()
{
    
    sf::RenderWindow window(sf::VideoMode(WINDOW_WIDTH, WINDOW_HEIGHT), "Pong");
	sf::Font font;
    if(!font.loadFromFile(FONT_PATH))
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

        // limpa a tela
        window.clear(sf::Color::Black);

        // desenha os objetos da cena
        window.draw(ball);
        window.draw(info);

        // exibe a cena
        window.display();
    }
    return 0;
}