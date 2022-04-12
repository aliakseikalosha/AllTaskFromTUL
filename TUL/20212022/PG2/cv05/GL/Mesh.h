#pragma once
#include <vector>
#include <string>
#include <glm/vec3.hpp>
#include <glm/vec2.hpp>

//
// Created by Aliaksei Kalosha on 11.04.2022.
//
struct Vertex{
    glm::vec3 pos;
    glm::vec3 normal;
    glm::vec2 texture;
};
class Mesh{
private:
    std::vector<Vertex> vertex;
    std::vector<uint32_t> index;
public:
    void load(std::string pathToModel){

    }
};
