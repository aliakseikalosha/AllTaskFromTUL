
#include <iostream>
#include <stdio.h>

#define GL_SILENCE_DEPRECATION

#include "GL/glInfo.h"
#include "GL/glerror.h"
#include "GL/glInit.h"
#include <chrono>
// OpenCV 
// does not depend on GL
#include <opencv2/opencv.hpp>

// Without this gl.h gets included instead of gl3.h
#define GLFW_INCLUDE_NONE

void inputInit(GLFWwindow *window);

#include <GLFW/glfw3.h>

// For includes related to OpenGL, make sure their are included after glfw3.h
#include <OpenGL/gl3.h>
#include "glm/gtc/type_ptr.hpp"
#include "GL/shape.h"
#include "GL/input.h"
#include "Game/Game.h"

int main(void) {
    GLFWwindow *window = init();
    if (window == NULL) {
        std::cout << "Failed Create Window" << std::endl;
        exit(EXIT_FAILURE);
    }
    inputInit(window);
    // Vertex array object
    unsigned int VAO;
    glGenVertexArrays(1, &VAO);
    glBindVertexArray(VAO);

    std::vector<float> vert;
    std::vector<unsigned int> ind;
    shapeAnnulus(vert, ind, 1.0f, .8f, 0, 0, -2);

    // Vertex data and buffer
    unsigned int VBO;
    glGenBuffers(1, &VBO);
    glBindBuffer(GL_ARRAY_BUFFER, VBO);
    glBufferData(GL_ARRAY_BUFFER, sizeof(float) * vert.size(), vert.data(), GL_STATIC_DRAW);

    //Element data and buffer
    unsigned int EBO;
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
    unsigned int shaderProgram;
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

    // Binding the buffers
    glVertexAttribPointer(glGetAttribLocation(shaderProgram, "aPos"), 3, GL_FLOAT, GL_FALSE, 3 * sizeof(float),(void *) 0);
    glEnableVertexAttribArray(0);

    int width, height;
    glfwGetFramebufferSize(window, &width, &height);
    std::cout << "SET to width " << width << " height " << height << std::endl;
    glViewport(0, 0, width, height);
    Game *game = new Game();
    printInfo();
    // Application loop
    auto start = std::chrono::steady_clock::now();
    auto end = std::chrono::steady_clock::now();
    std::chrono::duration<double> elapsed_seconds;
    int frameCount = 0;
    GLfloat prev = (GLfloat) glfwGetTime();
    GLfloat now;
    GLfloat dt = 0;
    while (!glfwWindowShouldClose(window)) {
        frameCount++;
        now = (GLfloat) glfwGetTime();
        dt = now - prev;
        prev = now;

        // Resize the viewport
        updateInput(window);

        // OpenGL Rendering related code
        glClear(GL_COLOR_BUFFER_BIT);
        {
            glfwGetWindowSize(window, &width, &height);
            if (height <= 0)
                height = 1;
            float ratio = static_cast<float>(width) / height;

            glm::mat4 projectionMatrix = glm::perspective(glm::radians(60.0f), ratio, 0.1f, 10000.0f);
            //set uniform for shaders - projection matrix
            glUniformMatrix4fv(glGetUniformLocation(shaderProgram, "uProj_m"), 1, GL_FALSE,
                               glm::value_ptr(projectionMatrix));

            game->Update(dt);
            glm::mat4 viewMat = game->viewMat;
            glUniformMatrix4fv(glGetUniformLocation(shaderProgram, "uV_m"), 1, GL_FALSE, glm::value_ptr(viewMat));

            glm::mat4 modelMat =
                    glm::rotate(glm::mat4(1.0f), (float) sin(now), glm::vec3(0.0f, 0.0f, 1.0f));
            glUniformMatrix4fv(glGetUniformLocation(shaderProgram, "uM_m"), 1, GL_FALSE, glm::value_ptr(modelMat));

            game->Draw(projectionMatrix, dt);
        }

        glUseProgram(shaderProgram);
        glBindVertexArray(VAO);
        glDrawElements(GL_TRIANGLES, vert.size(), GL_UNSIGNED_INT, 0);
        glBindVertexArray(0);

        glGetError();
        // Swap front and back buffers
        glfwSwapBuffers(window);

        // Poll for and process events
        glfwPollEvents();
        //resetLook();
        end = std::chrono::steady_clock::now();
        elapsed_seconds = end - start;
        if (elapsed_seconds.count() > 1.0) {
            std::string fps(std::to_string(frameCount / elapsed_seconds.count()));
            glfwSetWindowTitle(window, fps.c_str());
            start = std::chrono::steady_clock::now();
            frameCount = 0;
        }
    }


    if (window)
        glfwDestroyWindow(window);
    glfwTerminate();

    exit(EXIT_SUCCESS);
}
