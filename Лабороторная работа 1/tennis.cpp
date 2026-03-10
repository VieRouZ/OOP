#include "tennis.h"
#include <iostream>
using namespace std;

// Конструктор 1: полный
Tennis::Tennis(string name, int players, string surface, int sets, int score1, int score2, int game)
    : SportEvent(name, players), courtSurface(surface), setsCount(sets), 
      player1Score(score1), player2Score(score2), currentGame(game) 
{}

// Конструктор 2: без счета и гейма
Tennis::Tennis(string name, int players, string surface, int sets)
    : SportEvent(name, players), courtSurface(surface), setsCount(sets), 
      player1Score(0), player2Score(0), currentGame(1) 
{}

// Конструктор 3: минимальный
Tennis::Tennis(string name, int players)
    : SportEvent(name, players), courtSurface("Грунт"), setsCount(3), 
      player1Score(0), player2Score(0), currentGame(1) 
{}

void Tennis::startGame() {
    isActive = true;
    cout << eventName << " (теннис) начался!" << endl;
    cout << "  Покрытие корта: " << courtSurface << endl;
    cout << "  Матч до " << setsCount << " сетов" << endl;
    cout << "  Текущий счет: " << player1Score << " - " << player2Score 
         << " (гейм " << currentGame << ")" << endl;
}

void Tennis::stopGame() {
    isActive = false;
    cout << eventName << " (теннис) завершен. Финальный счет: " 
         << player1Score << " - " << player2Score << endl;
}

void Tennis::showInfo() {
    cout << "Событие: " << eventName 
         << " | Вид спорта: Теннис"
         << " | Статус: " << (isActive ? "ИДЕТ МАТЧ" : "НЕ АКТИВЕН")
         << " | Покрытие: " << courtSurface
         << " | Сетов: " << setsCount
         << " | Счет: " << player1Score << " - " << player2Score
         << " (гейм " << currentGame << ")" << endl;
}

void Tennis::serve(string player) {
    if (isActive) {
        cout << eventName << ": Матч приостановлен и идёт выполнение подачи" << endl;
         cout << eventName << ": " << player << " выполняет подачу. ";
        return;
    }
    
    
    // Симуляция подачи (70% успешных)
    bool success = (rand() % 100) < 70;
    if (success) {
        cout << "Подача успешна!" << endl;
        
        // Начисление очков (40% вероятность выигрыша очка)
        bool winPoint = (rand() % 100) < 40;
        if (winPoint) {
            if (player == "Игрок 1") {
                player1Score++;
                cout << "  " << player << " выигрывает очко! Счет: " 
                     << player1Score << " - " << player2Score << endl;
            } else if (player == "Игрок 2") {
                player2Score++;
                cout << "  " << player << " выигрывает очко! Счет: " 
                     << player1Score << " - " << player2Score << endl;
            }
        }
    } else {
        cout << "Ошибка подачи!" << endl;
    }
}

void Tennis::changeSides() {
    if (isActive) {
        cout << eventName << ": Матч приостановлен и идёт смена сторон" << endl;
        cout << eventName << ": Смена сторон. Игроки меняются сторонами корта." << endl;
        currentGame++;
        return;
    }
    
    cout << eventName << ": Смена сторон. Игроки меняются сторонами корта." << endl;
    currentGame++;
}
