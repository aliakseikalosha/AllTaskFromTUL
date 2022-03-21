//
// Created by Aliaksei Kalosha on 21.03.2022.
//

#include "Avatar.h"

class Player : Avatar {
protected:
    float speed = 0;
    const glm::vec3 worldUp = glm::vec3(0, 1, 0);
public:
    Player(float speed) : speed(speed) {}

    void ProcessInput(const glm::vec3 &move, const glm::vec2 &look, const float &dt) {
        float v = dt * speed;
        angle += look;
        auto f = forvard();
        auto r = glm::normalize(glm::cross(f, glm::vec3()));
        pos += f * v * move.x +  r * v * move.y;
    }
};
