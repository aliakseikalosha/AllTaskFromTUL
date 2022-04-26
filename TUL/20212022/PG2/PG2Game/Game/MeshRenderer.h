//
// Created by Aliaksei Kalosha on 11.04.2022.
//
#include <string>
#include <glm/ext/matrix_float4x4.hpp>
#include "../GL/Mesh.h"

class MeshRenderer{
private:
    std::string vertexShaderPath;
    std::string fragmentShaderPath;
    GLsizei vertSize;
    GLfloat now = 0;
    GLuint textureId;
    unsigned int shaderProgram;
    unsigned int VAO, VBO, EBO;

public:
    void init(const Mesh & mesh, const char* vertPath, const char* fragPath, GLuint textureId);
    void render(const glm::mat4 &projectionMatrix, const glm::mat4 &viewMatrix, const float &dt,const glm::vec3 &pos);
};

void MeshRenderer::render(const glm::mat4 &projectionMatrix, const glm::mat4 &viewMatrix, const float &dt, const glm::vec3 &pos) {
    now+=dt;
    //set uniform for shaders - projection matrix
    glUniformMatrix4fv(glGetUniformLocation(shaderProgram, "uProj_m"), 1, GL_FALSE,
                       glm::value_ptr(projectionMatrix));

    glUniformMatrix4fv(glGetUniformLocation(shaderProgram, "uV_m"), 1, GL_FALSE, glm::value_ptr(viewMatrix));

    glm::mat4 modelMatrix = glm::translate(glm::mat4(1.0f), pos);
    glUniformMatrix4fv(glGetUniformLocation(shaderProgram, "uM_m"), 1, GL_FALSE, glm::value_ptr(modelMatrix));

    glUseProgram(shaderProgram);
    glBindTexture(GL_TEXTURE_2D, textureId);
    glBindVertexArray(VAO);
    glDrawElements(GL_TRIANGLES, vertSize, GL_UNSIGNED_INT, 0);
    glBindVertexArray(0);
}

void MeshRenderer::init(const Mesh &mesh, const char* vertPath, const char* fragPath, GLuint textureId) {

    glGenVertexArrays(1, &VAO);
    glBindVertexArray(VAO);

    //LOAD MODEL
    std::vector<Vertex> vert = mesh.vertex;
    std::vector<unsigned int> ind = mesh.index;
    //annulus(vert, ind, .5f, .4f, 0, 0, 0);
    this->textureId = textureId;
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

    glUseProgram(shaderProgram);
}
