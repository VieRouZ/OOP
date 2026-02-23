#include "tennis.h"
#include <iostream>
using namespace std;

Tennis::Tennis(string name, int players, string surface, int sets, 
               int score1, int score2, int game)
    : SportEvent(name, players), courtSurface(surface), setsCount(sets), 
      player1Score(score1), player2Score(score2), currentGame(game) 
{
}

void Tennis::startGame() {
    isActive = true;
    cout << eventName << " (теннис) начался!" << endl;
    cout << "  Покрытие корта: " << courtSurface << endl;
    cout << "  Матч до " << setsCount << " сетов" << endl;
}

void Tennis::stopGame() {
    isActive = false;
    cout << eventName << " (теннис) завершен." << endl;
    showScore();
}

void Tennis::showScore() {
    cout << "Счет: " << player1Score << " - " << player2Score 
         << " (гейм " << currentGame << ")" << endl;
}

void Tennis::changeSides() {
    if (!isActive) {
        cout << eventName << ": Матч не активен!" << endl;
        return;
    }
    
    cout << eventName << ": Смена сторон. ";
    cout << "Игроки меняются сторонами корта." << endl;
}

void Tennis::serve(string player) {
    if (!isActive) {
        cout << eventName << ": Матч не активен!" << endl;
        return;
    }
    
    cout << eventName << ": " << player << " выполняет подачу. ";
    
    // Симуляция подачи (70% успешных подач)
    bool success = (rand() % 100) < 70;
    if (success) {
        cout << "Подача успешна!" << endl;
        
        // Начисление очков (30% вероятность выигрыша очка с подачи)
        bool winPoint = (rand() % 100) < 30;
        if (winPoint) {
            if (player == "Игрок 1") {
                player1Score++;
                cout << "  " << player << " выигрывает очко!" << endl;
            } else if (player == "Игрок 2") {
                player2Score++;
                cout << "  " << player << " выигрывает очко!" << endl;
            }
        }
    } else {
        cout << "Ошибка подачи!" << endl;
    }
}