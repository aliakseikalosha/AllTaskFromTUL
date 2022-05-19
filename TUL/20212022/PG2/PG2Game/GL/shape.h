//
// Created by Aliaksei Kalosha on 09.03.2022.
//
#pragma once
#include <vector>
#include <math.h>
#include <iostream>
#include "Vertex.h"

#define PI (3.14159265)

void annulus(std::vector<float> &vert, std::vector<unsigned int> &ind, float r1, float r2, float x, float y, float z) {
    vert.clear();
    ind.clear();
    int step = 15;
    int m = 360 / step * 2;
    float angle = (step * 1.0f) / 180 * PI;
    float c;
    float s;
    for (int i = 0; i < 360 / step; i++) {
        c = cos(i * angle);
        s = sin(i * angle);
        vert.push_back(x + r1 * c);
        vert.push_back(y + r1 * s);
        vert.push_back(z);
        vert.push_back(x + r2 * c);
        vert.push_back(y + r2 * s);
        vert.push_back(z);

        ind.push_back((i * 2) % m);
        ind.push_back((i * 2 + 2) % m);
        ind.push_back((i * 2 + 1) % m);

        ind.push_back((i * 2 + 1) % m);
        ind.push_back((i * 2 + 2) % m);
        ind.push_back((i * 2 + 3) % m);
    }
}

void quadV(std::vector<Vertex> &vert, std::vector<unsigned int> &ind, std::vector<float> &tex, float a, glm::vec2 bottomLeft, float textureA) {
    vert.clear();
    ind.clear();
    vert = {
            {glm::vec3 (0, 0, 0), glm::vec2 (bottomLeft.x, bottomLeft.y)},
            {glm::vec3 (a, 0, 0), glm::vec2 (bottomLeft.x + textureA, bottomLeft.y)},
            {glm::vec3 ( 0, a, 0), glm::vec2 (bottomLeft.x, bottomLeft.y + textureA)},
            {glm::vec3 (a, a, 0), glm::vec2 (bottomLeft.x + textureA, bottomLeft.y + textureA)},
    };

    ind =
            {
                    // lower left triangle
                    0, 2, 1,
                    // upper right triangle
                    2, 3, 1
            };
}

void quadH(std::vector<Vertex> &vert, std::vector<unsigned int> &ind, float a, glm::vec2 bottomLeft, float textureA) {
    vert.clear();
    ind.clear();
    vert = {
            {glm::vec3 (0, 0, 0), glm::vec2 (bottomLeft.x, bottomLeft.y)},
            {glm::vec3 (a, 0, 0), glm::vec2 (bottomLeft.x + textureA, bottomLeft.y)},
            {glm::vec3 ( 0, 0, a), glm::vec2 (bottomLeft.x, bottomLeft.y + textureA)},
            {glm::vec3 (a, 0, a), glm::vec2 (bottomLeft.x + textureA, bottomLeft.y + textureA)},
    };


    ind = {
            // lower left triangle
            0, 2, 1,
            // upper right triangle
            2, 3, 1
        };
}

Vertex combineVertex(float x, float y, float z, float textureX, float textureY){
    return {glm::vec3 (x, y, z), glm::vec2 (textureX, textureY)};
}


void toplessCube(std::vector<Vertex> &vert, std::vector<unsigned int> &ind, int a, glm::vec2 bottomLeft, float textureA) {
    vert.clear();
    ind.clear();
    vert = {
            combineVertex(0,a,0,bottomLeft.x, bottomLeft.y + textureA),
            combineVertex(0,0,0,bottomLeft.x, bottomLeft.y),
            combineVertex(a,a,0,bottomLeft.x + textureA, bottomLeft.y + textureA),
            combineVertex(a,0,0,bottomLeft.x + textureA, bottomLeft.y),

            combineVertex(a,a,a,bottomLeft.x, bottomLeft.y + textureA),
            combineVertex(a,0,a,bottomLeft.x, bottomLeft.y),
            combineVertex(0,a,a,bottomLeft.x + textureA, bottomLeft.y + textureA),
            combineVertex(0,0,a,bottomLeft.x + textureA, bottomLeft.y),
    };
    ind={
            1,0,3,
            0,2,3,

            3,2,5,
            2,4,5,

            5,4,7,
            4,6,7,

            7,6,1,
            6,0,1
    };
}

void customQuadV(std::vector<Vertex> &vert, std::vector<unsigned int> &ind,float height, float  width, glm::vec2 bottomLeft, float textureHeight, float  textureWidth){
    vert.clear();
    ind.clear();
    vert = {
            combineVertex(0,0,0,bottomLeft.x, bottomLeft.y),
            combineVertex(width,0,0,bottomLeft.x + textureWidth, bottomLeft.y),
            combineVertex(0,height,0,bottomLeft.x, bottomLeft.y + textureHeight),
            combineVertex(width,height,0,bottomLeft.x + textureWidth, bottomLeft.y + textureHeight),
    };
    ind={
            0,2,1,
            2,3,1,
    };
}
