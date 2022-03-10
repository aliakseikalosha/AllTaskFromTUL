int main(int argc, char* argv[])
{
  
// ...  init ...

//... create and activate shaders ...

// create and set projection matrix
// you can only set uniforms AFTER shader compile & activation 
{
	int width, height;
	glfwGetWindowSize(window, &width, &height);
	if (height <= 0)
		height = 1;

	float ratio = static_cast<float>(width) / height;
	
	glm::mat4 projectionMatrix = glm::perspective(
		glm::radians(60.0f), // The vertical Field of View, in radians: the amount of "zoom". Think "camera lens". Usually between 90° (extra wide) and 30° (quite zoomed in)
		ratio,			     // Aspect Ratio. Depends on the size of your window.
		0.1f,                // Near clipping plane. Keep as big as possible, or you'll get precision issues.
		20000.0f              // Far clipping plane. Keep as little as possible.
	);
    
    //set uniform for shaders - projection matrix
    GLint location_proj = glGetUniformLocation(shader_prog_ID, "uProj_m"); 
    glUniformMatrix4fv(location_proj, 1, GL_FALSE, glm::value_ptr(projectionMatrix));
    
    //or projection matrix with one-liner
    //glUniformMatrix4fv(glGetUniformLocation(currProgram, "uProj_m"), 1, GL_FALSE, glm::value_ptr(projectionMatrix));
    
    // set viewport
    glViewport(0, 0, width, height);
    
    //now your canvas has [0,0] in bottom left corner, and its size is [width x height] 
    
    //set View matrix - no transformation (so far), e.g. identity matrix (unit matrix)
   	glm::mat4 v_m = glm::mat4();
    glUniformMatrix4fv(glGetUniformLocation(shader_prog_ID, "uV_m"), 1, GL_FALSE, glm::value_ptr(mv_mc));
    
    //set Model matrix - no transformations (so far), e.g. identity matrix (unit matrix)
   	glm::mat4 v_m = glm::mat4();
    glUniformMatrix4fv(glGetUniformLocation(shader_prog_ID, "uM_m"), 1, GL_FALSE, glm::value_ptr(mv_mc));

    // now you are (camera is) at [0,0,0] point, looking at -Z direction  
}    
    
// draw using COMPATIBLE profile
// e.g. glBegin(), glVertex(), etc...
// or array of vertices and glDrawArray() etc...    
    
    
}