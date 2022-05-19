//
// Created by Aliaksei Kalosha on 21.03.2022.
//
#pragma once

#include <glm/vec3.hpp>
#include <glm/vec2.hpp>
#include <cmath>
#include <glm/geometric.hpp>
#include <iostream>
#include "SceneObject.h"

class Avatar : public SceneObject {
public:
    Avatar();

    Avatar(const glm::vec3 &pos, const glm::vec2 &angle);

    ~Avatar();

    virtual void printState() {
        std::cout << "pos : [" << pos.x << "," << pos.y << "," << pos.x << "]\n" << "angle : [" << angle.x << ","
                  << angle.y << "]\n" << std::endl;
    }
};

Avatar::Avatar(const glm::vec3 &pos, const glm::vec2 &angle) {
    this->pos = pos;
    this->angle = angle;
}

Avatar::Avatar() {
    this->pos = glm::vec3();
    this->angle = glm::vec2();
}

Avatar::~Avatar() {

}

