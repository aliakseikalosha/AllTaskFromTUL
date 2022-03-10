//
// Created by Aliaksei Kalosha on 09.03.2022.
//
#include <vector>
#include <math.h>
#include <iostream>

#define PI (3.14159265)

void shapeAnnulus(std::vector<float> &vert, float r1, float r2) {
    vert.clear();
    std::vector<int> ind;
    float z = 0;
    int step = 15;
    int m = 360/step*2;
    float angle = (step * 1.0f) / 180 * PI;
    float c;
    float s;
    for (int i = 0; i < 360 / step; i++) {
        c = cos(i * angle);
        s = sin(i * angle);
        vert.push_back(r1 * c);
        vert.push_back(r1 * s);
        vert.push_back(z);
        vert.push_back(r2 * c);
        vert.push_back(r2 * s);
        vert.push_back(z);

        ind.push_back((i * 2) % m);
        ind.push_back((i * 2 + 2) % m);
        ind.push_back((i * 2 + 1) % m);

        ind.push_back((i * 2 + 1) % m);
        ind.push_back((i * 2 + 2) % m);
        ind.push_back((i * 2 + 3) % m);
    }
    std::vector<float> vertices;
    for (const auto &item: ind) {
        vertices.push_back(vert.at(item * 3));
        vertices.push_back(vert.at(item * 3 + 1));
        vertices.push_back(vert.at(item * 3 + 2));
    }
    vert = vertices;
}
