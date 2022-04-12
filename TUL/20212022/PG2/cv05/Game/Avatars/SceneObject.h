//
// Created by Aliaksei Kalosha on 11.04.2022.
//
#pragma once
#include <glm/vec3.hpp>
#include <glm/vec2.hpp>
#include <glm/geometric.hpp>
#include "../MeshRenderer.h"

class SceneObject{
protected:
    glm::vec3 pos;
    glm::vec2 angle;
public:
    glm::vec3 getPos();
    glm::vec2 getAngle();
    glm::vec3 forvard();
    MeshRenderer *renderer;

    SceneObject();

    SceneObject(const glm::vec3 &pos, const glm::vec2 &angle);

};
SceneObject::SceneObject(const glm::vec3 &pos, const glm::vec2 &angle) {
    this->pos = pos;
    this->angle = angle;
}

SceneObject::SceneObject() {
    this->pos = glm::vec3();
    this->angle = glm::vec2();
}

glm::vec3 SceneObject::getPos() {
    return pos;
}

glm::vec2 SceneObject::getAngle() {
    return angle;
}

glm::vec3 SceneObject::forvard() {
    return glm::normalize(glm::vec3(
            cos(angle.x) * cos(angle.y),
            sin(angle.y),
            sin(angle.x) * cos(angle.y)
    ));
}
