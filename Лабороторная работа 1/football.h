#ifndef FOOTBALL_H
#define FOOTBALL_H

#include "sportevent.h"
#include <string>

class Football : public SportEvent {
private:
    string stadiumName;
    int team1Score;
    int team2Score;

public:
    // Значения по умолчанию
    Football(string name = "Футбольный матч", 
             int players = 22, 
             string stadium = "Стандартный стадион",
             int score1 = 0, 
             int score2 = 0);
    
    void startGame() override;
    void stopGame() override;
    void showScore() override;
    
    void shootOnGoal(string team);
    void substitutePlayer(string playerOut, string playerIn);
    
    string getStadiumName() { return stadiumName; }
    int getTeam1Score() { return team1Score; }
    int getTeam2Score() { return team2Score; }
};

#endif