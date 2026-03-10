#ifndef SPORTEVENT_H
#define SPORTEVENT_H

#include <string>
using namespace std;

class SportEvent {
protected:
    string eventName;
    bool isActive;
    int playersCount;

public:
    SportEvent(string name, int players);
    
    virtual void startGame() = 0;
    virtual void stopGame() = 0;
    virtual void showInfo() = 0;
    
    string getName();
    int getPlayersCount();
    bool getStatus();
    
    virtual ~SportEvent() {}
};

#endif
