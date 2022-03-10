glDebugMessageCallback(MessageCallback, 0);
glEnable(GL_DEBUG_OUTPUT);

//default is asynchronous debug output, use this to simulate glGetError() functionality
glEnable(GL_DEBUG_OUTPUT_SYNCHRONOUS);