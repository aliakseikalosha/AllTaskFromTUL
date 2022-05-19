//
// Created by Aliaksei Kalosha on 18.05.2022.
//

#ifndef PG2GAME_SPRITEDATA_H
#define PG2GAME_SPRITEDATA_H


#include "glm/gtc/type_ptr.hpp"
class SpriteData {
public:
     glm::vec2 bottomLeft;
     glm::vec2 size;

    SpriteData(const glm::vec2 &bottomLeft, const glm::vec2 &size) : bottomLeft(bottomLeft), size(size) {}
};


#endif //PG2GAME_SPRITEDATA_H
