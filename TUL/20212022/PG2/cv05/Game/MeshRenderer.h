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
    unsigned int shaderProgram;
    unsigned int VAO, VBO, EBO;

public:
    void init(const Mesh & mesh, const glm::vec3 &pos);
    void render(const glm::mat4 &projectionMatrix, const glm::mat4 &viewMatrix, const float &dt);
};

void MeshRenderer::render(const glm::mat4 &projectionMatrix, const glm::mat4 &viewMatrix, const float &dt) {
    now+=dt;
    //set uniform for shaders - projection matrix
    glUniformMatrix4fv(glGetUniformLocation(shaderProgram, "uProj_m"), 1, GL_FALSE,
                       glm::value_ptr(projectionMatrix));

    glUniformMatrix4fv(glGetUniformLocation(shaderProgram, "uV_m"), 1, GL_FALSE, glm::value_ptr(viewMatrix));

    glm::mat4 modelMat = glm::mat4(1.0f);
            //glm::rotate(glm::mat4(1.0f), (float) sin(now), glm::vec3(0.0f, 0.0f, 1.0f));
    glUniformMatrix4fv(glGetUniformLocation(shaderProgram, "uM_m"), 1, GL_FALSE, glm::value_ptr(modelMat));

    glUseProgram(shaderProgram);
    glBindVertexArray(VAO);
    glDrawElements(GL_TRIANGLES, vertSize, GL_UNSIGNED_INT, 0);
    glBindVertexArray(0);
}

void MeshRenderer::init(const Mesh &mesh, const glm::vec3 &pos) {

    glGenVertexArrays(1, &VAO);
    glBindVertexArray(VAO);

    //LOAD MODEL
    std::vector<float> vert;
    std::vector<unsigned int> ind;
    shapeAnnulus(vert, ind, 1.0f, .8f, pos.x, pos.y, pos.z);

    vertSize = vert.size();
    // Vertex data and buffer
    glGenBuffers(1, &VBO);
    glBindBuffer(GL_ARRAY_BUFFER, VBO);
    glBufferData(GL_ARRAY_BUFFER, vertSize, vert.data(), GL_STATIC_DRAW);

    //Element data and buffer
    glGenBuffers(1, &EBO);
    glBindBuffer(GL_ELEMENT_ARRAY_BUFFER, EBO);
    glBufferData(GL_ELEMENT_ARRAY_BUFFER, sizeof(unsigned int) * ind.size(), ind.data(), GL_STATIC_DRAW);

    // Vertex shader
    unsigned int vertexShader;
    if (!loadShader("../resources/shader/basic.vert", vertexShader, GL_VERTEX_SHADER)) {
        std::cout << "Error in vertex shader" << std::endl;
    }

    // Fragment shader
    unsigned int fragmentShader;
    if (!loadShader("../resources/shader/basic.frag", fragmentShader, GL_FRAGMENT_SHADER)) {
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
