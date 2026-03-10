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
    // Три конструктора
    Football(string name, int players, string stadium, int score1, int score2);
    Football(string name, int players, string stadium);
    Football(string name, int players);
    
    void startGame() override;
    void stopGame() override;
    void showInfo() override;
    
    void shootOnGoal(string team);
    void substitutePlayer(string playerOut, string playerIn);
    
    string getStadiumName() { return stadiumName; }
    int getTeam1Score() { return team1Score; }
    int getTeam2Score() { return team2Score; }
};

#endif
