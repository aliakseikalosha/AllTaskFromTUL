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


    ind =
            {
                    // lower left triangle
                    0, 2, 1,
                    // upper right triangle
                    2, 3, 1
            };
}



void toplessCube(std::vector<Vertex> &vert, std::vector<unsigned int> &ind, int a, glm::vec2 bottomLeft, float textureA) {
    vert.clear();
    ind.clear();
    vert = {
            {glm::vec3 (0, 0, 0), glm::vec2 (bottomLeft.x, bottomLeft.y)},
            {glm::vec3 (a, 0, 0), glm::vec2 (bottomLeft.x + textureA, bottomLeft.y)},
            {glm::vec3 ( 0, 0, a), glm::vec2 (bottomLeft.x, bottomLeft.y + textureA)},
            {glm::vec3 (a, 0, a), glm::vec2 (bottomLeft.x + textureA, bottomLeft.y + textureA)},
    };


    ind =
            {
                    // lower left triangle
                    0, 2, 1,
                    // upper right triangle
                    2, 3, 1
            };
}
