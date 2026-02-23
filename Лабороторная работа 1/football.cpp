#include "football.h"
#include <iostream>
using namespace std;

Football::Football(string name, int players, string stadium, int score1, int score2)
    : SportEvent(name, players), stadiumName(stadium), team1Score(score1), team2Score(score2) 
{
}

void Football::startGame() {
    isActive = true;
    cout << eventName << " (футбол) начался!" << endl;
    cout << "  Стадион: " << stadiumName << endl;
    cout << "  На поле " << playersCount << " игроков" << endl;
}

void Football::stopGame() {
    isActive = false;
    cout << eventName << " (футбол) завершен." << endl;
    showScore();
}

void Football::showScore() {
    cout << "Счет: " << team1Score << " : " << team2Score << endl;
}

void Football::shootOnGoal(string team) {
    if (!isActive) {
        cout << eventName << ": Игра не активна! Сначала начните матч." << endl;
        return;
    }
    
    cout << team << ": Удар по воротам! ";
    
    // Симуляция гола (50% вероятность)
    bool isGoal = rand() % 2;
    if (isGoal) {
        if (team == "Команда 1") {
            team1Score++;
            cout << "ГОООЛ! Счет: " << team1Score << " : " << team2Score << endl;
        } else if (team == "Команда 2") {
            team2Score++;
            cout << "ГОООЛ! Счет: " << team1Score << " : " << team2Score << endl;
        } else {
            cout << "Мяч в аут!" << endl;
        }
    } else {
        cout << "Вратарь спасает команду!" << endl;
    }
}

void Football::substitutePlayer(string playerOut, string playerIn) {
    if (!isActive) {
        cout << eventName << ": Замена возможна только во время игры!" << endl;
        return;
    }
    
    cout << eventName << ": Замена игрока " << playerOut 
         << " на " << playerIn << endl;
    cout << "  " << playersCount << " игроков на поле (без учета замен)" << endl;
}