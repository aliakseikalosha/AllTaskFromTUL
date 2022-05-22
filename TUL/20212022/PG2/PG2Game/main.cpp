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

#include "glm/gtc/type_ptr.hpp"
#include "GL/input.h"
#include "Game/Game.h"

int main(void) {
    GLFWwindow *window = init();
    if (window == NULL) {
        std::cout << "Failed Create Window" << std::endl;
        exit(EXIT_FAILURE);
    }
    inputInit(window);
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

        glfwGetWindowSize(window, &width, &height);
        if (height <= 0)
            height = 1;
        float ratio = static_cast<float>(width) / height;

        glm::mat4 projectionMatrix = glm::perspective(glm::radians(60.0f), ratio, 0.1f, 10000.0f);

        game->Update(dt);

        //glEnable(GL_CULL_FACE);
        glEnable(GL_DEPTH_TEST);
        glDepthFunc(GL_LESS);
        glClear(GL_COLOR_BUFFER_BIT | GL_DEPTH_BUFFER_BIT);
        game->Draw(projectionMatrix, dt);

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
