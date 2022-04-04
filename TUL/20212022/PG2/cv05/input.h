//
// Created by Aliaksei Kalosha on 14.03.2022.
//
#pragma once
#include <GLFW/glfw3.h>
#include <glm/vec2.hpp>
#include <glm/vec3.hpp>

struct input {
    glm::vec2 lookDelta = glm::vec2(0.0f, 0.0f);
    glm::vec3 moveDelta = glm::vec3(0.0f, 0.0f, 0.0f);
} inputState;

void mouseButtonCallback(GLFWwindow *window, int button, int action, int mods) {
    if (button == GLFW_MOUSE_BUTTON_LEFT) {
        if (action == GLFW_PRESS) {
            //action
        }
    }
    if (button == GLFW_MOUSE_BUTTON_RIGHT) {
        if (action == GLFW_PRESS) {
            //action
        }
    }
}

bool isEqual(float a, float b, float e) {
    return abs(a - b) < e;
}


static void cursorPositionCallback(GLFWwindow *window, double xpos, double ypos) {
    static glm::vec2 old = glm::vec2(0.0f, 0.0f);
    static glm::vec2 delta = glm::vec2(0.0f, 0.0f);
    static float e = 4.0;
    static bool init = false;
    if (init) {
        if (isEqual(old.x, xpos, e)) {
            inputState.lookDelta.x = 0;
        } else {
            inputState.lookDelta.x = -xpos + old.x > 0 ? 1 : -1;
        }
        if (isEqual(old.y, ypos, e)) {
            inputState.lookDelta.y = 0;
        } else {
            inputState.lookDelta.y = -ypos + old.y > 0 ? 1 : -1;
        }
    } else {
        init = true;
    }
    /*
    //std::cout<< " cursorPositionCallback x:" << xpos <<"y: "<<ypos<<" old x "<< old.x <<" old y : "<< old.y<<std::endl;
    std::cout<< " lookDelta ["<<   inputState.lookDelta.x<<":"<<inputState.lookDelta.y <<"]"
    <<"xpos ["<<xpos<<":"<<ypos<<"]"
    <<std::endl;
     */


    old.x = xpos;
    old.y = ypos;
}


void inputInit(GLFWwindow *window) {
    glfwSetCursorPosCallback(window, cursorPositionCallback);
    glfwSetMouseButtonCallback(window, mouseButtonCallback);
}

bool isPressed(GLFWwindow *window, int key) {
    return glfwGetKey(window, key) == GLFW_PRESS;
}

static input *updateInput(GLFWwindow *window) {
    static glm::vec3 dir = glm::vec3();
    dir.x = 0;
    dir.y = 0;
    dir.z = 0;

    if (isPressed(window, GLFW_KEY_W)) {
        dir.z += 1;
    }
    if (isPressed(window, GLFW_KEY_S)) {
        dir.z -= 1;
    }
    if (isPressed(window, GLFW_KEY_A)) {
        dir.x -= 1;
    }
    if (isPressed(window, GLFW_KEY_D)) {
        dir.x += 1;
    }
    inputState.moveDelta = dir;
    return &inputState;
}

glm::vec3 moveDirection(glm::vec3 &move, glm::vec2 &angle) {
    static glm::vec3 dir = glm::vec3();
    dir.x = move.x * cos(angle.x);
    dir.y = 0;
    dir.z = move.z * sin(angle.x);
    return dir;
}

void resetLook() {
    inputState.lookDelta.x = 0;
    inputState.lookDelta.y = 0;
}