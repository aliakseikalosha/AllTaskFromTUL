//
// Created by Aliaksei Kalosha on 18.05.2022.
//

#ifndef PG2GAME_ACTORRENDERER_H
#define PG2GAME_ACTORRENDERER_H


#include <GLES3/gl3.h>
#include "MeshRenderer.h"

class ActorRenderer : public  MeshRenderer{
public:
    void render(const glm::mat4 &projectionMatrix, const glm::mat4 &viewMatrix, const float &dt, const glm::vec3 &pos, const glm::vec2 &angle, std::vector<LightData> lights) override{
        now+=dt;
        glUseProgram(shaderProgram);
        setMatrix(projectionMatrix, viewMatrix, pos, angle);
        glUniform3fv(glGetUniformLocation(shaderProgram, "worldPos"), 1, glm::value_ptr(pos));

        setLight(lights, pos);

        glActiveTexture(GL_TEXTURE0);
        glBindTexture(GL_TEXTURE_2D, textureId);
        glActiveTexture(GL_TEXTURE1);
        glBindTexture(GL_TEXTURE_2D, normalId);
        glBindVertexArray(VAO);
        glDrawElements(GL_TRIANGLES, vertSize, GL_UNSIGNED_INT, 0);
        glBindVertexArray(0);
    }
};


#endif //PG2GAME_ACTORRENDERER_H
