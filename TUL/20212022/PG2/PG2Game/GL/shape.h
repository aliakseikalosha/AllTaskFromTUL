//
// Created by Aliaksei Kalosha on 09.03.2022.
//
#pragma once
#include <vector>
#include <math.h>
#include <iostream>
#include "Vertex.h"

Vertex combineVertex(float x, float y, float z, float textureX, float textureY, float  nx, float  ny, float nz){
    return {glm::vec3 (x, y, z), glm::vec2 (textureX, textureY), glm::vec3 (nx,ny,nz)};
}

void quadH(std::vector<Vertex> &vert, std::vector<unsigned int> &ind, float a, glm::vec2 bottomLeft, float textureA) {
    vert.clear();
    ind.clear();
    vert = {
            combineVertex(0,0,0,bottomLeft.x, bottomLeft.y, 0,1,0),
            combineVertex(a,0,0,bottomLeft.x - textureA, bottomLeft.y, 0,1,0),
            combineVertex(0,0,a,bottomLeft.x, bottomLeft.y - textureA, 0,1,0),
            combineVertex(a,0,a,bottomLeft.x - textureA, bottomLeft.y - textureA, 0,1,0),
    };


    ind = {
            // lower left triangle
            0, 2, 1,
            // upper right triangle
            2, 3, 1
        };
}

void customQuadV(std::vector<Vertex> &vert, std::vector<unsigned int> &ind,float height, float  width, glm::vec2 bottomLeft, float textureHeight, float  textureWidth){
    vert.clear();
    ind.clear();
    vert = {
            combineVertex(0,0,0,bottomLeft.x, bottomLeft.y, 0,0,-1),
            combineVertex(width,0,0,bottomLeft.x - textureWidth, bottomLeft.y,0,0,-1),
            combineVertex(0,height,0,bottomLeft.x, bottomLeft.y - textureHeight,0,0,-1),
            combineVertex(width,height,0,bottomLeft.x - textureWidth, bottomLeft.y - textureHeight,0,0,-1),
    };
    ind={
            0,2,1,
            2,3,1,
    };
}
