#pragma once

#include <vector>
#include <string>
#include <glm/vec3.hpp>
#include <glm/vec2.hpp>
#include "shape.h"
#include "Vertex.h"

//
// Created by Aliaksei Kalosha on 11.04.2022.
//

class Mesh {
public:
    std::vector<Vertex> vertex;
    std::vector<unsigned int> index;

    void loadFloor(glm::vec2 bottomLeft, float textureWidth) {
        quadH(vertex, index, 1, bottomLeft, textureWidth);
    }

    void loadWall(glm::vec2 bottomLeft, float textureWidth){
        toplessCube(vertex, index, 1, bottomLeft, textureWidth);
    }
};