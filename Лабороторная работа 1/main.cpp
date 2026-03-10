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
    
    // Создание объектов с разными конструкторами
    Football match1("Лига Чемпионов", 22, "Уэмбли", 2, 1);     // полный конструктор
    Football match2("Чемпионат Мира", 22, "Лужники");          // конструктор без счета
    Football match3("Товарищеский матч", 22);                  // минимальный конструктор
    
    Tennis tennis1("Уимблдон", 2, "Трава", 5, 1, 0, 2);        // полный конструктор
    Tennis tennis2("Roland Garros", 2, "Грунт", 3);            // конструктор без счета
    Tennis tennis3("Кубок Кремля", 2);                          // минимальный конструктор
    
    // Проверка свойств
    cout << "\n=== ФУТБОЛЬНЫЕ МАТЧИ ===" << endl;
    
    cout << "\nМАТЧ 1 (полный конструктор):" << endl;
    cout << "  Название: " << match1.getName() << endl;
    cout << "  Игроков: " << match1.getPlayersCount() << endl;
    cout << "  Стадион: " << match1.getStadiumName() << endl;
    cout << "  Счет: " << match1.getTeam1Score() << " : " << match1.getTeam2Score() << endl;
    
    cout << "\nМАТЧ 2 (конструктор без счета):" << endl;
    cout << "  Название: " << match2.getName() << endl;
    cout << "  Игроков: " << match2.getPlayersCount() << endl;
    cout << "  Стадион: " << match2.getStadiumName() << endl;
    cout << "  Счет: " << match2.getTeam1Score() << " : " << match2.getTeam2Score() << endl;
    
    cout << "\nМАТЧ 3 (минимальный конструктор):" << endl;
    cout << "  Название: " << match3.getName() << endl;
    cout << "  Игроков: " << match3.getPlayersCount() << endl;
    cout << "  Стадион: " << match3.getStadiumName() << endl;
    cout << "  Счет: " << match3.getTeam1Score() << " : " << match3.getTeam2Score() << endl;
    
    cout << "\n=== ТЕННИСНЫЕ МАТЧИ ===" << endl;
    
    cout << "\nТЕННИС 1 (полный конструктор):" << endl;
    cout << "  Название: " << tennis1.getName() << endl;
    cout << "  Игроков: " << tennis1.getPlayersCount() << endl;
    cout << "  Покрытие: " << tennis1.getCourtSurface() << endl;
    cout << "  Сетов: " << tennis1.getSetsCount() << endl;
    
    cout << "\nТЕННИС 2 (конструктор без счета):" << endl;
    cout << "  Название: " << tennis2.getName() << endl;
    cout << "  Игроков: " << tennis2.getPlayersCount() << endl;
    cout << "  Покрытие: " << tennis2.getCourtSurface() << endl;
    cout << "  Сетов: " << tennis2.getSetsCount() << endl;
    
    cout << "\nТЕННИС 3 (минимальный конструктор):" << endl;
    cout << "  Название: " << tennis3.getName() << endl;
    cout << "  Игроков: " << tennis3.getPlayersCount() << endl;
    cout << "  Покрытие: " << tennis3.getCourtSurface() << endl;
    cout << "  Сетов: " << tennis3.getSetsCount() << endl;
    
    cout << "\n" << string(60, '=') << "\n" << endl;
    

    SportEvent* events[] = {&match1, &match2, &match3, &tennis1, &tennis2, &tennis3};
    // ЗАПУСК СОБЫТИЙ (без цикла)
    cout << "ЗАПУСК СПОРТИВНЫХ СОБЫТИЙ:" << endl;
    cout << "\nСобытие 1: ";
    events[0]->startGame();  // match1
    cout << "\nСобытие 2: ";
    events[1]->startGame();  // match2
    cout << "\nСобытие 3: ";
    events[2]->startGame();  // match3
    cout << "\nСобытие 4: ";
    events[3]->startGame();  // tennis1
    cout << "\nСобытие 5: ";
    events[4]->startGame();  // tennis2
    cout << "\nСобытие 6: ";
    events[5]->startGame();  // tennis3
    
    // ИНФОРМАЦИЯ О СОБЫТИЯХ (без цикла)
    cout << "\n\nИНФОРМАЦИЯ О СОБЫТИЯХ:" << endl;
    cout << "\nСобытие 1: ";
    events[0]->showInfo();
    cout << "\nСобытие 2: ";
    events[1]->showInfo();
    cout << "\nСобытие 3: ";
    events[2]->showInfo();
    cout << "\nСобытие 4: ";
    events[3]->showInfo();
    cout << "\nСобытие 5: ";
    events[4]->showInfo();
    cout << "\nСобытие 6: ";
    events[5]->showInfo();
    
    cout << "\n" << string(60, '=') << "\n" << endl;
    
    // УНИКАЛЬНЫЕ МЕТОДЫ
    cout << "УНИКАЛЬНЫЕ МЕТОДЫ:" << endl;
    
    // Для футбола
    cout << "\n--- Футбольные матчи ---" << endl;
    
    // match1 - удар команды 1
    cout << "\n";
    dynamic_cast<Football*>(events[0])->shootOnGoal("Команда 1");
    
    // match2 - удары обеих команд
    cout << "\n";
    dynamic_cast<Football*>(events[1])->shootOnGoal("Команда 1");
    cout << "\n";
    dynamic_cast<Football*>(events[1])->shootOnGoal("Команда 2");
    
    // match3 - замена игрока
    cout << "\n";
    dynamic_cast<Football*>(events[2])->substitutePlayer("Форвард", "Защитник");
    
    // Для тенниса
    cout << "\n--- Теннисные матчи ---" << endl;
    
    // tennis1 - подача игрока 1
    cout << "\n";
    dynamic_cast<Tennis*>(events[3])->serve("Игрок 1");
    
    // tennis2 - подачи обоих игроков
    cout << "\n";
    dynamic_cast<Tennis*>(events[4])->serve("Игрок 1");
    cout << "\n";
    dynamic_cast<Tennis*>(events[4])->serve("Игрок 2");
    
    // tennis3 - смена сторон
    cout << "\n";
    dynamic_cast<Tennis*>(events[5])->changeSides();
    
    cout << "\n" << string(60, '=') << "\n" << endl;

    // ОБНОВЛЕННАЯ ИНФОРМАЦИЯ ПОСЛЕ ДЕЙСТВИЙ (без цикла)
    cout << "ИНФОРМАЦИЯ ПОСЛЕ ДЕЙСТВИЙ:" << endl;
    cout << "\nСобытие 1: ";
    events[0]->showInfo();
    cout << "\nСобытие 2: ";
    events[1]->showInfo();
    cout << "\nСобытие 3: ";
    events[2]->showInfo();
    cout << "\nСобытие 4: ";
    events[3]->showInfo();
    cout << "\nСобытие 5: ";
    events[4]->showInfo();
    cout << "\nСобытие 6: ";
    events[5]->showInfo();
    
    cout << "\n" << string(60, '=') << "\n" << endl;
    
    // ЗАВЕРШЕНИЕ СОБЫТИЙ (без цикла)
    cout << "ЗАВЕРШЕНИЕ СПОРТИВНЫХ СОБЫТИЙ:" << endl;
    cout << "\nСобытие 1: ";
    events[0]->stopGame();
    cout << "\nСобытие 2: ";
    events[1]->stopGame();
    cout << "\nСобытие 3: ";
    events[2]->stopGame();
    cout << "\nСобытие 4: ";
    events[3]->stopGame();
    cout << "\nСобытие 5: ";
    events[4]->stopGame();
    cout << "\nСобытие 6: ";
    events[5]->stopGame();
    
    cout << "\n" << string(60, '=') << "\n" << endl;
    cout << "ПРОГРАММА ЗАВЕРШЕНА" << endl;
    
    return 0;
}
