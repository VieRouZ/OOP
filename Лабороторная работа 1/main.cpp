#include <iostream>
#include <cstdlib>
#include <ctime>
#include "sportevent.h"
#include "football.h"
#include "tennis.h"

using namespace std;

int main() {
    // Инициализация генератора случайных чисел
    srand(time(0));
    
    cout << "=== СПОРТИВНЫЕ СОБЫТИЯ ===" << endl;
    
    Football football("Лига Чемпионов", 22);
    Tennis tennis("Уимблдон", 2, "Трава", 5);
    
    // Проверка свойств
    cout << "\nФУТБОЛ:" << endl;
    cout << "  Название: " << football.getName() << endl;
    cout << "  Игроков: " << football.getPlayersCount() << endl;
    cout << "  Стадион: " << football.getStadiumName() << endl;
    cout << "  Статус: " << (football.getStatus() ? "активен" : "не активен") << endl;
    
    cout << "\nТЕННИС:" << endl;
    cout << "  Название: " << tennis.getName() << endl;
    cout << "  Игроков: " << tennis.getPlayersCount() << endl;
    cout << "  Покрытие: " << tennis.getCourtSurface() << endl;
    cout << "  Сетов: " << tennis.getSetsCount() << endl;
    cout << "  Статус: " << (tennis.getStatus() ? "активен" : "не активен") << endl;
    
    cout << "\n" << string(50, '=') << "\n" << endl;
    
    // Массив указателей на базовый класс для демонстрации полиморфизма
    SportEvent* events[] = {&football, &tennis};
    
    // Запуск событий
    cout << "ЗАПУСК СОБЫТИЙ:" << endl;
    events[0]->startGame();  // Футбол
    events[1]->startGame();  // Теннис
    
    // Информация
    cout << "\nИНФОРМАЦИЯ О СОБЫТИЯХ:" << endl;
    events[0]->showScore();
    events[1]->showScore();
    
    cout << "\n" << string(50, '=') << "\n" << endl;
    
    // Уникальные методы
    cout << "УНИКАЛЬНЫЕ МЕТОДЫ:" << endl;
    
    // Для футбола
    cout << "\n--- Футбол ---" << endl;
    Football* footballPtr = dynamic_cast<Football*>(events[0]);
    footballPtr->shootOnGoal("Команда 1");
    footballPtr->shootOnGoal("Команда 2");
    footballPtr->substitutePlayer("Роналду", "Месси");
    
    // Для тенниса
    cout << "\n--- Теннис ---" << endl;
    Tennis* tennisPtr = dynamic_cast<Tennis*>(events[1]);
    tennisPtr->serve("Игрок 1");
    tennisPtr->serve("Игрок 2");
    tennisPtr->changeSides();
    
    cout << "\n" << string(50, '=') << "\n" << endl;
    
    // Показ счета после действий
    cout << "СЧЕТ ПОСЛЕ ДЕЙСТВИЙ:" << endl;
    events[0]->showScore();
    events[1]->showScore();
    
    cout << "\n" << string(50, '=') << "\n" << endl;
    
    // Прерывание событий
    cout << "ПРЕРЫВАНИЕ СОБЫТИЙ:" << endl;
    events[0]->stopGame();
    events[1]->stopGame();
    
    // Попытка действий после остановки
    cout << "\nПОПЫТКА ДЕЙСТВИЙ ПОСЛЕ ОСТАНОВКИ:" << endl;
    footballPtr->shootOnGoal("Команда 1");
    tennisPtr->serve("Игрок 1");
    
    return 0;
}