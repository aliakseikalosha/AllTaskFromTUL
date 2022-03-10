// icp.cpp 
// Author: JJ

// C++
// include anywhere, in any order
#include <iostream>
#include <chrono>
#include <stack>
#include <random>

// OpenCV 
// does not depend on GL
#include <opencv2\opencv.hpp>

// OpenGL Extension Wrangler: allow all multiplatform GL functions
#include <GL/glew.h> 
// WGLEW = Windows GL Extension Wrangler (change for different platform) 
// platform specific functions (in this case Windows)
#include <GL/wglew.h> 

// GLFW toolkit
// Uses GL calls to open GL context, i.e. GLEW must be first.
#include <GLFW/glfw3.h>

// OpenGL math
#include <glm/glm.hpp>
#include <glm/gtc/type_ptr.hpp>

// OpenGL textures
#include <gli/gli.hpp>

//project includes...
#include "globals.h"
#include "init.h"
#include "callbacks.h"
#include "glerror.h" // Check for GL errors

#include "lua_engine.h"
#include "lua_interface.h"

// forward declarations


//---------------------------------------------------------------------
// MAIN
//---------------------------------------------------------------------
int main(int argc, char** argv)
{
	// init glew
	// http://glew.sourceforge.net/basic.html
	glewInit();
	wglewInit();

	// init glfw
	// https://www.glfw.org/documentation.html
	glfwInit();

	// open window (GL canvas) with no special properties
    // https://www.glfw.org/docs/latest/quick.html#quick_create_window
	GLFWwindow * window = glfwCreateWindow(800, 600, "OpenGL context", NULL, NULL);

	// Application loop
	while (!glfwWindowShouldClose(window))
	{
		// ... do_something();
		// 
		// if (condition)
		// 
		
		// Clear OpenGL canvas, both color buffer and Z-buffer
		glClear(GL_COLOR_BUFFER_BIT|GL_DEPTH_BUFFER_BIT);

		// Swap front and back buffers
		glfwSwapBuffers(window);

		// Poll for and process events
		glfwPollEvents();
	}


	if (window)
		glfwDestroyWindow(window);
	glfwTerminate();

	exit(EXIT_SUCCESS);
}


