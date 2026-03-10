#ifndef TENNIS_H
#define TENNIS_H

#include "sportevent.h"
#include <string>

class Tennis : public SportEvent {
private:
    string courtSurface;
    int setsCount;
    int player1Score;
    int player2Score;
    int currentGame;

public:
    // Три конструктора
    Tennis(string name, int players, string surface, int sets, int score1, int score2, int game);
    Tennis(string name, int players, string surface, int sets);
    Tennis(string name, int players);
    
    void startGame() override;
    void stopGame() override;
    void showInfo() override;
    
    void changeSides();
    void serve(string player);
    
    string getCourtSurface() { return courtSurface; }
    int getSetsCount() { return setsCount; }
};

#endif
