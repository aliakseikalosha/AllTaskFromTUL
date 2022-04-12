//
// Created by Aliaksei Kalosha on 21.03.2022.
//
#pragma once
#include "Avatar.h"

class Camera : public Avatar{
private:

public:
    Camera(const glm::vec3 &pos, const glm::vec2 &angle);
    void UpdateFollow(Avatar &avatar,const glm::vec3 offset);
};

void Camera::UpdateFollow(Avatar &avatar, const glm::vec3 offset) {
    glm::vec3 o = glm::vec3 (offset.x, offset.y, offset.z);
    glm::vec3 f = avatar.forvard();
    o.x *= f.x;
    o.y *= f.y;
    o.z *= f.z;

    pos = avatar.getPos() + o;
    angle = avatar.getAngle();
}

Camera::Camera(const glm::vec3 &pos, const glm::vec2 &angle) : Avatar(pos, angle) {}
