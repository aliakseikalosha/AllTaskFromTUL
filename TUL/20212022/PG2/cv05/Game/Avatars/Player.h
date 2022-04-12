//
// Created by Aliaksei Kalosha on 21.03.2022.
//
#pragma once
#include "Avatar.h"

class Player : public Avatar {
protected:
    float speed = 0;
    const glm::vec3 worldUp = glm::vec3(0, 1, 0);
public:
    Player(const glm::vec3 &pos, const glm::vec2 &angle, float speed) : Avatar(pos, angle), speed(speed) {}

    void ProcessInput(const glm::vec3 &move, const glm::vec2 &look, const float &dt) {
        float v = dt * speed;
        angle += look * dt;
        auto f = forvard();
        auto r = glm::normalize(glm::cross(f, worldUp));
        pos += f * v * move.z +  r * v * move.x;
    }
};
