//
// Created by Aliaksei Kalosha on 11.04.2022.
//
#pragma once
#include <string>
#include <glm/ext/matrix_float4x4.hpp>
#include "../../GL/Mesh.h"
#include "../Data/LightData.h"
#include "../Config.h"

#define MAX_LIGHT_COUNT (10)

class MeshRenderer{
protected:
    glm::vec4  ambientLight = glm::vec4 ();
    GLuint textureId;
    GLuint normalId;
    unsigned int shaderProgram;
    GLfloat now = 0;
    GLsizei vertSize;
    unsigned int VAO,VBO, EBO;

public:
    virtual void init(const Mesh & mesh, const char* vertPath, const char* fragPath, GLuint textureId, GLuint normalId){

        glGenVertexArrays(1, &VAO);
        glBindVertexArray(VAO);

        //LOAD MODEL
        std::vector<Vertex> vert = mesh.vertex;
        std::vector<unsigned int> ind = mesh.index;
        //annulus(vert, ind, .5f, .4f, 0, 0, 0);
        this->textureId = textureId;
        this->normalId = normalId;
        vertSize = sizeof(Vertex) * vert.size();
        // Vertex data and buffer
        glGenBuffers(1, &VBO);
        glBindBuffer(GL_ARRAY_BUFFER, VBO);
        glBufferData(GL_ARRAY_BUFFER, vertSize, vert.data(), GL_STATIC_DRAW);

        glVertexAttribPointer(0, 3, GL_FLOAT, GL_FALSE, sizeof(Vertex), reinterpret_cast<void*>(0 + offsetof(Vertex, pos)));
        glEnableVertexAttribArray(0);

        glVertexAttribPointer(1, 2, GL_FLOAT, GL_FALSE, sizeof(Vertex), reinterpret_cast<void*>(0 + offsetof(Vertex, texture)));
        glEnableVertexAttribArray(1);

        glVertexAttribPointer(2, 3, GL_FLOAT, GL_FALSE, sizeof(Vertex), reinterpret_cast<void*>(0 + offsetof(Vertex, normal)));
        glEnableVertexAttribArray(2);
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

        glUseProgram(shaderProgram);
        glUniform1i(glGetUniformLocation(shaderProgram, "tex"), 0);
        glUniform1i(glGetUniformLocation(shaderProgram, "norm"), 1);
    }

    virtual void setLight(std::vector<LightData> &lights, const glm::vec3 &pos){

        glm::vec3 lightPos[MAX_LIGHT_COUNT];
        glm::vec4 lightColor[MAX_LIGHT_COUNT];
        GLint count = 0;
        for (auto &l:lights) {
            if(count>=MAX_LIGHT_COUNT){
                continue;
            }
            lightPos[count] = l.pos - pos;
            lightColor[count] = l.color;
            count++;
        }
        count = glm::min(count, MAX_LIGHT_COUNT);

        glUniform1i(glGetUniformLocation(shaderProgram, "lightCount"), count);
        glUniform3fv(glGetUniformLocation(shaderProgram, "lightPos"), count, glm::value_ptr(lightPos[0]));
        glUniform4fv(glGetUniformLocation(shaderProgram, "lightColor"), count, glm::value_ptr(lightColor[0]));
    }

    virtual void setMatrix(const glm::mat4 &projectionMatrix, const glm::mat4 &viewMatrix,const glm::vec3 &pos, const glm::vec2 &angle){
        //set uniform for shaders - projection matrix
        glUniformMatrix4fv(glGetUniformLocation(shaderProgram, "uProj_m"), 1, GL_FALSE, glm::value_ptr(projectionMatrix));
        glUniformMatrix4fv(glGetUniformLocation(shaderProgram, "uV_m"), 1, GL_FALSE, glm::value_ptr(viewMatrix));
        glm::mat4 modelMatrix = glm::translate(glm::mat4(1.0f), pos);
        modelMatrix =  glm::rotate(modelMatrix, angle.x, glm::vec3(1.0, 0.0, 0));
        modelMatrix =  glm::rotate(modelMatrix, angle.y, glm::vec3(0.0, 1.0, 0));
        glUniformMatrix4fv(glGetUniformLocation(shaderProgram, "uM_m"), 1, GL_FALSE, glm::value_ptr(modelMatrix));
    }

    virtual void render(const glm::mat4 &projectionMatrix, const glm::mat4 &viewMatrix, const float &dt,const glm::vec3 &pos, const glm::vec2 &angle, std::vector<LightData> lights){
        now+=dt;

        glUseProgram(shaderProgram);
        setMatrix(projectionMatrix, viewMatrix, pos, angle);
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