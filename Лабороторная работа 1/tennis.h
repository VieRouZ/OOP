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
    // Значения по умолчанию
    Tennis(string name = "Теннисный матч", 
           int players = 2, 
           string surface = "Грунт",
           int sets = 3,
           int score1 = 0,
           int score2 = 0,
           int game = 1);
    
    void startGame() override;
    void stopGame() override;
    void showScore() override;
    
    void changeSides();
    void serve(string player);
    
    string getCourtSurface() { return courtSurface; }
    int getSetsCount() { return setsCount; }
};

#endif