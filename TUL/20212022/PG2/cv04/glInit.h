//
// Created by Aliaksei Kalosha on 08.03.2022.
//

#include <cstdio>
#include <GLFW/glfw3.h>
#include <iostream>
#include <fstream>
#include "glerror.h"

void errorCallback(int error, const char *description) {
    fputs(description, stderr);
}

void frameBufferResizeCallback(GLFWwindow *window, int width, int height) {
    std::cout << "framebuffer w:" << width << " h:" << height << std::endl;
    glViewport(0, 0, width, height);
}

void keyCallback(GLFWwindow *window, int key, int scancode, int action, int mods) {
    if (key == GLFW_KEY_ESCAPE && action == GLFW_PRESS)
        glfwSetWindowShouldClose(window, GLFW_TRUE);
}

GLFWwindow *init() {

    // Set callback for errors
    glfwSetErrorCallback(errorCallback);

    // Initialize the library
    if (glfwInit() == GLFW_FALSE) {
        std::cout << "Error while glfwInit" << std::endl;
        gl_check_error();
        return NULL;
    }

    //https://stackoverflow.com/questions/28242148/glews-glewissupportedgl-version-3-1-returning-false-on-mbp-osx-10-10-1-with
    //GLenum err = glewInit();
    //if (GLEW_OK != err)
    //{
    //    // Problem: glewInit failed, something is seriously wrong.
    //    fprintf(stderr, "Error: %s\n", glewGetErrorString(err));
    //    return -1;
    //}

    glfwWindowHint(GLFW_CONTEXT_VERSION_MAJOR, 4);
    glfwWindowHint(GLFW_CONTEXT_VERSION_MINOR, 1);

    // Without these two hints, nothing above OpenGL version 2.1 is supported
    glfwWindowHint(GLFW_OPENGL_FORWARD_COMPAT, GLFW_TRUE);
    glfwWindowHint(GLFW_OPENGL_PROFILE, GLFW_OPENGL_CORE_PROFILE);

    //glfwWindowHint(GLFW_DOUBLEBUFFER, GLFW_FALSE);


    // open window (GL canvas) with no special properties
    // https://www.glfw.org/docs/latest/quick.html#quick_create_window
    GLFWwindow *window = glfwCreateWindow(400, 400, "OpenGL context", NULL, NULL);

    glfwSetFramebufferSizeCallback(window, frameBufferResizeCallback);

    // Make the window's context current
    glfwMakeContextCurrent(window);

    // Used to avoid screen tearing
    glfwSwapInterval(0);

    glfwSetKeyCallback(window, keyCallback);

    // OpenGL initializations start from here
    glClearColor(0.2f, 0.3f, 0.3f, 1.0f);
    gl_check_error();
    glfwSetInputMode(window, GLFW_CURSOR, GLFW_CURSOR_DISABLED);
    return window;
}

bool loadShader(const char *path, unsigned int &shader, GLenum shaderType) {
    std::string shaderCode;
    std::ifstream ShaderStream(path, std::ios::in);
    if (ShaderStream.is_open()) {
        std::string Line = "";
        while (getline(ShaderStream, Line))
            shaderCode += "\n" + Line;
        ShaderStream.close();
    }
    char const *sourcePointer = shaderCode.c_str();
    shader = glCreateShader(shaderType);
    glShaderSource(shader, 1, &sourcePointer, NULL);
    glCompileShader(shader);
    int success;
    char infoLog[512];
    glGetShaderiv(shader, GL_COMPILE_STATUS, &success);
    if (!success) {
        glGetShaderInfoLog(shader, 512, NULL, infoLog);
        fputs(infoLog, stderr);
    }
    return success;
}
