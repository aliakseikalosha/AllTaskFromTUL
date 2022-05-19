//
// Created by Aliaksei Kalosha on 18.05.2022.
//

#ifndef PG2GAME_ACTORRENDERER_H
#define PG2GAME_ACTORRENDERER_H


#include <GLES3/gl3.h>
#include "MeshRenderer.h"

class ActorRenderer : public  MeshRenderer{
public:
    void render(const glm::mat4 &projectionMatrix, const glm::mat4 &viewMatrix, const float &dt, const glm::vec3 &pos, std::vector<LightData> lights) override{
        now+=dt;
        //set uniform for shaders - projection matrix
        glUniformMatrix4fv(glGetUniformLocation(shaderProgram, "uProj_m"), 1, GL_FALSE, glm::value_ptr(projectionMatrix));
        glUniformMatrix4fv(glGetUniformLocation(shaderProgram, "uV_m"), 1, GL_FALSE, glm::value_ptr(viewMatrix));
        glm::mat4 modelMatrix = glm::translate(glm::mat4(1.0f), pos);
        glUniformMatrix4fv(glGetUniformLocation(shaderProgram, "uM_m"), 1, GL_FALSE, glm::value_ptr(modelMatrix));
        glUniform3fv(glGetUniformLocation(shaderProgram, "worldPos"), 1, glm::value_ptr(pos));

        glUseProgram(shaderProgram);
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
