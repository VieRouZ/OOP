#include "sportevent.h"
#include <iostream>
using namespace std;

SportEvent::SportEvent(string name, int players) 
    : eventName(name), isActive(false), playersCount(players) {}

string SportEvent::getName() { 
    return eventName; 
}

int SportEvent::getPlayersCount() { 
    return playersCount; 
}

bool SportEvent::getStatus() { 
    return isActive; 
}