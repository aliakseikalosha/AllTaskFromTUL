
#include <iostream>
#include <stdio.h>
#define GL_SILENCE_DEPRECATION
#include "glInfo.h"
#include <chrono>
// OpenCV 
// does not depend on GL
#include <opencv2/opencv.hpp>

// Without this gl.h gets included instead of gl3.h
#define GLFW_INCLUDE_NONE
#include <GLFW/glfw3.h>

// For includes related to OpenGL, make sure their are included after glfw3.h
#include <OpenGL/gl3.h>

void errorCallback(int error, const char *description)
{
    fputs(description, stderr);
}

void keyCallback(GLFWwindow *window, int key, int scancode, int action, int mods)
{
    if (key == GLFW_KEY_ESCAPE && action == GLFW_PRESS)
        glfwSetWindowShouldClose(window, GLFW_TRUE);
}

void frameBufferResizeCallback(GLFWwindow *window, int width, int height)
{
    std::cout << "framebuffer w:" << width << " h:" << height << std::endl;
    glViewport(0, 0, width, height);
}

int main(void)
{
    GLFWwindow *window;

    // Set callback for errors
    glfwSetErrorCallback(errorCallback);

    // Initialize the library
    if (glfwInit() == GLFW_FALSE)
    {
        std::cout << "Error while glfwInit" << std::endl;
        return -1;
    }

    //https://stackoverflow.com/questions/28242148/glews-glewissupportedgl-version-3-1-returning-false-on-mbp-osx-10-10-1-with
    //GLenum err = glewInit();
    //if (GLEW_OK != err)
    //{
    //    // Problem: glewInit failed, something is seriously wrong.
    //    fprintf(stderr, "Error: %s\n", glewGetErrorString(err));
    //    return -1;
    //}

    glfwWindowHint(GLFW_CONTEXT_VERSION_MAJOR, 3);
    glfwWindowHint(GLFW_CONTEXT_VERSION_MINOR, 3);

    // Without these two hints, nothing above OpenGL version 2.1 is supported
    glfwWindowHint(GLFW_OPENGL_FORWARD_COMPAT, GLFW_TRUE);
    glfwWindowHint(GLFW_OPENGL_PROFILE, GLFW_OPENGL_CORE_PROFILE);


    // open window (GL canvas) with no special properties
    // https://www.glfw.org/docs/latest/quick.html#quick_create_window
    window = glfwCreateWindow(800, 600, "OpenGL context", NULL, NULL);


    glfwSetFramebufferSizeCallback(window, frameBufferResizeCallback);

    // Make the window's context current
    glfwMakeContextCurrent(window);

    // Used to avoid screen tearing
    glfwSwapInterval(0);

    // OpenGL initializations start from here
    glClearColor(0.2f, 0.3f, 0.3f, 1.0f);

    // Vertex array object
    unsigned int VAO;
    glGenVertexArrays(1, &VAO);
    glBindVertexArray(VAO);

    // Vertex data and buffer
    float vertices[] = {
            -0.5f, -0.5f, 0.0f,
            0.5f, -0.5f, 0.0f,
            0.0f, 0.5f, 0.0f};
    unsigned int VBO;
    glGenBuffers(1, &VBO);
    glBindBuffer(GL_ARRAY_BUFFER, VBO);
    glBufferData(GL_ARRAY_BUFFER, sizeof(vertices), vertices, GL_STATIC_DRAW);

    // Vertex shader
    const char *vertexShaderSource = "#version 410 core\n"
                                     "layout (location = 0) in vec3 aPos;\n"
                                     "void main()\n"
                                     "{\n"
                                     "   gl_Position = vec4(aPos.x, aPos.y, aPos.z, 1.0);\n"
                                     "}\0";
    unsigned int vertexShader;
    vertexShader = glCreateShader(GL_VERTEX_SHADER);
    glShaderSource(vertexShader, 1, &vertexShaderSource, NULL);
    glCompileShader(vertexShader);
    int success;
    char infoLog[512];
    glGetShaderiv(vertexShader, GL_COMPILE_STATUS, &success);
    if (!success)
    {
        glGetShaderInfoLog(vertexShader, 512, NULL, infoLog);
        fputs(infoLog, stderr);
    }

    // Fragment shader
    const char *fragmentShaderSource = "#version 410 core\n"
                                       "out vec4 FragColor;\n"
                                       "void main()\n"
                                       "{\n"
                                       "   FragColor = vec4(0.0f, 0.5f, 0.2f, 1.0f);\n"
                                       "}\0";
    unsigned int fragmentShader;
    fragmentShader = glCreateShader(GL_FRAGMENT_SHADER);
    glShaderSource(fragmentShader, 1, &fragmentShaderSource, NULL);
    glCompileShader(fragmentShader);
    glGetShaderiv(fragmentShader, GL_COMPILE_STATUS, &success);
    if (!success)
    {
        glGetShaderInfoLog(vertexShader, 512, NULL, infoLog);
        fputs(infoLog, stderr);
    }

    // Shader program
    unsigned int shaderProgram;
    shaderProgram = glCreateProgram();
    glAttachShader(shaderProgram, vertexShader);
    glAttachShader(shaderProgram, fragmentShader);
    glLinkProgram(shaderProgram);
    glDeleteShader(vertexShader);
    glDeleteShader(fragmentShader);
    glGetProgramiv(shaderProgram, GL_LINK_STATUS, &success);
    if (!success)
    {
        glGetProgramInfoLog(shaderProgram, 512, NULL, infoLog);
        fputs(infoLog, stderr);
    }
    glUseProgram(shaderProgram);

    // Binding the buffers
    glVertexAttribPointer(0, 3, GL_FLOAT, GL_FALSE, 3 * sizeof(float), (void *)0);
    glEnableVertexAttribArray(0);


    printInfo();
    // Application loop
    auto start = std::chrono::steady_clock::now();
    auto end = std::chrono::steady_clock::now();
    std::chrono::duration<double> elapsed_seconds;
    int frameCount=0;

    while (!glfwWindowShouldClose(window))
    {
        frameCount++;
        // Resize the viewport
        int width, height;
        glfwGetFramebufferSize(window, &width, &height);
        glViewport(0, 0, width, height);

        // OpenGL Rendering related code
        glClear(GL_COLOR_BUFFER_BIT);
        glUseProgram(shaderProgram);
        glBindVertexArray(VAO);
        glDrawArrays(GL_TRIANGLES, 0, 3);

        // Swap front and back buffers
        glfwSwapBuffers(window);

        // Poll for and process events
        glfwPollEvents();

        end = std::chrono::steady_clock::now();
        elapsed_seconds = end-start;
        if(elapsed_seconds.count()>1.0){
            std::string fps(std::to_string(frameCount/elapsed_seconds.count()));
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