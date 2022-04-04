//
// Created by Aliaksei Kalosha on 21.03.2022.
//
#pragma once

#include <glm/vec3.hpp>
#include <glm/vec2.hpp>
#include <cmath>
#include <glm/geometric.hpp>
#include <iostream>

class Avatar {
protected:
    glm::vec3 pos;
    glm::vec2 angle;
public:
    glm::vec3 getPos();

    glm::vec2 getAngle();

    glm::vec3 forvard();

    Avatar();

    Avatar(const glm::vec3 &pos, const glm::vec2 &angle);

    ~Avatar();

    virtual void printState() {
        std::cout << "pos : [" << pos.x << "," << pos.y << "," << pos.x << "]\n" << "angle : [" << angle.x << "," << angle.y << "]\n" << std::endl;
    }
};

glm::vec3 Avatar::getPos() {
    return pos;
}

glm::vec2 Avatar::getAngle() {
    return angle;
}

glm::vec3 Avatar::forvard() {

    return glm::normalize(glm::vec3(
            cos(angle.x) * cos(angle.y),
            sin(angle.y),
            sin(angle.x) * cos(angle.y)
    ));
}

Avatar::Avatar(const glm::vec3 &pos, const glm::vec2 &angle) : pos(pos), angle(angle) {}

Avatar::Avatar() : pos(glm::vec3()), angle(glm::vec2()) {}

Avatar::~Avatar() {

}

