//
// Created by Aliaksei Kalosha on 17.05.2022.
//

#ifndef PG2GAME_LIGHT_H
#define PG2GAME_LIGHT_H


#pragma once
#include "SceneObject.h"
#include "../Data/LightData.h"
#include "../Renderer/LightRenderer.h"

class Light : public SceneObject{
private:
    Mesh empty;
    glm::vec4 color;
public:

    Light(const glm::vec3 &pos, const glm::vec2 &angle, const glm::vec4 &color){
        this->pos = pos;
        this->angle = angle;
        this->color = color;
        LightRenderer *r = new LightRenderer(color);
        r->init(empty, "../resources/shader/light.vert", "../resources/shader/light.frag", -1, -1);
        this->renderer = r;
    }

    LightData getData(){
        return LightData(pos, color);
    }
};


#endif //PG2GAME_LIGHT_H
