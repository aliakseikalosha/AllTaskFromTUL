//
// Created by Aliaksei Kalosha on 17.05.2022.
//

#ifndef PG2GAME_LIGHTDATA_H
#define PG2GAME_LIGHTDATA_H


#pragma once
#include "glm/gtc/type_ptr.hpp"
class LightData {
public:
    glm::vec3 pos;
    glm::vec4 color;

    LightData(const glm::vec3 &pos, const glm::vec4 &color) : pos(pos), color(color) {}
};


#endif //PG2GAME_LIGHTDATA_H
