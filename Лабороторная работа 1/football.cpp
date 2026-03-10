#include "football.h"
#include <iostream>
using namespace std;

// Конструктор 1: полный
Football::Football(string name, int players, string stadium, int score1, int score2)
    : SportEvent(name, players), stadiumName(stadium), team1Score(score1), team2Score(score2) 
{}

// Конструктор 2: без счёта
Football::Football(string name, int players, string stadium)
    : SportEvent(name, players), stadiumName(stadium), team1Score(0), team2Score(0) 
{}

// Конструктор 3: минимальный
Football::Football(string name, int players)
    : SportEvent(name, players), stadiumName("Стандартный стадион"), team1Score(0), team2Score(0) 
{}

void Football::startGame() {
    isActive = true;
    cout << eventName << " (футбол) начался!" << endl;
    cout << "  Стадион: " << stadiumName << endl;
    cout << "  На поле " << playersCount << " игроков" << endl;
    cout << "  Текущий счет: " << team1Score << " : " << team2Score << endl;
}

void Football::stopGame() {
    isActive = false;
    cout << eventName << " (футбол) завершен. Финальный счет: " 
         << team1Score << " : " << team2Score << endl;
}

void Football::showInfo() {
    cout << "Событие: " << eventName 
         << " | Вид спорта: Футбол"
         << " | Статус: " << (isActive ? "ИДЕТ МАТЧ" : "НЕ АКТИВЕН")
         << " | Игроков: " << playersCount
         << " | Стадион: " << stadiumName
         << " | Счет: " << team1Score << " : " << team2Score << endl;
}

void Football::shootOnGoal(string team) {
    if (!isActive) {
        cout << eventName << ": Сначала начните матч!" << endl;
        return;
    }
    
    cout << eventName << ": " << team << " - удар по воротам! ";
    
    // Симуляция гола (30% вероятности)
    bool isGoal = (rand() % 10) < 3;
    if (isGoal) {
        if (team == "Команда 1" || team == "хозяева") {
            team1Score++;
            cout << "ГООООЛ! Счет: " << team1Score << " : " << team2Score << endl;
        } else if (team == "Команда 2" || team == "гости") {
            team2Score++;
            cout << "ГООООЛ! Счет: " << team1Score << " : " << team2Score << endl;
        }
    } else {
        cout << "Мимо ворот!" << endl;
    }
}

void Football::substitutePlayer(string playerOut, string playerIn) {
    if (!isActive) {
        cout << eventName << ": Замена возможна только во время матча!" << endl;
        return;
    }
    
    cout << eventName << ": Замена - " << playerOut << " уходит, " 
         << playerIn << " выходит на поле" << endl;
}
