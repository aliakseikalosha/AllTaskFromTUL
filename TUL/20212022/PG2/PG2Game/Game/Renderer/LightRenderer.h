//
// Created by Aliaksei Kalosha on 17.05.2022.
//

#ifndef PG2GAME_LIGHTRENDERER_H
#define PG2GAME_LIGHTRENDERER_H


#pragma once

#include <GLES3/gl3.h>
#include "MeshRenderer.h"
#include "../../GL/glInit.h"

class LightRenderer : public MeshRenderer {
private:
    glm::vec4 color;
public:
    LightRenderer(const glm::vec4 &color) {
        this->color = color;
    }

    void init(const Mesh &mesh, const char *vertPath, const char *fragPath, uint textureId, uint normalId) override {
        Mesh m;
        customQuadV(m.vertex, m.index, 0.1f, 0.1f, glm::vec2(0,0), 0,0 );

        glGenVertexArrays(1, &VAO);
        glBindVertexArray(VAO);

        //LOAD MODEL
        std::vector<Vertex> vert = m.vertex;
        std::vector<unsigned int> ind = m.index;

        vertSize = sizeof(Vertex) * vert.size();
        // Vertex data and buffer
        glGenBuffers(1, &VBO);
        glBindBuffer(GL_ARRAY_BUFFER, VBO);
        glBufferData(GL_ARRAY_BUFFER, vertSize, vert.data(), GL_STATIC_DRAW);

        glVertexAttribPointer(0, 3, GL_FLOAT, GL_FALSE, sizeof(Vertex), reinterpret_cast<void*>(0 + offsetof(Vertex, pos)));
        glEnableVertexAttribArray(0);

        glVertexAttribPointer(1, 2, GL_FLOAT, GL_FALSE, sizeof(Vertex), reinterpret_cast<void*>(0 + offsetof(Vertex, texture)));
        glEnableVertexAttribArray(1);
        //Element data and buffer
        glGenBuffers(1, &EBO);
        glBindBuffer(GL_ELEMENT_ARRAY_BUFFER, EBO);
        glBufferData(GL_ELEMENT_ARRAY_BUFFER, sizeof(unsigned int) * ind.size(), ind.data(), GL_STATIC_DRAW);

        // Vertex shader
        unsigned int vertexShader;
        if (!loadShader(vertPath, vertexShader, GL_VERTEX_SHADER)) {
            std::cout << "Error in vertex shader" << std::endl;
        }

        // Fragment shader
        unsigned int fragmentShader;
        if (!loadShader(fragPath, fragmentShader, GL_FRAGMENT_SHADER)) {
            std::cout << "Error in fragment shader" << std::endl;
        }
        int success;
        char infoLog[512];
        // Shader program
        shaderProgram = glCreateProgram();
        glAttachShader(shaderProgram, vertexShader);
        glAttachShader(shaderProgram, fragmentShader);
        glLinkProgram(shaderProgram);
        glDeleteShader(vertexShader);
        glDeleteShader(fragmentShader);
        glGetProgramiv(shaderProgram, GL_LINK_STATUS, &success);
        if (!success) {
            glGetProgramInfoLog(shaderProgram, 512, NULL, infoLog);
            fputs(infoLog, stderr);
        }
    }

    void render(const glm::mat4 &projectionMatrix, const glm::mat4 &viewMatrix, const float &dt, const glm::vec3 &pos,const glm::vec2 &angle, std::vector<LightData> lights) override{
        now+=dt;
        glUseProgram(shaderProgram);
        //set uniform for shaders - projection matrix
        glUniformMatrix4fv(glGetUniformLocation(shaderProgram, "uProj_m"), 1, GL_FALSE, glm::value_ptr(projectionMatrix));
        glUniformMatrix4fv(glGetUniformLocation(shaderProgram, "uV_m"), 1, GL_FALSE, glm::value_ptr(viewMatrix));
        glm::mat4 modelMatrix = glm::translate(glm::mat4(1.0f), pos);
        glUniformMatrix4fv(glGetUniformLocation(shaderProgram, "uM_m"), 1, GL_FALSE, glm::value_ptr(modelMatrix));

        glUniform4fv(glGetUniformLocation(shaderProgram, "color"), 1, glm::value_ptr(color));
        glUniform3fv(glGetUniformLocation(shaderProgram, "worldPos"), 1, glm::value_ptr(pos));

        glBindVertexArray(VAO);
        glDrawElements(GL_TRIANGLES, vertSize, GL_UNSIGNED_INT, 0);
        glBindVertexArray(0);
    }
};


#endif //PG2GAME_LIGHTRENDERER_H
