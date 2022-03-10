// get any info from GL
// https://docs.gl/gl3/glGet
glGet*


//get integer
GLint myint;
glGetIntegerv(GL_SOME_PARAMETER_NAME, &myint);

std::cout << "Value is:" << myint << '\n';

//get string
const char* mystring = (const char*)glGetString(GL_SOME_PARAMETER_NAME);
 
std::cout << "Value is:" << mystring << '\n';
