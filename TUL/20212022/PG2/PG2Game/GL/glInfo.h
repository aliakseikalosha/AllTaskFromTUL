//
// Created by Aliaksei Kalosha on 08.03.2022.
//
#include <OpenGL/gl3.h>
#include <iostream>

void printInfo(){
    //GL_VENDOR, GL_RENDERER, GL_VERSION, GL_SHADING_LANGUAGE_VERSION
    //GL_CONTEXT_PROFILE_MASK GL_CONTEXT_CORE_PROFILE_BIT
    const char *renderer = (const char *) glGetString(GL_RENDERER);
    const char *version = (const char *) glGetString(GL_VERSION);
    const char *vendor = (const char *) glGetString(GL_VENDOR);
    const char *shaderVersion = (const char *) glGetString(GL_SHADING_LANGUAGE_VERSION);
    printf("GL_RENDERER = %s\n", renderer);
    printf("GL_VERSION = %s\n", version);
    printf("GL_VENDOR = %s\n", vendor);
    printf("GL_SHADING_LANGUAGE_VERSION = %s\n", shaderVersion);

    GLint data;
    glGetIntegerv(GL_CONTEXT_PROFILE_MASK, &data);
    printf("GL_CONTEXT_PROFILE_MASK = %i\n", data);
    glGetIntegerv(GL_CONTEXT_CORE_PROFILE_BIT, &data);
    printf("GL_CONTEXT_CORE_PROFILE_BIT = %i\n", data);
}